using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using QuantConnect.QCPlugin;
using QuantConnect.QCStudioPlugin.Actions;
using QuantConnect.RestAPI.Models;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using QuantConnect.Views;

namespace QuantConnect.QCStudioPlugin.Forms
{
    public partial class ChartControl : UserControl
    {
        private string url = "";
        private string backtestId = "";
        private string browserData = "{}";
        
        public ChartControl()
        {
            InitializeComponent();
        }

        public void InitBacktestResults(string url, string backtestId)
        {
            //Save off the messaging event handler we need:
            this.url = url;
            this.backtestId = backtestId;

            refreshBacktest.Enabled = true;
        }

        private async void refreshBacktest_Tick(object sender, EventArgs e)
        {
            var _results = await QCStudioPluginActions.GetBacktestResults(backtestId);

            if (_results == null)
            {
                refreshBacktest.Enabled = false;
                FormToolStripStatusLabel.Text = "Backtest Failed.";
                return;
            }

            if (_results.Progress == "0%" || _results.Progress == "")
            {
                return;
            }

            FormToolStripProgressBar.ProgressBar.Value = Convert.ToInt32(_results.Progress.Replace("%", ""));

            //If finished draw stats and orders
            if (_results.Progress == "100%")// && _results.Results.Statistics.Count > 0)
            {
                refreshBacktest.Enabled = false;
                FormToolStripStatusLabel.Text = "Backtest Completed.";

                MessagingOnBacktestResultEvent(_results);
            }
        }

        private void ChartControl_Load(object sender, EventArgs e)
        {
            if (WBEmulator.IsWindows && !WBEmulator.IsBrowserEmulationSet())
            {
                WBEmulator.SetBrowserEmulationVersion();
            }
        }

        private Tuple<DateTime, DateTime> CalcPeriods(PacketBacktestResult packet)
        {
            long _startDate = long.MaxValue, _endDate = -1;

            foreach (var chart in packet.Results.Charts.Values)
            {
                foreach (Series series in chart.Series.Values)
                {
                    if (series.Values.Count == 0) continue;

                    var mindt = series.Values.Min(x => x.x);
                    var maxdt = series.Values.Max(x => x.x);
                    if (_startDate > mindt) _startDate = mindt;
                    if (_endDate < maxdt) _endDate = maxdt;
                } 
            }

            if (_endDate < 0) return null;

            DateTime startDate = new DateTime(1970, 1, 1, 0, 0, 0, 0).AddSeconds(_startDate);
            DateTime endDate = new DateTime(1970, 1, 1, 0, 0, 0, 0).AddSeconds(_endDate);

            return new Tuple<DateTime, DateTime>(startDate, endDate);
        }

        private void MessagingOnBacktestResultEvent(PacketBacktestResult packet)
        {
            if (packet.Progress != "100%") return;

            //Generate JSON:
            var jObj = new JObject();
            var dateFormat = "yyyy-MM-dd HH:mm:ss";
            
            dynamic final = jObj;
            var period = CalcPeriods(packet);
            final.dtPeriodStart = period.Item1.ToString(dateFormat);
            final.dtPeriodFinished = period.Item2.AddDays(1).ToString(dateFormat);
            
            dynamic resultData = new JObject();
            resultData.version = "3";
            resultData.results = JObject.FromObject(packet.Results);
            resultData.statistics = JObject.FromObject(packet.Results.Statistics);
            resultData.iTradeableDates = 1;
            resultData.ranking = null;
            
            final.oResultData = resultData;
            browserData = JsonConvert.SerializeObject(final);
            
            Browser.Navigate(url);

            foreach (var pair in packet.Results.Statistics)
            {
                LogTextBox.AppendText("STATISTICS:: " + pair.Key + " " + pair.Value, Color.Black);
            }
        }

        private void Browser_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            //MONO WEB BROWSER RESULT SET:
            if (Browser.Document == null) return;
            Browser.Document.InvokeScript("eval", new object[] { "window.jnBacktest = JSON.parse('" + browserData + "');" });
            Browser.Document.InvokeScript("eval", new object[] { "$.holdReady(false)" });
        }
    }
}
