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
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using QuantConnect.RestAPI.Models;
using QCTerminalControl.Properties;

namespace QCTerminalControl
{
    public partial class JSChartControl : ChartControl
    {        
        private string browserData = null;
        private string url;

        public JSChartControl()
        {
            InitializeComponent();
        }

        public static string GetTerminalUrl(string backtestId, string UserID, string AuthToken, int ProjectId = 0, bool liveMode = false)
        {
            var url = "";
            var embedPage = liveMode ? "embeddedLive" : "embedded";

            url = string.Format(
                "https://www.quantconnect.com/terminal/{0}?user={1}&token={2}&pid={3}&version={4}&holdReady={5}&bid={6}",
                embedPage, UserID, AuthToken, ProjectId, Resources.QuantConnectVersion, "{0}", backtestId);

            return url;
        }

        public override void Initialize(params string[] args)
        {
            if (WBEmulator.IsWindows)
            {
                WBEmulator.Logger = Logger;
                WBEmulator.ValidateAndUpdateBrowserEmulation();
            }

            url = GetTerminalUrl(args[0], args[1], args[2]);
            Browser.Navigate(string.Format(url, 0));
        }

        public override void Run(PacketBacktestResult _results)
        {
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
                Logger(ex.ToString());
            }
        }

        private void Browser_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            if (Browser.Document == null) return;
            Browser.Document.Window.Error += (object s2, HtmlElementErrorEventArgs e2) =>
            {
                var err = string.Format("JS ERROR: Url=\"{0}\", Line={0}, Description=\"{0}\", ", e2.Url, e2.LineNumber, e2.Description);
                Logger(err);
            };

            if (browserData == null) return;
            Browser.Document.InvokeScript("eval", new object[] { "window.jnBacktest = JSON.parse('" + browserData + "');" });
            Browser.Document.InvokeScript("eval", new object[] { "$.holdReady(false);" });
        }
    }
}
