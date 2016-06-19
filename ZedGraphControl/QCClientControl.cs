/*
* QUANTCONNECT.COM - Democratizing Finance, Empowering Individuals,
* QuantConnect Visual Studio Plugin
*/

using QuantConnect.Orders;
using QuantConnect.QCPlugin;
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
    public partial class QCClientControl : ChartControl
    {
        public QCClientControl()
        {
            InitializeComponent();
        }

        public override void Initialize(params string[] args)
        { 
        }

        public override void Run(PacketBacktestResult _results)
        {
            try
            {
                dataGridViewTrades.DataSource = _results.Results.Orders.Select(itm => new
                {
                    DateTime = itm.Value.Time,
                    Symbol = itm.Value.Symbol.Value,
                    Price = itm.Value.Price,
                    Type = (OrderType)itm.Value.Type,
                    Quantity = itm.Value.Quantity,
                    Operation = itm.Value.Quantity > 0 ? OrderDirection.Buy : itm.Value.Quantity < 0 ? OrderDirection.Sell : OrderDirection.Hold,
                    Status = (OrderStatus)itm.Value.Status
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
                //QCPluginUtilities.OutputCommandString(ex.ToString(), QCPluginUtilities.Severity.Error);
            }                     
        }
    }
}
