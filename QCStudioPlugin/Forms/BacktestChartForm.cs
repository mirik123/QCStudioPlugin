using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using QuantConnect.RestAPI;
using QuantConnect.RestAPI.Models;
using QuantConnect;
using QuantConnect.Models;
using ZedGraph;

namespace QuantConnect.QCPlugin
{
    public partial class BacktestChartForm : Form
    {
        /******************************************************** 
        * CLASS VARIABLES
        *********************************************************/
        private string _backtestId = "";
        private bool _processing = false;
        private IAlgorithm _algorithm;
        private DateTime _startDate = new DateTime();
        private DateTime _endDate = new DateTime();
        private PacketBacktestResult _results = new PacketBacktestResult();
        private bool _backtestResultUpdated = false;
        private Dictionary<string, ZedGraphControl> _chartObjects = new Dictionary<string, ZedGraphControl>();
        private Dictionary<string, Dictionary<string, bool>> _initialized = new Dictionary<string, Dictionary<string, bool>>();
        
        //Cache of data:
        private Dictionary<string, Dictionary<string, CurveItem>> _cache = new Dictionary<string, Dictionary<string, CurveItem>>();
        private Dictionary<string, Dictionary<string, double>> _cacheMax = new Dictionary<string, Dictionary<string, double>>();

        /******************************************************** 
        * INITIALIZE CHARTS
        *********************************************************/
        public BacktestChartForm()
        {
            InitializeComponent();
        }

        public void ShowLogin(Action callback)
        {
            Login form = new Login();
            form.SetCallBacks(callback);
            form.Show();
        }

        /// <summary>
        /// Set the id for this chart:
        /// </summary>
        /// <param name="backtestId"></param>
        public void SetBacktestId(string backtestId)
        {
            this._backtestId = backtestId;
        }

        /// <summary>
        /// Set the results:
        /// </summary>
        public void SetBacktestResult(PacketBacktestResult result)
        {
            this._results = result;
            this._backtestResultUpdated = true;
        }

        public void SetProcessing(bool processing)
        {
            this._processing = processing;
        }

        private void timerRefresh_Tick(object sender, EventArgs e)
        {
            if (_processing) return;
            _processing = true;

            statusLabel.Text = "Updating Results for Backtest Id: " + _backtestId + "...";

            Async.Add(new APIJob(APICommand.BacktestResults, (backtestResult, errors) =>
            {
                //Handle login and API errors:
                switch (QuantConnectPlugin.HandleErrors(errors))
                {
                    //Handle project specific actions with a login error:
                    case APIErrors.NotLoggedIn:
                        this.SafeInvoke(d => d.ShowLogin(() => { OpenProjects form = new OpenProjects(); form.StartPosition = FormStartPosition.CenterScreen; form.Show(); }));
                        this.SafeInvoke(d => d.Close());
                        return;
                }

                //Handle Results Packet:
                PacketBacktestResult packet = (PacketBacktestResult)backtestResult;

                //this.SafeInvoke(d => d.statusProgress.ProgressBar.Value = Convert.ToInt32(packet.Progress.Replace("%", "")));
                this.SafeInvoke(d => d.SetBacktestResult(packet));
                this.SafeInvoke(d => d.SetProcessing(false));

            }, _backtestId)); // End of Async

        } // End of timer:


        /// <summary>
        /// Draw the charts from a result packet:
        /// </summary>
        /// <param name="charts"></param>
        private void DrawCharts(Dictionary<string, Chart> charts)
        {
            //Draw charts:
            foreach (Chart chart in charts.Values)
            {
                DrawChart(charts[chart.Name]);
            }
        }

        /// <summary>
        /// Unix in milliseconds to date time
        /// </summary>
        /// <param name="unix"></param>
        /// <returns></returns>
        private DateTime UnixMsToDateTime(double unix)
        {
            DateTime time = new DateTime(1970, 1, 1, 0, 0, 0, 0);
            time = time.AddSeconds(unix);
            return time;
        }

