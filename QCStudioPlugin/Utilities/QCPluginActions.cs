/*
* QUANTCONNECT.COM - Democratizing Finance, Empowering Individuals,
* QuantConnect Visual Studio Plugin
*/

using QuantConnect.QCStudioPlugin.Forms;
using Newtonsoft.Json;
using QuantConnect.QCPlugin;
using QuantConnect.RestAPI;
using QuantConnect.RestAPI.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using System.Threading;
using QuantConnect.QCStudioPlugin.Properties;
using QuantConnect.QCStudioPlugin.Lean.interfaces;
using Newtonsoft.Json.Linq;


namespace QuantConnect.QCStudioPlugin.Actions
{
    public static class QCStudioPluginActions
    {
        private static API api;

        public static void Initialize()
        {
            QCStudioPluginActions.api = new API();
            //Authenticate();
        }

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

        public async static Task CreateBacktest(int ProjectID, string backtestName)
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
            }
            catch (Exception ex)
            {
                var msg = ex.Message;
                if (msg.Contains("Time out on build request"))
                {
                    QCPluginUtilities.OutputCommandString("Build timed out, retrying...", QCPluginUtilities.Severity.Warning);
                    CreateBacktest(ProjectID, backtestName);
                }
                else if (msg.Contains("Please upgrade your account"))
                {
                    QCPluginUtilities.OutputCommandString("You have reached the limit of 5 backtests per day via API on a free account. Please upgrade your account to unlock unlimited backtests.", QCPluginUtilities.Severity.Info);
                }
                else
                {
                    QCPluginUtilities.OutputCommandString("Run backtest error: " + ex.ToString(), QCPluginUtilities.Severity.Error);
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

        public async static Task<PacketBacktestResult> GetBacktestResults(string backtestId)
        {
            try
            {
                await Authenticate();

                QCPluginUtilities.OutputCommandString("getting backtest results...", QCPluginUtilities.Severity.Info);

                var results = await api.BacktestResults(backtestId);
                results.UserID = api.UserID;
                results.AuthToken = api.AuthToken;

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

        public static Task<PacketBacktestResult> RunLocalBacktest(string algorithmPath, string fileName, string pluginsPath, string dataPath)
        {
            var statistics = new Dictionary<string, string>();
            var task = new TaskCompletionSource<PacketBacktestResult>();

            var tokenSource = new CancellationTokenSource();
            var token = tokenSource.Token;

            var lean = new LeanRefactoring();

            try
            {
                lean.LoadLibraries(pluginsPath);

                lean.SetConfiguration("environment", "");   //"backtesting-desktop"
                lean.SetConfiguration("plugin-directory", pluginsPath);
                lean.SetConfiguration("data-folder", dataPath);
                lean.SetConfiguration("data-directory", dataPath);                
                lean.SetConfiguration("algorithm-location", algorithmPath);
                lean.SetConfiguration("algorithm-type-name", fileName);

                lean.SetConfiguration("algorithm-language", "CSharp");
                lean.SetConfiguration("live-mode", "false");
                lean.SetConfiguration("debug-mode", "true");

                lean.SetConfiguration("messaging-handler", "QuantConnect.Messaging.EventMessagingHandler");
                lean.SetConfiguration("job-queue-handler", "QuantConnect.Queues.JobQueue");
                lean.SetConfiguration("api-handler", "QuantConnect.Api.Api");
                lean.SetConfiguration("result-handler", "QuantConnect.Lean.Engine.Results.BacktestingResultHandler");
                lean.SetConfiguration("setup-handler", "QuantConnect.Lean.Engine.Setup.ConsoleSetupHandler");
                lean.SetConfiguration("data-feed-handler", "QuantConnect.Lean.Engine.DataFeeds.FileSystemDataFeed");
                lean.SetConfiguration("real-time-handler", "QuantConnect.Lean.Engine.RealTime.BacktestingRealTimeHandler");
                lean.SetConfiguration("transaction-handler", "QuantConnect.Lean.Engine.TransactionHandlers.BacktestingTransactionHandler");
                lean.SetConfiguration("log-handler", "QuantConnect.Logging.QueueLogHandler");


                //Composer.Instance.Reset();
                var composer = lean.CreateComposer();

                //Log.LogHandler = Composer.Instance.GetExportedValueByTypeName<ILogHandler>("QuantConnect.Logging.QueueLogHandler");
                lean.SetLogHandler(composer, (packet) =>
                {
                    var json = JObject.FromObject(packet);
                    var message = json.GetValue("Message").Value<string>();
                    var hstack = json.GetValue("StackTrace").Value<string>();

                    QCPluginUtilities.OutputCommandString(message +", "+ hstack, QCPluginUtilities.Severity.Info);
                });

                Task enginetask = null;
                //var systemHandlers = LeanEngineSystemHandlers.FromConfiguration(Composer.Instance);
                var systemHandlers = lean.CreateLeanEngineSystemHandlers(composer);

                //var algorithmHandlers = LeanEngineAlgorithmHandlers.FromConfiguration(Composer.Instance);
                var algorithmHandlers = lean.CreateLeanEngineAlgorithmHandlers(composer);

                QCPluginUtilities.OutputCommandString("Running " + fileName + "...", QCPluginUtilities.Severity.Info);

                //var _messaging = systemHandlers.Notify;
                lean.AddMessagingEvents(systemHandlers, algorithmHandlers, (packet) =>
                {
                    var json = JObject.FromObject(packet);
                    var message = json.GetValue("Message").Value<string>();
                    var hstack = json.GetValue("StackTrace").Value<string>();

                    QCPluginUtilities.OutputCommandString(message + ", " + hstack, QCPluginUtilities.Severity.Info);
                }, (packet) =>
                {
                    (systemHandlers as IDisposable).Dispose();
                    (algorithmHandlers as IDisposable).Dispose();
                    tokenSource.Cancel();
                    tokenSource.Dispose();

                    var json = JObject.FromObject(packet);
                    var result = new PacketBacktestResult
                    {
                        PeriodFinish = json.GetValue("PeriodFinish").Value<DateTime>(),
                        PeriodStart = json.GetValue("PeriodStart").Value<DateTime>(),
                        Results = json.GetValue("Results").Value<BacktestResult>(),
                        Progress = json.GetValue("Progress").Value<string>()
                    };
                    task.SetResult(result);
                });

                string _algorithmPath;
                var job = lean.GetNextJob(systemHandlers, out _algorithmPath);

                //var engine = new Lean.Engine.Engine(systemHandlers, algorithmHandlers, false);
                var engine = lean.CreateEngine(systemHandlers, algorithmHandlers, false);
                enginetask = Task.Factory.StartNew(() =>
                {
                    try
                    {
                        //engine.Run(job, _algorithmPath); 
                        lean.RunEngine(engine, job, _algorithmPath);
                    }
                    catch (Exception ex)
                    {
                        QCPluginUtilities.OutputCommandString(string.Format("{0} {1}", ex.Message, ex.StackTrace), QCPluginUtilities.Severity.Error);
                        task.SetException(ex);
                    }
                }, token);
            }
            catch (Exception ex)
            {
                QCPluginUtilities.OutputCommandString(string.Format("{0} {1}", ex.Message, ex.StackTrace), QCPluginUtilities.Severity.Error);
                task.SetException(ex);
            }

            return task.Task;
        }

        /*public static Task<BacktestResultPacket> RunLocalBacktest(string algorithmPath, string fileName)
        {
            var statistics = new Dictionary<string, string>();
            var task = new TaskCompletionSource<BacktestResultPacket>();

            var tokenSource = new CancellationTokenSource();
            var token = tokenSource.Token;

            try
            {
                Composer.Instance.Reset();

                // set the configuration up
                Config.Set("algorithm-type-name", fileName);
                Config.Set("live-mode", "false");
                Config.Set("environment", "");
                Config.Set("messaging-handler", "QuantConnect.Messaging.EventMessagingHandler");
                Config.Set("job-queue-handler", "QuantConnect.Queues.JobQueue");
                Config.Set("api-handler", "QuantConnect.Api.Api");
                Config.Set("result-handler", "QuantConnect.Lean.Engine.Results.BacktestingResultHandler");
                Config.Set("algorithm-language", "CSharp");
                Config.Set("algorithm-location", algorithmPath);

                Task enginetask = null;
                var algorithmHandlers = LeanEngineAlgorithmHandlers.FromConfiguration(Composer.Instance);
                var systemHandlers = LeanEngineSystemHandlers.FromConfiguration(Composer.Instance);
                
                QCPluginUtilities.OutputCommandString("{0}: Running " + fileName + "...", QCPluginUtilities.Severity.Info);

                var _messaging = (EventMessagingHandler)systemHandlers.Notify;
                _messaging.DebugEvent += (DebugPacket packet) => {
                    QCPluginUtilities.OutputCommandString(packet.Message, QCPluginUtilities.Severity.Info);
                };
                _messaging.LogEvent += (LogPacket packet) => {
                    QCPluginUtilities.OutputCommandString(packet.Message, QCPluginUtilities.Severity.Info);
                };
                _messaging.RuntimeErrorEvent += (RuntimeErrorPacket packet) => {
                    var hstack = !string.IsNullOrEmpty(packet.StackTrace) ? packet.StackTrace : string.Empty;
                    QCPluginUtilities.OutputCommandString(packet.Message + hstack, QCPluginUtilities.Severity.Error);
                };
                _messaging.HandledErrorEvent += (HandledErrorPacket packet) => {
                    var hstack = !string.IsNullOrEmpty(packet.StackTrace) ? packet.StackTrace : string.Empty;
                    QCPluginUtilities.OutputCommandString(packet.Message + hstack, QCPluginUtilities.Severity.Error);
                };
                _messaging.BacktestResultEvent += (BacktestResultPacket packet) => {
                    systemHandlers.Dispose();
                    algorithmHandlers.Dispose();
                    tokenSource.Cancel();
                    tokenSource.Dispose();

                    task.SetResult(packet);
                };

                string _algorithmPath;
                var job = systemHandlers.JobQueue.NextJob(out _algorithmPath);
                var engine = new Lean.Engine.Engine(systemHandlers, algorithmHandlers, false);
                enginetask = Task.Factory.StartNew(() => { engine.Run(job, algorithmPath); }, token);                            
            }
            catch (Exception ex)
            {
                QCPluginUtilities.OutputCommandString(string.Format("{0} {1}", ex.Message, ex.StackTrace), QCPluginUtilities.Severity.Error);
                task.SetException(ex);
            }

            return task.Task;
        }*/

        public static string GetTerminalUrl(string backtestId, int ProjectId = 0, bool liveMode = false, bool holdReady = true)
        {
            var url = "";
            var hold = holdReady == false ? "0" : "1";
            var embedPage = liveMode ? "embeddedLive" : "embedded";

            url = string.Format(
                "https://www.quantconnect.com/terminal/{0}?user={1}&token={2}&pid={3}&version={4}&holdReady={5}&bid={6}",
                embedPage, api.UserID, api.AuthToken, ProjectId, Resources.QuantConnectVersion, hold, backtestId);

            return url;
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
