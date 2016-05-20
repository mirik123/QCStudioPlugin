/*
* Mark Babayev (https://github.com/mirik123) - User Control for ToolWindowPane
 * The desing and the idea are based on the original QuantConnect client plugin:
 * https://github.com/QuantConnect/QCStudioPlugin
*/

using QuantConnect.QCPlugin;
using QuantConnect.QCStudioPlugin.Actions;
using QuantConnect.RestAPI.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuantConnect.QCStudioPlugin.Forms
{
    public partial class QCClientControl : UserControl
    {
        private string _backtestId = "";
        private DrawChartsFactory _drawChartActions;

        public QCClientControl()
        {
            _drawChartActions = new DrawChartsFactory();
            InitializeComponent();
        }

        public void InitBacktestResults(string backtestId)
        {
            this.backtestId = backtestId;

            refreshBacktest.Enabled = true;
        }

        private async void refreshBacktest_Tick(object sender, EventArgs e)
        {
            statusLabel.Text = "Updating Results...";

            var _results = await QCStudioPluginActions.GetBacktestResults(_backtestId);

            if (_results == null)
            {
                refreshBacktest.Enabled = false;
                statusLabel.Text = "Backtest Failed.";
                return;
            }

            if (_results.Progress == "0%" || _results.Progress == "")
            {
                return;
            }

            var zedgraphs = _drawChartActions.DrawCharts(_results.Results.Charts);
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

            statusProgress.ProgressBar.Value = Convert.ToInt32(_results.Progress.Replace("%", ""));

            //If finished draw stats and orders
            if (_results.Progress == "100%")// && _results.Results.Statistics.Count > 0)
            {
                refreshBacktest.Enabled = false;
                statusLabel.Text = "Backtest Completed.";

                dataGridViewTrades.DataSource = _results.Results.Orders;
                dataGridViewStats.DataSource = _results.Results.Statistics;
            }
        }
    }
}
