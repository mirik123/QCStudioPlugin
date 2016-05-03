/*
* Mark Babayev (https://github.com/mirik123) - QC internal actions
*/

using Newtonsoft.Json;
using QuantConnect.QCPlugin;
using QuantConnect.RestAPI;
using QuantConnect.RestAPI.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;


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
                QCPluginUtilities.OutputCommandString("Authentication error: " + ex.ToString());
            }
        }

        private async static Task Authenticate()
        {
            if(!api.IsAuthenticated)
            {
                var user = new User { Email = "", Password = "" };
                var credentialFile = Path.Combine(QCPluginUtilities.InstallPath, "credentials.config");

                QCPluginUtilities.OutputCommandString("Authenticating QC user...");
                if (File.Exists(credentialFile))
                {
                    var userData = File.ReadAllText(credentialFile);
                    var jsonUser = JsonConvert.DeserializeAnonymousType<User>(userData, user);

                    user.Email = Encrypter.DecryptString(jsonUser.Email);
                    user.Password = Encrypter.DecryptString(jsonUser.Password);
                }

                try
                {
                    await api.Authenticate(user.Email, user.Password);
                }
                catch(Exception ex)
                {
                    QCPluginUtilities.OutputCommandString("Authentication error: " + ex.ToString());
                    QCPluginUtilities.OutputCommandString("Failed to authenticate. Enter credentials manually.");

                    bool remember = false;
                    var win = new FormLogin(user.Email, user.Password);
                    win.SuccessCallback = (email2, pass2, remember2) =>
                    {
                        user.Email = Encrypter.EncryptString(email2);
                        user.Password = Encrypter.EncryptString(pass2);
                        remember = remember2;
                        
                        return api.Authenticate(email2, pass2);
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

                QCPluginUtilities.OutputCommandString("User authenticated successfuly.");
            }
        }

        public async static Task UploadProject(int ProjectID, string ProjectDir)
        {
            try
            {
                await Authenticate();

                var fileList = new Dictionary<string, string>();

                SavetoCloud_AddSubdirectoryToFileList(ProjectDir, ProjectDir, fileList);
                var parsed_filesList = fileList.Select(x => new QCFile(x.Key, x.Value)).ToList();

                //Upload Files
                QCPluginUtilities.OutputCommandString("Saving project updates...");

                await api.ProjectUpdate(ProjectID, parsed_filesList);

                QCPluginUtilities.OutputCommandString("Project Saved Successfully");
            }
            catch(Exception ex)
            {
                QCPluginUtilities.OutputCommandString("Save Project error: " + ex.ToString());
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
            QCPluginUtilities.OutputCommandString("Removed user credentials.");
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

                QCPluginUtilities.OutputCommandString("Building project...");
                var res = await api.Compile(ProjectID);

                QCPluginUtilities.OutputCommandString("Running backtest...");
                var backtestResult = await api.Backtest(ProjectID, res.CompileId, backtestName);
                if (string.IsNullOrEmpty(backtestResult.BacktestId) || backtestResult.BacktestId == "0")
                    throw new Exception("Failed to run backtest.");

                QCPluginUtilities.OutputCommandString("Project Backtest created.");
            }
            catch (Exception ex)
            {
                var msg = ex.Message;
                if (msg.Contains("Time out on build request"))
                {
                    QCPluginUtilities.OutputCommandString("Build timed out, retrying...");
                    CreateBacktest(ProjectID, backtestName);
                }
                else if (msg.Contains("Please upgrade your account"))
                {
                    QCPluginUtilities.OutputCommandString("You have reached the limit of 5 backtests per day via API on a free account. Please upgrade your account to unlock unlimited backtests.");
                }
                else
                {
                    QCPluginUtilities.OutputCommandString("Run backtest error: " + ex.ToString());
                }               
            }            
        }

        public async static Task DeleteBacktest(string BacktestID)
        {
            try
            {
                await Authenticate();

                QCPluginUtilities.OutputCommandString("Deleting backtest...");

                await api.BacktestDelete(BacktestID);

                QCPluginUtilities.OutputCommandString("Backtest deleted successfuly...");
            }
            catch (Exception ex)
            {
                QCPluginUtilities.OutputCommandString("Error deleting backtest: " + ex.ToString());
            }
        }

        public async static Task<List<BacktestSummary>> GetBacktestList(int ProjectID)
        {
            try
            {
                await Authenticate();

                QCPluginUtilities.OutputCommandString("Receiving backtests...");

                var backtests = await api.BacktestList(ProjectID);

                QCPluginUtilities.OutputCommandString("Backtests received successfuly...");

                return backtests.Summary;
            }
            catch(Exception ex)
            {
                QCPluginUtilities.OutputCommandString("Error receiving backtests: " + ex.ToString());
            }

            return null;
        }

        public async static Task<List<CombinedProject>> GetProjectList()
        {
            try
            {
                await Authenticate();

                QCPluginUtilities.OutputCommandString("Receiving projects...");

                var projects = await api.ProjectList();

                QCPluginUtilities.OutputCommandString("Projects received successfuly...");

                //FULL OUTER JOIN !!!
                var alookup = QCPluginUtilities.GetAllProjects().ToLookup(x => x.Id);
                var blookup = projects.Projects.ToLookup(y => y.Id);

                var keys = new HashSet<int>(alookup.Select(p => (int)p.Key));
                keys.UnionWith(blookup.Select(p => p.Key));

                var combproj = 
                    from key in keys
                    from xa in alookup[key].DefaultIfEmpty(new { name = "", path = "", Id = 0 })
                    from xb in blookup[key].DefaultIfEmpty(new Project())
                    select new CombinedProject
                    {
                        Id = key,
                        Name = xb.Name,
                        CloudProjectName = xb.Name,
                        Modified = xb.Modified,
                        LocalProjectName = xa.name,
                        LocalProjectPath = xa.path
                    };

                return combproj.ToList();
            }
            catch (Exception ex)
            {
                QCPluginUtilities.OutputCommandString("Error receiving projects: " + ex.ToString());
            }

            return null;
        }

        public async static Task CreateProject(string projectName)
        {
            try
            {
                await Authenticate();

                QCPluginUtilities.OutputCommandString("Creating project...");

                await api.ProjectCreate(projectName);

                QCPluginUtilities.OutputCommandString("Project created successfuly...");
            }
            catch (Exception ex)
            {
                QCPluginUtilities.OutputCommandString("Error creating project: " + ex.ToString());
            }
        }

        public async static Task<PacketBacktestResult> GetBacktestResults(string backtestId)
        {
            try
            {
                QCPluginUtilities.OutputCommandString("getting backtest results...");

                return await api.BacktestResults(backtestId);

                QCPluginUtilities.OutputCommandString("backtest results received...");
            }
            catch (Exception ex)
            {
                QCPluginUtilities.OutputCommandString("Error getting backtest results: " + ex.ToString());
            }

            return null;
        }

        public async static Task DeleteProject(int ProjectID)
        {
            try
            {
                await Authenticate();

                QCPluginUtilities.OutputCommandString("Deleting project...");

                await api.ProjectDelete(ProjectID);

                QCPluginUtilities.OutputCommandString("Project deleted successfuly...");
            }
            catch (Exception ex)
            {
                QCPluginUtilities.OutputCommandString("Error deleting project: " + ex.ToString());
            }
        }

        public async static Task DownloadProject(int ProjectID, string ProjectDir)
        {
            try
            {
                await Authenticate();

                QCPluginUtilities.OutputCommandString("Creating project...");

                var files = await api.ProjectFiles(ProjectID);

                QCPluginUtilities.OutputCommandString("Project created successfuly...");
            }
            catch (Exception ex)
            {
                QCPluginUtilities.OutputCommandString("Error creating project: " + ex.ToString());
            }
        }
    }

    public class User
    {
        [JsonProperty(PropertyName = "email")]
        public string Email { get; set; }

        [JsonProperty(PropertyName = "password")]
        public string Password { get; set; }
    }

    public class CombinedProject: Project
    {
        public string CloudProjectName { get; set; }
        public string LocalProjectPath { get; set; }
        public string LocalProjectName { get; set; }
    }
}
