/*
* QUANTCONNECT.COM - Democratizing Finance, Empowering Individuals,
* QuantConnect Visual Studio Plugin
*/

using QuantConnect.QCStudioPlugin.Forms;
using Newtonsoft.Json;
using QuantConnect.QCPlugin;
using QuantConnect.RestAPI;

using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using System.Threading;
using Newtonsoft.Json.Linq;
using QuantConnect.QCStudioPlugin.Properties;
using QuantConnect.RestAPI.Models;
using System.ComponentModel;



namespace QuantConnect.QCStudioPlugin.Actions
{
    public static class QCStudioPluginActions
    {
        private static API api = new API();

        public static string UserID { get { return api.UserID; } }
        public static string AuthToken { get { return api.AuthToken; } }

        public async static void Login()
        {
            try
            {
                await Authenticate();
            }
            catch (Exception ex)
            {
                QCPluginUtilities.OutputCommandString("Authentication error: " + ex.ToString(), QCPluginUtilities.Severity.Error);
            }
        }

        public async static Task Authenticate()
        {
            if(!api.IsAuthenticated)
            {
                var user = new User { Email = "", Password = "", AuthToken = "", UserID = "" };
                var credentialFile = Path.Combine(QCPluginUtilities.InstallPath, "credentials.config");

                QCPluginUtilities.OutputCommandString("Authenticating QC user...", QCPluginUtilities.Severity.Info);
                if (File.Exists(credentialFile))
                {
                    var userData = File.ReadAllText(credentialFile);
                    var jsonUser = JsonConvert.DeserializeAnonymousType<User>(userData, user);

                    user.Email = Encrypter.DecryptString(jsonUser.Email);
                    user.Password = Encrypter.DecryptString(jsonUser.Password);
                    user.UserID = Encrypter.DecryptString(jsonUser.UserID);
                    user.AuthToken = Encrypter.DecryptString(jsonUser.AuthToken);
                }

                try
                {
                    await api.Authenticate(user.Email, user.Password, user.UserID, user.AuthToken);
                }
                catch(Exception ex)
                {
                    QCPluginUtilities.OutputCommandString("Authentication error: " + ex.ToString(), QCPluginUtilities.Severity.Error);
                    QCPluginUtilities.OutputCommandString("Failed to authenticate. Enter credentials manually.", QCPluginUtilities.Severity.Info);

                    bool remember = false;
                    var win = new FormLogin(user.Email, user.Password, user.UserID, user.AuthToken);
                    win.SuccessCallback = (email2, pass2, uid2, authtoken2, remember2) =>
                    {
                        user.Email = Encrypter.EncryptString(email2);
                        user.Password = Encrypter.EncryptString(pass2);
                        user.UserID = Encrypter.EncryptString(uid2);
                        user.AuthToken = Encrypter.EncryptString(authtoken2);
                        remember = remember2;

                        return api.Authenticate(email2, pass2, uid2, authtoken2);
                    };

                    win.ShowDialog();

                    if (!api.IsAuthenticated)
                        throw new Exception("User authentication failed");
                    else if (remember)
                    {
                        var jsonUser = JsonConvert.SerializeObject(user);

                        if (File.Exists(credentialFile))
                            File.Delete(credentialFile);

                        File.WriteAllText(credentialFile, jsonUser);
                    }
                }

                QCPluginUtilities.OutputCommandString("User authenticated successfuly.", QCPluginUtilities.Severity.Info);
            }
        }