        /// <summary>
        /// Add the data to the empty chart:
        /// </summary>
        /// <param name="chart"></param>
        private void DrawChart(Chart chart)
        {

            //Setting up this chart for the first time:
            if (!_initialized.ContainsKey(chart.Name))
            {
                _initialized.Add(chart.Name, new Dictionary<string, bool>());
                _cacheMax.Add(chart.Name, new Dictionary<string,double>());
                _cache.Add(chart.Name, new Dictionary<string, CurveItem>());

                //Create the tab and zedgraph control:
                var tab = new TabPage(chart.Name);
                var zedControl = new ZedGraphControl();
                zedControl.MasterPane.PaneList.Clear();
                _chartObjects.Add(chart.Name, zedControl);
                tab.Controls.Add(zedControl);
                tabCharts.TabPages.Add(tab);
            }

            foreach (Series series in chart.Series.Values)
            {
                if (!_initialized[chart.Name].ContainsKey(series.Name))
                {
                    _initialized[chart.Name].Add(series.Name, true);
                    _cacheMax[chart.Name].Add(series.Name, 0);
                    _cache[chart.Name].Add(series.Name, null);

                    Initialize(chart, series);
                }

                switch (series.SeriesType)
                {
                    case SeriesType.Candle:
                        var updatePoints = CreateCandle(series, _cacheMax[chart.Name][series.Name]);
                        foreach (var pt in updatePoints)
                        {
                            _cache[chart.Name][series.Name].AddPoint(pt);
                            _cacheMax[chart.Name][series.Name] = pt.X;
                        }
                        break;

                    case SeriesType.Line:
                        var updates = (from point in series.Values
                                      where point.x > _cacheMax[chart.Name][series.Name]
                                      select point).ToList();

                        foreach (ChartPoint point in updates)
                        {
                            DateTime time = UnixMsToDateTime(point.x);
                            _cache[chart.Name][series.Name].AddPoint(time.ToOADate(), (double)point.y);
                        }

                        if (updates.Count > 0)
                        {
                            _cacheMax[chart.Name][series.Name] = updates.Last().x;
                        }
                        break;
                }
            }

            //RefreshChart(ref chart);
            var zed = _chartObjects[chart.Name];
            using (Graphics g = zed.CreateGraphics())
            {
                zed.MasterPane.SetLayout(g, PaneLayout.SingleColumn);
                zed.AxisChange();
            }
            zed.Invalidate();
        }

        /// <summary>
        /// Initialize the charts
        /// </summary>
        private void Initialize(Chart resultChart, Series series)
        {
            //Setup Chart:
            var chart = _chartObjects[resultChart.Name];
            chart.Dock = DockStyle.Fill;
            chart.MasterPane.Border.IsVisible = false;
            chart.IsSynchronizeXAxes = true;

            //Based on the QC chart type, draw the panes:
            switch (resultChart.ChartType)
            { 
                case ChartType.Overlay:
                    GraphPane overlayPane = CreateGraphPane(resultChart.Name);
                    CurveItem curveItem = CreateCurveItem(series);
                    overlayPane.CurveList.Add(curveItem);
                    _cache[resultChart.Name][series.Name] = curveItem;
                    chart.MasterPane.Add(overlayPane);
                    break;

                case ChartType.Stacked:
                    GraphPane stackedPane = CreateGraphPane(series.Name);
                    CurveItem stackedCurveItem = CreateCurveItem(series);
                    stackedPane.CurveList.Add(stackedCurveItem);
                    chart.MasterPane.Add(stackedPane);
                    _cache[resultChart.Name][series.Name] = stackedCurveItem;
                    break;
            }

            //Refresh it:
            RefreshChart(ref chart);
        }


        /// <summary>
        /// Create a new curve item
        /// </summary>
        public CurveItem CreateCurveItem(Series series)
        {
            CurveItem item = null;
            switch (series.SeriesType)
            {
                case SeriesType.Candle:
                    JapaneseCandleStickItem candle = new JapaneseCandleStickItem(series.Name, new StockPointList());
                    candle.Stick.IsAutoSize = true;
                    candle.Stick.Color = Color.FromArgb(46, 56, 59);
                    candle.Stick.RisingFill = new Fill(Color.FromArgb(140, 193, 118));
                    candle.Stick.FallingFill = new Fill(Color.FromArgb(184, 44, 12));
                    item = candle;
                    break;

                case SeriesType.Line:
                    LineItem line = new LineItem(series.Name, new DateTimePointList(), Color.DarkBlue, SymbolType.None);
                    item = line;
                    break;
                     
                case SeriesType.Scatter:
                    LineItem scatter = new LineItem(series.Name, new DateTimePointList(), Color.Black, SymbolType.Circle);
                    scatter.Line = new Line();
                    scatter.Line.IsVisible = false;
                    scatter.Symbol.Size = 10;
                    scatter.Symbol.Fill = new Fill(Color.LightGreen);
                    item = scatter;
                    break;
            }
            return item;
        }
        

