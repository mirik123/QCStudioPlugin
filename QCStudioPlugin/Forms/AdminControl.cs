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
    public partial class AdminControl : UserControl
    {
        public AdminControl()
        {           
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

        private async void BacktestMenu_Click(object sender, ToolStripItemClickedEventArgs e)
        {
            if (dgrProjects.SelectedRows.Count == 0) return;
            var selproj = dgrProjects.SelectedRows[0].DataBoundItem as CombinedProject;
            if (selproj.Id == 0)
                return;

            var sourceControl = e.ClickedItem;
            if (sourceControl.Name == "mnRefreshBacktests")
            {
                dgrBacktests.DataSource = null;
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
                    case "mnLoadBacktestJS":
                        await QCPluginUtilities.ShowBacktestJSRemote(selbacktest.BacktestId);

                        break;
                    case "mnLoadBacktestZED":
                        await QCPluginUtilities.ShowBacktestZEDRemote(selbacktest.BacktestId);

                        break;
                    case "mnDeleteBacktest":
                        await QCStudioPluginActions.DeleteBacktest(selbacktest.BacktestId);

                        RefreshBacktestsMenu.PerformClick();

                        break;
                }
            }
        }

        private async void ProjectsMenu_Click(object sender, ToolStripItemClickedEventArgs e)
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
                        QCPluginUtilities.OutputCommandString("Project name cannot be empty.", QCPluginUtilities.Severity.Error);
                        return;
                    }

                    await QCStudioPluginActions.CreateProject(projectName);
                    RefreshProjectsMenu.PerformClick();

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

                    RefreshBacktestsMenu.PerformClick();

                    break;
                case "mnConnectProjectID":
                        var projects = dgrProjects.DataSource as List<CombinedProject>;
                        var cloudproj = projects.Where(x => string.IsNullOrEmpty(x.LocalProjectName)).ToArray();
                        var localproj = projects.Where(x => x.Id == 0).ToArray();
                        if (localproj.Length == 0 || cloudproj.Length == 0)
                        {
                            QCPluginUtilities.OutputCommandString("No orphaned projects found on both cloud and local side.", QCPluginUtilities.Severity.Error);
                            return;
                        }

                        var win = new ConnectQCID();
                        win.cmbCloud.Items.AddRange(cloudproj);
                        win.cmbLocal.Items.AddRange(localproj);

                        if (dgrProjects.SelectedRows.Count > 0)
                        {
                            var selproj2 = dgrProjects.SelectedRows[0].DataBoundItem as CombinedProject;
                            win.cmbCloud.SelectedItem = selproj2;
                            win.cmbLocal.SelectedItem = selproj2;
                        }

                        var dlgres = win.ShowDialog();

                        if (dlgres == DialogResult.OK)
                        {
                            var selID = win.cmbCloud.SelectedItem as CombinedProject;
                            var selName = win.cmbLocal.SelectedItem as CombinedProject;

                            QCPluginUtilities.SetProjectID(selID.Id, selName.uniqueName);
                            RefreshProjectsMenu.PerformClick();
                        }

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
                            
                            QCPluginUtilities.SetProjectID(0, selproj.uniqueName);
                            RefreshProjectsMenu.PerformClick();

                            break;
                        case "mnUploadProject":
                            if (string.IsNullOrEmpty(selproj.LocalProjectPath)) return;
                            await QCStudioPluginActions.UploadProject(selproj.Id, selproj.CloudProjectName, selproj.LocalProjectPath);

                            break;
                        case "mnDownloadProject":

                            await QCStudioPluginActions.DownloadProject(selproj.Id, selproj.CloudProjectName, selproj.LocalProjectName, selproj.LocalProjectPath);

                            break;                        
                        case "mnDisconnectProjectID":
                            QCPluginUtilities.SetProjectID(0, selproj.uniqueName);
                            RefreshProjectsMenu.PerformClick();

                            break;
                        case "mnCompileProject":
                            string backtestName = QCPluginUtilities.GetStartupProjectName();
                            backtestName = Microsoft.VisualBasic.Interaction.InputBox("Enter new backtest name", QCPluginUtilities.AppTitle, backtestName);
                            backtestName = CleanInput(backtestName);

                            if (string.IsNullOrWhiteSpace(backtestName))
                            {
                                QCPluginUtilities.OutputCommandString("Backtest name cannot be empty.", QCPluginUtilities.Severity.Error);
                                return;
                            }

                            var isfinished = false;
                            var cnt = 0;
                            while (!isfinished && cnt++ < 5)
                            {
                                isfinished = await QCStudioPluginActions.CreateBacktest(selproj.Id, backtestName);
                            }

                            break;
                    }

                    break;
            }
        }

        private void dgrProjects_DoubleClick(object sender, EventArgs e)
        {
            RefreshBacktestsMenu.PerformClick();
        }

        private void dgrBacktests_DoubleClick(object sender, EventArgs e)
        {
            LoadBacktestZEDMenu.PerformClick();
        }

        private void BacktestMenu_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {
            var isProjSelected = false;
            if (dgrProjects.SelectedRows.Count > 0)
            {
                var selproj = dgrProjects.SelectedRows[0].DataBoundItem as CombinedProject;
                if (selproj.Id > 0)
                    isProjSelected = true;
            }

            var isSelected = false;
            if (dgrBacktests.SelectedRows.Count > 0)
            {
                var selbacktest = dgrBacktests.SelectedRows[0].DataBoundItem as BacktestSummary;
                if (!string.IsNullOrEmpty(selbacktest.BacktestId) && selbacktest.BacktestId != "0")
                    isSelected = true;
            }

            foreach (ToolStripMenuItem itm in BacktestMenu.Items)
            {
                itm.Enabled = isProjSelected && (isSelected || itm.Tag.ToString() == "1");
            }
        }

        private void ProjectsMenu_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {
            var isSelected = false;
            var hasLocalProject = false;

            if (dgrProjects.SelectedRows.Count > 0)
            {
                var selproj = dgrProjects.SelectedRows[0].DataBoundItem as CombinedProject;
                if (selproj.Id > 0)
                    isSelected = true;

                if(!string.IsNullOrEmpty(selproj.LocalProjectName))
                    hasLocalProject = true;
            }

            foreach (ToolStripMenuItem itm in ProjectsMenu.Items)
            {
                itm.Enabled = isSelected || itm.Tag.ToString() == "1";
            }

            if (!hasLocalProject)
                UploadProjectMenu.Enabled = false;
        }
    }
}
