/*
* QUANTCONNECT.COM - Democratizing Finance, Empowering Individuals,
* QuantConnect Visual Studio Plugin
*/

/**********************************************************
* USING NAMESPACES
**********************************************************/
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
using EnvDTE100;
using System.Threading;

namespace QuantConnect.QCPlugin
{
    public partial class FormLoadBacktest : Form
    {
        /// Load this Result 
        public string LoadResult = "";

        /// <summary>
        /// Create the form:
        /// </summary>
        public FormLoadBacktest()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Set a callback and show the login form.
        /// </summary>
        /// <param name="callback"></param>
        public void ShowLogin(Action callback)
        {
            FormLogin form = new FormLogin();
            form.SetCallBacks(callback);
            form.Show();
        }

        /// <summary>
        /// Launch a backtest.
        /// </summary>
        public void Backtest(bool retry = false)
        {
            
            int _projectID = QuantConnectPlugin.ProjectID;

            if (QuantConnectPlugin.LocalCompile())
            {
                progressBar.Value = 33;
                if(!retry)
                {
                    labelMessage.Text = "Building on QuantConnect...";
                }                

                // Compile on QC
                Async.Add(new APIJob(APICommand.Compile, (compile, errors) =>
                {
                    // Handle login and API errors:
                    switch (QuantConnectPlugin.HandleErrors(errors))
                    {
                        // Handle project specific actions with a login error:
                        case APIErrors.NotLoggedIn:
                            this.SafeInvoke(d => d.ShowLogin(() => { FormLoadBacktest form = new FormLoadBacktest(); form.StartPosition = FormStartPosition.CenterScreen; form.Show(); }));
                            this.SafeInvoke(d => d.Close());
                            return;
                        case APIErrors.CompileTimeout:
                            labelMessage.SafeInvoke(d => d.Text = "Building timedout, retrying...");
                            this.SafeInvoke(d => d.Backtest(true));
                            return;
                        case APIErrors.CompileError:
                            MessageBox.Show("There was a build error: " + errors[0], "QuantConnect Build Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            this.SafeInvoke(d => d.Close());
                            return;
                    }
                    progressBar.SafeInvoke(d => d.Value = 66);                    
                    labelMessage.SafeInvoke(d => d.Text = "Issuing Backtest...");

                    var packet = (PacketCompile)compile;
                    if (!packet.Success)
                    {
                        pictureError.SafeInvoke(d => d.Visible = true);
                        labelMessage.SafeInvoke(d=>d.Text = "QuantConnect build failed. Please see project at QuantConnect.com");
                        return;
                    }

                    //Sending Backtest...
                    var backtestId = QuantConnectPlugin.GetBacktestID(packet, _projectID);
                    if (backtestId == "")
                    {
                        return;
                    }

                    //Got Id, Open Backtest Form:
                    progressBar.SafeInvoke(d => d.Value = 99);
                    labelMessage.SafeInvoke(d => d.Text = "Backtest Sent Successfully.");
                    pictureCheck.SafeInvoke(d => d.Visible = true);
                    this.SafeInvoke(d => d.LoadResult = backtestId);
                }, _projectID));
            }
            else
            {
                pictureError.Visible = true;
                labelMessage.Text = "Local build failed. Please see Visual Studio";
            }
        }

        /// <summary>
        /// Send backtest
        /// </summary>
        private void StartBacktest_Load(object sender, EventArgs e)
        {
            Backtest();
        }

        /// <summary>
        /// Launch the backtest result form.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timer_Tick(object sender, EventArgs e)
        {
            if (this.LoadResult != "")
            {
                FormBacktestChart form = new FormBacktestChart();
                form.SetBacktestId(LoadResult);
                form.Show();
                this.Close();                
            }

            if (QuantConnectPlugin.RateLimitReached)
            {
                QuantConnectPlugin.RateLimitReached = false;
                QuantConnectPlugin.ShowRateLimit();
                this.Close();
            }
        }
    }
}