        /// <summary>
        /// Create a new graph pane
        /// </summary>
        public GraphPane CreateGraphPane(string name)
        {
            GraphPane pane = new GraphPane(); //Here I create a new pane to stack charts?
            FontSpec _fontTitle = new FontSpec("Arial", 12, Color.Black, true, false, false); 
            FontSpec _fontChart = new FontSpec("Arial", 10, Color.Black, false, false, false); 
            FontSpec _fontLegend = new FontSpec("Arial", 10, Color.Black, false, false, false);

            _fontTitle.Border.IsVisible = false; _fontTitle.Border.Color = Color.Transparent;
            _fontChart.Border.IsVisible = false; _fontChart.Border.Color = Color.Transparent;
            _fontLegend.Border.IsVisible = false; _fontLegend.Border.Color = Color.Transparent;

            pane.Border.IsVisible = false;
            pane.Chart.IsRectAuto = true;
            pane.Chart.Border.Width = 1;
            int titleIndex = DrawTitle(ref pane, name, 0.5, 0.15, 13);
            pane.Title.FontSpec = _fontTitle;

            //X axis
            pane.XAxis.Title.IsVisible = false;
            pane.XAxis.Title.Text = "Time";
            pane.XAxis.Title.FontSpec = _fontChart;
            pane.XAxis.Scale.FontSpec = _fontChart;
            pane.XAxis.Color = Color.Black;

            if (_startDate != new DateTime()) pane.XAxis.Scale.Min = _startDate.ToOADate();
            if (_endDate != new DateTime()) pane.XAxis.Scale.Max = _endDate.ToOADate();

            //pane.XAxis.CrossAuto = true;
            pane.XAxis.MajorGrid.IsVisible = true;
            pane.XAxis.Scale.MajorStepAuto = true;
            pane.XAxis.Type = AxisType.Date;
            pane.XAxis.Scale.Format = "yyyy-MM-dd";

            //Y axis
            pane.YAxis.Title.IsVisible = false;
            pane.YAxis.MajorGrid.IsVisible = true;
            pane.Y2Axis.Title.FontSpec = _fontChart;
            pane.Y2Axis.Scale.FontSpec = _fontChart;
            pane.YAxis.Title.FontSpec = _fontChart;
            pane.YAxis.Scale.FontSpec = _fontChart;
            pane.YAxis.Color = Color.Black;
            pane.Chart.Fill = new Fill(Color.White);
            pane.LineType = LineType.Stack;

            //Set the legend:
            pane.Legend.Position = LegendPos.InsideBotRight;
            pane.Legend.FontSpec = _fontLegend;
            pane.Legend.Border.IsVisible = false;
            return pane;
        }
        

        /// <summary>
        /// Create candles from series x,y data:
        /// </summary>
        public List<StockPt> CreateCandle(Series series, double lastUpdate)
        {
            decimal start = 0;
            int day = 24 * 60 * 60;
            decimal dateTime = 0;
            decimal equity = 0;
            List<decimal[]> equityCandles = new List<decimal[]>();
            decimal[] candleToday = new decimal[0];
            var spl = new List<StockPt>();

            if (series.Values.Count > 2000)
            {
                day = 30 * 24 * 60 * 60;
            } else if (series.Values.Count > 365)
            {
                day = 7* 24 * 60 * 60;
            }

            for (int i = 0; i < series.Values.Count; i++)
            {
                if (series.Values[i].x < lastUpdate) continue;

                dateTime = series.Values[i].x;
                equity = series.Values[i].y;
                if (start == 0 || (start + day) < dateTime) start = dateTime;
                if (start == dateTime)
                {
                    if (candleToday.Length > 0) equityCandles.Add(candleToday);
                    candleToday = new decimal[5] { -1, 0, -1, 999999999, 0 };
                    candleToday[0] = start;
                }
                if (candleToday[1] == 0) candleToday[1] = equity;
                if (candleToday[2] < equity) candleToday[2] = equity;
                if (candleToday[3] > equity) candleToday[3] = equity;
                candleToday[4] = equity;
            }
            foreach (var candle in equityCandles)
            {
                DateTime time = UnixMsToDateTime((double)candle[0]);
                StockPt point = new StockPt(time.ToOADate(), Convert.ToDouble(candle[2]), Convert.ToDouble(candle[3]), Convert.ToDouble(candle[1]), Convert.ToDouble(candle[4]), 1000);
                spl.Add(point);
            }
            return spl;
        }