        public async static Task UploadProject(int ProjectID, string cloudProjectName, string ProjectDir)
        {
            try
            {
                await Authenticate();

                var fileList = new Dictionary<string, string>();

                SavetoCloud_AddSubdirectoryToFileList(ProjectDir, ProjectDir, fileList);
                var parsed_filesList = fileList.Select(x => new QCFile(x.Key, x.Value)).ToList();
                var cloudfiles = await api.ProjectFiles(ProjectID);

                //FULL OUTER JOIN !!!
                var alookup = parsed_filesList.Select(x => new KeyValuePair<string, dynamic>(x.Name, x));
                var blookup = cloudfiles.Files.Select(x => new KeyValuePair<string, dynamic>(x.Name, x));
                var combolist = QCPluginUtilities.FullOuterJoin<string>(alookup, blookup);
                var rows = combolist.Select(x => {
                    var row = new DataGridViewRow();
                    var chkaction = new DataGridViewCheckBoxCell() { Value = false, Tag = x.Item2 };
                    
                    row.Cells.Add(new DataGridViewTextBoxCell() { Value = x.Item3 != null ? x.Item3.Name : "" });
                    row.Cells.Add(chkaction);
                    row.Cells.Add(new DataGridViewTextBoxCell() { Value = x.Item2 != null ? x.Item2.Name : "" });

                    if (x.Item2 == null)
                    {
                        chkaction.Style.BackColor = Color.LightGray;
                        chkaction.ReadOnly = true;
                    }

                    return row;
                }).ToArray();

                var form = new ChooseFiles();                
                form.Text = "Upload files to " + cloudProjectName;
                form.chkAction.HeaderText = "Upload";
                form.dgrFiles.Rows.AddRange(rows);
                var res = form.ShowDialog();
                if (res != System.Windows.Forms.DialogResult.OK) return;

                parsed_filesList = form.dgrFiles.Rows.Cast<DataGridViewRow>()
                    .Where(x => (x.Cells[1] as DataGridViewCheckBoxCell).Selected)
                    .Select(x => x.Cells[1].Tag as QCFile)
                    .ToList();

                //Upload Files
                QCPluginUtilities.OutputCommandString("Saving project updates...", QCPluginUtilities.Severity.Info);

                await api.ProjectUpdate(ProjectID, parsed_filesList);

                QCPluginUtilities.OutputCommandString("Project Saved Successfully", QCPluginUtilities.Severity.Info);
            }
            catch(Exception ex)
            {
                QCPluginUtilities.OutputCommandString("Save Project error: " + ex.ToString(), QCPluginUtilities.Severity.Error);
            }
        }

        /// <summary>
        /// Worker function for adding subdirectories to the file list for saving projects to the cloud.
        /// </summary>
        /// <param name="basePath"></param>
        /// <param name="dirPath"></param>
        /// <param name="fileList"></param>
        private static void SavetoCloud_AddSubdirectoryToFileList(String basePath, String dirPath, Dictionary<string, string> fileList)
        {

            // TODO: Need to add directory prefix, get path relative to baseDir
            string[] files = Directory.GetFiles(dirPath, "*.cs");
            foreach (string file in files)
            {
                string relBaseDir = dirPath.Substring(basePath.Length);
                string fileName = Path.Combine(relBaseDir, Path.GetFileName(file));
                fileName = fileName.Replace('\\', '/'); // Need to match the server, RESTAPI should accept different styles of directory delimiters
                fileList.Add(fileName, File.ReadAllText(file));
            }

            string[] dirs = Directory.GetDirectories(dirPath);
            foreach (string dir in dirs)
            {
                // We want to check the name of this directory, not the containing directory
                string dirName = Path.GetFileName(dir);
                if ("bin".Equals(dirName, StringComparison.OrdinalIgnoreCase) || 
                    "obj".Equals(dirName, StringComparison.OrdinalIgnoreCase) || 
                    "properties".Equals(dirName, StringComparison.OrdinalIgnoreCase))
                {
                    // Lets not add bin or obj directories.
                    // a manifest approach would really be much cleaner...
                }
                else
                {
                    SavetoCloud_AddSubdirectoryToFileList(basePath, dir, fileList);
                }
            }
        }

        public static void Logout()
        {
            QCPluginUtilities.OutputCommandString("Removed user credentials.", QCPluginUtilities.Severity.Info);
            api.RemoveAuthentication();
            var credentialFile = Path.Combine(QCPluginUtilities.InstallPath, "credentials.config");

            if (File.Exists(credentialFile))
                File.Delete(credentialFile);
        }

