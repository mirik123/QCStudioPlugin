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
        private string browserData = null;

        public ChartControl()
        {
            InitializeComponent();
        }

        public void Run(string url)
        {
            if (WBEmulator.IsWindows)
                WBEmulator.ValidateAndUpdateBrowserEmulation();
            
            Browser.Navigate(string.Format(url, 0));

            Task<PacketBacktestResult>.Run(() =>
            { 
                return GetBacktestResultsCallback(); 
            })
            .ContinueWith(x =>
            {
                var _results = x.Result;            
                var dateFormat = "yyyy-MM-dd HH:mm:ss";

                try
                {
                    dynamic final = new
                    {
                        dtPeriodStart = _results.PeriodStart.ToString(dateFormat),
                        dtPeriodFinished = _results.PeriodFinish.AddDays(1).ToString(dateFormat),
                        oResultData = new
                        {
                            version = "3",
                            results = _results.Results,
                            statistics = _results.Results.Statistics,
                            iTradeableDates = 1,
                            ranking = (object)null
                        }
                    };

                    browserData = JsonConvert.SerializeObject(final);

                    Browser.Navigate(string.Format(url, 1));
                }
                catch (Exception ex)
                {
                    QCPluginUtilities.OutputCommandString(ex.ToString(), QCPluginUtilities.Severity.Error);
                }
            }, TaskScheduler.FromCurrentSynchronizationContext());     
        }

        private void Browser_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            if (Browser.Document == null) return;
            Browser.Document.Window.Error += (object s2, HtmlElementErrorEventArgs e2) =>
            {
                var err = string.Format("JS ERROR: Url=\"{0}\", Line={0}, Description=\"{0}\", ", e2.Url, e2.LineNumber, e2.Description);
                QCPluginUtilities.OutputCommandString(err, QCPluginUtilities.Severity.Error);
            };

            if (browserData == null) return;
            Browser.Document.InvokeScript("eval", new object[] { "window.jnBacktest = JSON.parse('" + browserData + "');" });
            Browser.Document.InvokeScript("eval", new object[] { "$.holdReady(false);" });
        }
    }
}