        /// <summary>
        /// Draw a right aligned title - return index of object
        /// </summary>
        private int DrawTitle(ref GraphPane zedPane, string sTitle, double dX = 0.5, double dY = 0.2, float dSize = 48f)
        {
            TextObj oTitle = new TextObj(sTitle, dX, dY);
            oTitle.FontSpec.Border.IsVisible = false;
            oTitle.FontSpec.Size = dSize;
            oTitle.FontSpec.FontColor = Color.Orange;
            oTitle.Location.CoordinateFrame = CoordType.PaneFraction;
            oTitle.ZOrder = ZOrder.E_BehindCurves;
            zedPane.GraphObjList.Add(oTitle);
            return zedPane.GraphObjList.Count - 1;
        }


        /// <summary>
        /// Redraw the chart
        /// </summary>
        /// <param name="zedChart"></param>
        private void RefreshChart(ref ZedGraphControl chart)
        {
            using (Graphics g = chart.CreateGraphics())
            {
                chart.MasterPane.SetLayout(g, PaneLayout.SingleColumn);
                chart.AxisChange();
            }
            chart.Invalidate();
        }

        /// <summary>
        /// Draw the orders to the list view:
        /// </summary>
        /// <param name="trades"></param>
        private void DrawTrades(Dictionary<int, Order> trades)
        {
            int i = 0;
            dataGridViewTrades.Rows.Clear();
            foreach (var trade in trades)
            {
                dataGridViewTrades.Rows.Add();
                dataGridViewTrades.Rows[i].Cells[0].Value = trade.Value.Time.Date;
                dataGridViewTrades.Rows[i].Cells[1].Value = trade.Value.Symbol;
                dataGridViewTrades.Rows[i].Cells[2].Value = trade.Value.Price;
                dataGridViewTrades.Rows[i].Cells[3].Value = trade.Value.Type;
                dataGridViewTrades.Rows[i].Cells[4].Value = trade.Value.Quantity;
                dataGridViewTrades.Rows[i].Cells[5].Value = trade.Value.Direction;
                dataGridViewTrades.Rows[i].Cells[6].Value = trade.Value.Status;
                i++;
            }
        }

        /// <summary>
        /// Draw the stats to the form
        /// </summary>
        /// <param name="stats"></param>
        private void DrawStatistics(Dictionary<string, string> stats)
        {
            int i = 0;
            dataGridViewStats.Rows.Clear();
            foreach (var stat in stats)
            {
                dataGridViewStats.Rows.Add();
                dataGridViewStats.Rows[i].Cells[0].Value = stat.Key;
                dataGridViewStats.Rows[i].Cells[1].Value = stat.Value;                
                i++;
            }
        }


        /// <summary>
        /// Setup the charts as soon as the form loads
        /// </summary>
        private void BacktestChart_Load(object sender, EventArgs e)
        {
            timerRefresh_Tick(new object(), new EventArgs());

            string error;
            IAlgorithm algorithm;
            Loader loader = new Loader();
            if (loader.CreateInstance<IAlgorithm>(QuantConnectPlugin.Directory + QuantConnectPlugin.ProjectID + " - " + QuantConnectPlugin.ProjectName + @"\bin\Debug\" + QuantConnectPlugin.ProjectName + ".dll", "QCAlgorithm", out algorithm, out error))
            {
                try
                {
                    algorithm.Initialize();
                    _startDate = algorithm.StartDate;
                    _endDate = algorithm.EndDate;
                }
                catch
                { }
            }
        }
        

        /// <summary>
        /// Using the new data update the charts:
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timerDrawChart_Tick(object sender, EventArgs e)
        {
            if (this._backtestResultUpdated)
            {
                this._backtestResultUpdated = false;

                //Clear and draw all charts
                if (this._results.Progress == "0%")
                {
                    return;
                }

                DrawCharts(this._results.Results.Charts);
                statusProgress.ProgressBar.Value = Convert.ToInt32(this._results.Progress.Replace("%", ""));

                //If finished draw stats and orders
                if (this._results.Progress == "100%" && this._results.Results.Statistics.Count > 0)
                {
                    statusLabel.Text = "Backtest Completed.";
                    DrawTrades(this._results.Results.Orders);
                    DrawStatistics(this._results.Results.Statistics);
                    this.timerDrawChart.Enabled = false;
                    this.timerDownloadResults.Enabled = false;
                }
            }
        }

    }
}