        public async static Task<bool> CreateBacktest(int ProjectID, string backtestName)
        {
            try
            {
                await Authenticate();

                QCPluginUtilities.OutputCommandString("Building project...", QCPluginUtilities.Severity.Info);
                var res = await api.Compile(ProjectID);

                QCPluginUtilities.OutputCommandString("Running backtest...", QCPluginUtilities.Severity.Info);
                var backtestResult = await api.Backtest(ProjectID, res.CompileId, backtestName);
                if (string.IsNullOrEmpty(backtestResult.BacktestId) || backtestResult.BacktestId == "0")
                    throw new Exception("Failed to run backtest.");

                QCPluginUtilities.OutputCommandString("Project Backtest created.", QCPluginUtilities.Severity.Info);
                return true;
            }
            catch (Exception ex)
            {
                var msg = ex.Message;
                if (msg.Contains("Time out on build request"))
                {
                    QCPluginUtilities.OutputCommandString("Build timed out, retrying...", QCPluginUtilities.Severity.Warning);
                    return false;
                }
                else if (msg.Contains("Please upgrade your account"))
                {
                    QCPluginUtilities.OutputCommandString("You have reached the limit of 5 backtests per day via API on a free account. Please upgrade your account to unlock unlimited backtests.", QCPluginUtilities.Severity.Info);
                    return true;
                }
                else
                {
                    QCPluginUtilities.OutputCommandString("Run backtest error: " + ex.ToString(), QCPluginUtilities.Severity.Error);
                    return true;
                }               
            }
        }

        public async static Task DeleteBacktest(string BacktestID)
        {
            try
            {
                await Authenticate();

                QCPluginUtilities.OutputCommandString("Deleting backtest...", QCPluginUtilities.Severity.Info);

                await api.BacktestDelete(BacktestID);

                QCPluginUtilities.OutputCommandString("Backtest deleted successfuly...", QCPluginUtilities.Severity.Info);
            }
            catch (Exception ex)
            {
                QCPluginUtilities.OutputCommandString("Error deleting backtest: " + ex.ToString(), QCPluginUtilities.Severity.Error);
            }
        }

        public async static Task<List<BacktestSummary>> GetBacktestList(int ProjectID)
        {
            try
            {
                await Authenticate();

                QCPluginUtilities.OutputCommandString("Receiving backtests...", QCPluginUtilities.Severity.Info);

                var backtests = await api.BacktestList(ProjectID);

                QCPluginUtilities.OutputCommandString("Backtests received successfuly...", QCPluginUtilities.Severity.Info);

                return backtests.Summary;
            }
            catch(Exception ex)
            {
                QCPluginUtilities.OutputCommandString("Error receiving backtests: " + ex.ToString(), QCPluginUtilities.Severity.Error);
            }

            return null;
        }

        public async static Task<List<CombinedProject>> GetProjectList()
        {
            try
            {
                await Authenticate();

                QCPluginUtilities.OutputCommandString("Receiving projects...", QCPluginUtilities.Severity.Info);

                var projects = await api.ProjectList();

                QCPluginUtilities.OutputCommandString("Projects received successfuly...", QCPluginUtilities.Severity.Info);

                //FULL OUTER JOIN !!!
                var alookup = QCPluginUtilities.GetAllProjects().Select(x => new KeyValuePair<int,dynamic>(x.Id, x));
                var blookup = projects.Projects.Select(x => new KeyValuePair<int, dynamic>(x.Id, x));

                var combproj = QCPluginUtilities.FullOuterJoin<int>(alookup, blookup);
                return combproj
                    .Select(x => new CombinedProject
                    {
                        Id =               x.Item1,
                        Name =             x.Item3 != null ? x.Item3.Name : "",
                        CloudProjectName = x.Item3 != null ? x.Item3.Name : "",
                        Modified =         x.Item3 != null ? x.Item3.Modified : DateTime.MinValue,
                        LocalProjectName = x.Item2 != null ? x.Item2.name : "",
                        LocalProjectPath = x.Item2 != null ? x.Item2.path : "",
                        uniqueName =       x.Item2 != null ? x.Item2.uniqueName : ""
                    })
                    .ToList();
            }
            catch (Exception ex)
            {
                QCPluginUtilities.OutputCommandString("Error receiving projects: " + ex.ToString(), QCPluginUtilities.Severity.Error);
            }

            return null;
        }

