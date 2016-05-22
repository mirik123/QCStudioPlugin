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
        private string browserData = "{}";
        
        public ChartControl()
        {
            InitializeComponent();
        }

        public async void Run(string url, string backtestId)
        {
            var _results = await QCStudioPluginActions.GetBacktestResults(backtestId);
            MessagingOnBacktestResultEvent(_results, url);
        }

        private void ChartControl_Load(object sender, EventArgs e)
        {
            if (WBEmulator.IsWindows && !WBEmulator.IsBrowserEmulationSet())
            {
                WBEmulator.SetBrowserEmulationVersion();
            }
        }

        private void MessagingOnBacktestResultEvent(PacketBacktestResult packet, string url)
        {
            if (packet.Progress != "100%") return;

            //Generate JSON:
            var jObj = new JObject();
            var dateFormat = "yyyy-MM-dd HH:mm:ss";
            
            dynamic final = jObj;
            final.dtPeriodStart = packet.PeriodStart.ToString(dateFormat);
            final.dtPeriodFinished = packet.PeriodFinish.AddDays(1).ToString(dateFormat);
            
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
                QCPluginUtilities.OutputCommandString("STATISTICS:: " + pair.Key + " " + pair.Value, QCPluginUtilities.Severity.Info);
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
