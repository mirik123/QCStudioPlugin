/*
* QUANTCONNECT.COM - Democratizing Finance, Empowering Individuals,
* QuantConnect Visual Studio Plugin
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
        public Func<Task<PacketBacktestResult>> GetBacktestResultsCallback;

        public QCClientControl()
        {
            InitializeComponent();
        }

        public async void Run()
        {
            var _results = await GetBacktestResultsCallback();

            //Time.Date; Symbol; Price; Type; Quantity; Direction; Status;
            dataGridViewTrades.DataSource = _results.Results.Orders;


            dataGridViewStats.DataSource = _results.Results.Statistics;

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
    }
}