        public async static Task CreateProject(string projectName)
        {
            try
            {
                await Authenticate();

                QCPluginUtilities.OutputCommandString("Creating project...", QCPluginUtilities.Severity.Info);

                await api.ProjectCreate(projectName);

                QCPluginUtilities.OutputCommandString("Project created successfuly...", QCPluginUtilities.Severity.Info);
            }
            catch (Exception ex)
            {
                QCPluginUtilities.OutputCommandString("Error creating project: " + ex.ToString(), QCPluginUtilities.Severity.Error);
            }
        }

        public async static Task<PacketBase> GetBacktestResults(string backtestId)
        {
            try
            {
                QCPluginUtilities.OutputCommandString("getting backtest results...", QCPluginUtilities.Severity.Info);

                var results = await api.BacktestResults(backtestId);
                return results;
            }
            catch (Exception ex)
            {
                QCPluginUtilities.OutputCommandString("Error getting backtest results: " + ex.ToString(), QCPluginUtilities.Severity.Error);
            }

            return null;
        }

        public async static Task DeleteProject(int ProjectID)
        {
            try
            {
                await Authenticate();

                QCPluginUtilities.OutputCommandString("Deleting project...", QCPluginUtilities.Severity.Info);

                await api.ProjectDelete(ProjectID);

                QCPluginUtilities.OutputCommandString("Project deleted successfuly...", QCPluginUtilities.Severity.Info);
            }
            catch (Exception ex)
            {
                QCPluginUtilities.OutputCommandString("Error deleting project: " + ex.ToString(), QCPluginUtilities.Severity.Error);
            }
        }

        public async static Task DownloadProject(int ProjectID, string cloudProjectName, string ProjectName, string ProjectDir)
        {
            try
            {
                await Authenticate();

                var fileList = new Dictionary<string, string>();
                SavetoCloud_AddSubdirectoryToFileList(ProjectDir, ProjectDir, fileList);
                var cloudfiles = await api.ProjectFiles(ProjectID);

                //FULL OUTER JOIN !!!
                var alookup = fileList.Select(x => new KeyValuePair<string, dynamic>(x.Key, new QCFile(x.Key, x.Value)));
                var blookup = cloudfiles.Files.Select(x => new KeyValuePair<string, dynamic>(x.Name, x));
                var combolist = QCPluginUtilities.FullOuterJoin<string>(alookup, blookup);
                var rows = combolist.Select(x =>
                {
                    var row = new DataGridViewRow();
                    var chkaction = new DataGridViewCheckBoxCell() { Value = false, Tag = x.Item3 };

                    row.Cells.Add(new DataGridViewTextBoxCell() { Value = x.Item3 != null ? x.Item3.Name : "" });
                    row.Cells.Add(chkaction);
                    row.Cells.Add(new DataGridViewTextBoxCell() { Value = x.Item2 != null ? x.Item2.Name : "" });

                    if (x.Item3 == null)
                    {
                        chkaction.Style.BackColor = Color.LightGray;
                        chkaction.ReadOnly = true;
                    }

                    return row;
                }).ToArray();

                var form = new ChooseFiles();
                form.Text = "Download files from " + cloudProjectName;
                form.chkAction.HeaderText = "Download";
                form.dgrFiles.Rows.AddRange(rows);
                var res = form.ShowDialog();
                if (res != System.Windows.Forms.DialogResult.OK) return;

                var parsed_filesList = form.dgrFiles.Rows.Cast<DataGridViewRow>()
                    .Where(x => (x.Cells[1] as DataGridViewCheckBoxCell).Selected)
                    .Select(x => x.Cells[1].Tag as QCFile)
                    .ToList();

                QCPluginUtilities.UpdateLocalProject(parsed_filesList, ProjectName);

                QCPluginUtilities.OutputCommandString("Project created successfuly...", QCPluginUtilities.Severity.Info);
            }
            catch (Exception ex)
            {
                QCPluginUtilities.OutputCommandString("Error creating project: " + ex.ToString(), QCPluginUtilities.Severity.Error);
            }
        }

