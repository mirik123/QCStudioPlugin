using QuantConnect.QCPlugin;
using QuantConnect.QCStudioPlugin.Actions;
using QuantConnect.RestAPI.Models;
using System;
using System.Collections.Generic;
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

        /// <summary>
        /// Replace invalid characters with empty strings. 
        /// </summary>
        /// <param name="strIn"></param>
        /// <returns></returns>
        public static string CleanInput(string strIn)
        {
            try
            {
                return Regex.Replace(strIn, @"[^\w\.-]", "", RegexOptions.None, TimeSpan.FromSeconds(1.5));
            }
            catch (RegexMatchTimeoutException)
            {
                return String.Empty;
            }
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

        private async void mnBacktest_Click(object sender, ToolStripItemClickedEventArgs e)
        {
            if (dgrProjects.SelectedRows.Count == 0) return;
            var selproj = dgrProjects.SelectedRows[0].DataBoundItem as CombinedProject;
            if (selproj.Id == 0)
                return;

            var sourceControl = e.ClickedItem;
            if (sourceControl.Name == "mnRefreshBacktests")
            {
                dgrBacktests.DataSource = await QCStudioPluginActions.GetBacktestList(selproj.Id);
            }
            else
            {
                if (dgrBacktests.SelectedRows.Count == 0) return;
                var selbacktest = dgrBacktests.SelectedRows[0].DataBoundItem as BacktestSummary;
                if (string.IsNullOrEmpty(selbacktest.BacktestId) || selbacktest.BacktestId == "0")
                    return;

                switch (sourceControl.Name)
                {
                    case "mnLoadBacktest":
                        _backtestId = selbacktest.BacktestId;
                        refreshBacktest.Enabled = true;

                        break;
                    case "mnDeleteBacktest":
                        await QCStudioPluginActions.DeleteBacktest(selbacktest.BacktestId);
                        var delrow = dgrBacktests.Rows.Cast<DataGridViewRow>().FirstOrDefault(x => (x.DataBoundItem as BacktestSummary).BacktestId == selbacktest.BacktestId);
                        if (delrow != null) dgrBacktests.Rows.Remove(delrow);

                        break;
                }
            }
        }

        private async void mnProjects_Click(object sender, ToolStripItemClickedEventArgs e)
        {
            var sourceControl = e.ClickedItem;
            switch (sourceControl.Name)
            {
                case "mnCreateProject":
                    string projectName = QCPluginUtilities.GetStartupProjectName();
                    projectName = Microsoft.VisualBasic.Interaction.InputBox("Enter new project name", QCPluginUtilities.AppTitle, projectName);
                    projectName = CleanInput(projectName);

                    if (string.IsNullOrEmpty(projectName))
                    {
                        QCPluginUtilities.OutputCommandString("Project name cannot be empty.");
                        return;
                    }

                    await QCStudioPluginActions.CreateProject(projectName);
                    mnRefreshProjects.PerformClick();

                    break;
                case "mnLogin":
                    QCStudioPluginActions.Login();

                    break;
                case "mnLogout":
                    QCStudioPluginActions.Logout();

                    break;
                case "mnRefreshProjects":
                    dgrProjects.DataSource = await QCStudioPluginActions.GetProjectList();
                    var firstrow = dgrProjects.Rows.Cast<DataGridViewRow>().FirstOrDefault(x => (x.DataBoundItem as CombinedProject).Id > 0);
                    if (firstrow != null) firstrow.Selected = true;

                    mnRefreshBacktests.PerformClick();

                    break;
                default:
                    if (dgrProjects.SelectedRows.Count == 0) return;
                    var selproj = dgrProjects.SelectedRows[0].DataBoundItem as CombinedProject;
                    if (selproj.Id == 0)
                        return;

                    switch (sourceControl.Name)
                    {
                        case "mnDeleteProject":
                            await QCStudioPluginActions.DeleteProject(selproj.Id);
                            var delrow = dgrProjects.Rows.Cast<DataGridViewRow>().FirstOrDefault(x => (x.DataBoundItem as CombinedProject).Id == selproj.Id);

                            if (!string.IsNullOrEmpty(selproj.LocalProjectName))
                                QCPluginUtilities.SetProjectID(0, selproj.LocalProjectName);
                            else if (delrow != null)
                                dgrProjects.Rows.Remove(delrow);

                            break;
                        case "mnUploadProject":
                            await QCStudioPluginActions.UploadProject(selproj.Id, selproj.LocalProjectPath);

                            break;
                        case "mnDownloadProject":
                            await QCStudioPluginActions.DownloadProject(selproj.Id, selproj.LocalProjectPath);

                            break;
                        case "mnUseProjectID":
                            var projects = dgrProjects.DataSource as List<CombinedProject>;
                            var cloudproj = projects.Where(x => string.IsNullOrEmpty(x.LocalProjectName)).ToArray();
                            var localproj = projects.Where(x => x.Id == 0).ToArray();
                            if (localproj.Length == 0 || cloudproj.Length == 0)
                            {
                                QCPluginUtilities.OutputCommandString("No orphaned projects found on both cloud and local side.");
                                return;
                            }

                            var win = new ConnectQCID();
                            win.cmbCloud.Items.AddRange(cloudproj);
                            win.cmbLocal.Items.AddRange(localproj);
                            var dlgres = win.ShowDialog();

                            if (dlgres == DialogResult.OK)
                            {
                                var selID = win.cmbCloud.SelectedItem as CombinedProject;
                                var selName = win.cmbLocal.SelectedItem as CombinedProject;

                                QCPluginUtilities.SetProjectID(selID.Id, selName.LocalProjectName);
                                mnRefreshProjects.PerformClick();
                            }

                            break;
                        case "mnCompileProject":
                            string backtestName = QCPluginUtilities.GetStartupProjectName();
                            backtestName = Microsoft.VisualBasic.Interaction.InputBox("Enter new backtest name", QCPluginUtilities.AppTitle, backtestName);
                            backtestName = CleanInput(backtestName);

                            if (string.IsNullOrWhiteSpace(backtestName))
                            {
                                QCPluginUtilities.OutputCommandString("Backtest name cannot be empty.");
                                return;
                            }

                            await QCStudioPluginActions.CreateBacktest(selproj.Id, "???");

                            break;
                    }

                    break;
            }
        }

        private void dgrProjects_DoubleClick(object sender, EventArgs e)
        {
            mnRefreshBacktests.PerformClick();
        }

        private void dgrBacktests_DoubleClick(object sender, EventArgs e)
        {
            mnLoadBacktest.PerformClick();
        }
    }
}
