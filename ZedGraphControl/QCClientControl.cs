/*
* QUANTCONNECT.COM - Democratizing Finance, Empowering Individuals,
* QuantConnect Visual Studio Plugin
*/


using Newtonsoft.Json;
using QCInterfaces;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ZedGraphUIControl
{
    public partial class QCClientControl : ChartControl
    {
        public QCClientControl()
        {
            InitializeComponent();
        }

        public override void Run(string rawData)
        {
            try
            {
                var _results = JsonConvert.DeserializeObject<BacktestResultPacket>(rawData);
                CalcPeriods(_results);
                dataGridViewTrades.DataSource = _results.Results.Orders.Select(itm => new
                {
                    DateTime = itm.Value.Time.Value,
                    Symbol = itm.Value.Symbol.Value.Value,
                    Price = itm.Value.Price.Value,
                    Type = (OrderType)itm.Value.Type.Value,
                    Quantity = itm.Value.Quantity.Value,
                    Operation = itm.Value.Quantity.Value > 0 ? OrderDirection.Buy : itm.Value.Quantity.Value < 0 ? OrderDirection.Sell : OrderDirection.Hold,
                    Status = (OrderStatus)itm.Value.Status.Value
                }).ToArray();

                dataGridViewStats.DataSource = _results.Results.Statistics.Select(itm => new
                {
                    Name = itm.Key,
                    Value = itm.Value
                }).ToArray();

                var _drawChartActions = new DrawChartsFactory();
                var zedgraphs = _drawChartActions.DrawCharts(_results.Results.Charts, _results.PeriodStart, _results.PeriodFinish);
                foreach (var zed in zedgraphs)
                {
                    if (!tabCharts.TabPages.ContainsKey(zed.Key))
                    {
                        //Create the tab and zedgraph control:
                        var tab = new TabPage(zed.Key);
                        tab.Controls.Add(zed.Value);
                        tabCharts.TabPages.Add(tab);
                    }
                }
            }
            catch (Exception ex)
            {
                Logger(ex.ToString());
            }                     
        }

        private static void CalcPeriods(BacktestResultPacket packet)
        {
            if (packet.PeriodStart > DateTime.MinValue && packet.PeriodFinish > DateTime.MinValue) return;
            
            long _startDate = long.MaxValue, _endDate = -1;
            foreach (var chart in packet.Results.Charts.Values)
            {
                foreach (var series in chart.Series.Values)
                {
                    if (series.Values.Count == 0) continue;

                    var mindt = series.Values.Min(x => x.x);
                    var maxdt = series.Values.Max(x => x.x);
                    if (_startDate > mindt) _startDate = mindt;
                    if (_endDate < maxdt) _endDate = maxdt;
                }
            }

            if (_endDate < 0) return;

            packet.PeriodStart = new DateTime(1970, 1, 1, 0, 0, 0, 0).AddSeconds(_startDate);
            packet.PeriodFinish = new DateTime(1970, 1, 1, 0, 0, 0, 0).AddSeconds(_endDate);
        }
    }
}