        public async static Task SaveRemoteBacktest(string backtestId)
        {
            await QCStudioPluginActions.Authenticate();
            var _results = await QCStudioPluginActions.GetBacktestResults(backtestId);

            QCPluginUtilities.OutputCommandString("GetBacktestResults succeded: " + _results.Success, QCPluginUtilities.Severity.Info);
            foreach (var err in _results.Errors)
            {
                QCPluginUtilities.OutputCommandString(err, QCPluginUtilities.Severity.Error);
            }

            if (_results != null)
            {
                var dlg = new SaveFileDialog
                {
                    AddExtension = true,
                    Filter = "JSON file|*.json|All files|*.*",
                    Title = "Save Backtest results to file"
                };
                
                if(DialogResult.OK == dlg.ShowDialog()) 
                {
                    if (string.IsNullOrEmpty(dlg.FileName)) return;
                    var json = JsonConvert.SerializeObject(_results);
                    File.Delete(dlg.FileName);
                    File.WriteAllText(dlg.FileName, json);
                }
            }
            else
                QCPluginUtilities.OutputCommandString("No backtest results for: " + backtestId, QCPluginUtilities.Severity.Error);
        }

        public async static Task SaveLocalBacktest(string pluginsPath, string dataPath)
        {
            string algorithmPath, className;
            QCPluginUtilities.GetSelectedItem(out algorithmPath, out className);
            if (algorithmPath == null || className == null) return;

            var json = await LeanActions.RunLocalBacktest(algorithmPath, className, pluginsPath, dataPath);
            if (json != null)
            {
                var dlg = new SaveFileDialog
                {
                    AddExtension = true,
                    Filter = "JSON file|*.json|All files|*.*",
                    Title = "Save Backtest results to file"
                };

                if (DialogResult.OK == dlg.ShowDialog())
                {
                    if (string.IsNullOrEmpty(dlg.FileName)) return;
                    File.Delete(dlg.FileName);
                    File.WriteAllText(dlg.FileName, json);
                }
            }
            else
                QCPluginUtilities.OutputCommandString("No backtest results for: " + className, QCPluginUtilities.Severity.Error);
        }

        public async static Task<PacketBase> LoadLocalBacktest(string FileName)
        {
            if (string.IsNullOrEmpty(FileName)) return null;
            if (!File.Exists(FileName))
            {
                QCPluginUtilities.OutputCommandString("Backtest file doesn't exist " + FileName, QCPluginUtilities.Severity.Error);
                return null;
            }

            string json = null;
            using (var reader = File.OpenText(FileName))
                json = await reader.ReadToEndAsync();

            var _results = JsonConvert.DeserializeObject<PacketBase>(json);
            _results.rawData = json;
            if (_results.Errors == null)
                _results.Errors = new List<string>();

            return _results;               
        }
    }

    public class User
    {
        [JsonProperty(PropertyName = "email")]
        public string Email { get; set; }

        [JsonProperty(PropertyName = "password")]
        public string Password { get; set; }

        [JsonProperty(PropertyName = "userid")]
        public string UserID { get; set; }

        [JsonProperty(PropertyName = "authtoken")]
        public string AuthToken { get; set; }
    }

    public class CombinedProject: Project
    {
        public string CloudProjectName { get; set; }
        public string LocalProjectName { get; set; }
        public string uniqueName;
        public string LocalProjectPath;       
    }
}
