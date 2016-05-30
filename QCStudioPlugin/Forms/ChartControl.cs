/*
* QUANTCONNECT.COM - Democratizing Finance, Empowering Individuals,
* QuantConnect Visual Studio Plugin
*/

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
        public Func<Task<PacketBacktestResult>> GetBacktestResultsCallback;
        private string browserData = "{}";
        
        public ChartControl()
        {
            InitializeComponent();
        }

        public async void Run(string url)
        {
            var _results = await GetBacktestResultsCallback();
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
            //Generate JSON:
            var dateFormat = "yyyy-MM-dd HH:mm:ss";

            dynamic final = new
            {
                dtPeriodStart = packet.PeriodStart.ToString(dateFormat),
                dtPeriodFinished = packet.PeriodFinish.AddDays(1).ToString(dateFormat),
                oResultData = new 
                { 
                    version = "3",
                    results = packet.Results,
                    statistics = packet.Results.Statistics,
                    iTradeableDates = 1,
                    ranking = (object)null
                }
            };

            browserData = JsonConvert.SerializeObject(final);
            
            Browser.Navigate(url);            
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
