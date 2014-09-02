/*
* QUANTCONNECT.COM - Democratizing Finance, Empowering Individuals,
* QuantConnect Visual Studio Plugin
*/

/**********************************************************
* USING NAMESPACES
**********************************************************/
using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuantConnect.RestAPI;
using QuantConnect.RestAPI.Models;
using System.Configuration;
using System.Windows.Forms;
using Extensibility;
using EnvDTE100;
using VSLangProj;
using System.Threading;
using System.Text.RegularExpressions;
using Microsoft.Build.Execution;
using System.Diagnostics;
using Newtonsoft.Json;
using ICSharpCode.SharpZipLib;
using ICSharpCode.SharpZipLib.Zip;
using ICSharpCode.SharpZipLib.Core;

namespace QuantConnect.QCPlugin
{
    /// <summary>
    /// PRIMARY COMMON QUANTCONNECT PLUGIN INFRASTRUCTURE. CROSS PLATFORM.
    /// </summary>
    public static class QuantConnectPlugin
    {
        /******************************************************** 
        * CLASS VARIABLES
        *********************************************************/
        public static string Email = "";
        public static string Password = "";
        public static int ProjectID = 0;
        public static string ProjectName = "";
        public static string Directory = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\QuantConnect Projects\";
        public static bool Logged = false;
        public static bool ProjectLoaded = false;
        public static bool RateLimitReached = false;

        /// <summary>
        /// Plugin API Access
        /// </summary>
        public static API API;

        /******************************************************** 
        * CLASS METHODS
        *********************************************************/
        public static void Initialize()
        {
            // Start thread
            Thread thread = new Thread(new ThreadStart(Async.Consumer));
            thread.Start();  

            // Set email and password
            string[] credentials = LoadCredentials();
            Email = credentials[0];
            Password = credentials[1];

            //Create API Object
            API = new API(Email, Password);

            //Attempt Login:
            QuantConnectPlugin.Logged = API.Authenticate(Email, Password);

            //Check if QCAlgorithm is saved to its last version
            try
            {
                Async.Add(new APIJob(APICommand.CheckQCAlgoVersion, (latestversion, errors) =>
                {
                    if (!(bool)latestversion)
                    {
                        API.DownloadQCAlgorithm(Directory);
                    }
                }, Directory));
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }

            //Check if QCTemplate exists
            try
            {
                Async.Add(new APIJob(APICommand.UpdateTemplate, (a, b) =>
                {
                    var finished = false;
                    var baseDirectory = Directory + "QCTemplate";
                    var destination = baseDirectory + @"\template.zip";

                    // Check if QCTemplate exists
                    if (!System.IO.Directory.Exists(baseDirectory))
                    {
                        System.IO.Directory.CreateDirectory(baseDirectory);
                        using (System.Net.WebClient client = new System.Net.WebClient())
                        {
                            try
                            {
                                client.DownloadFile(new Uri("https://www.quantconnect.com/api/v1/QCTemplate.zip"), destination);
                                finished = true;
                            } catch { }
                        }

                        if (finished)
                        {
                            API.WriteZip(baseDirectory, destination);
                        }
                    }
                }, Directory));
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }

        }

        /// <summary>
        /// Perform simple actions based on the errors from the API
        /// </summary>
        /// <param name="errors"></param>
        public static APIErrors HandleErrors(List<string> errors)
        {
            APIErrors errorResponse = APIErrors.None;
            if (errors == null) return errorResponse;

            foreach (string error in errors)
            {
                switch (error)
                {
                    case "Could not authenticate. Username or password incorrect":
                        //Disable buttons
                        QuantConnectPlugin.Logged = false;
                        SetButtonsState(false);
                        //ProjectLoaded = false;
                        RemoveCredentials();
                        errorResponse = APIErrors.NotLoggedIn;
                        break;

                    case "Time out on build request":
                        errorResponse = APIErrors.CompileTimeout;
                        break;

                    default:
                        errorResponse = APIErrors.CompileError;
                        break;
                }
            }
            return errorResponse;
        }

        /// <summary>
        /// Show password form, save credentials to file
        /// </summary>
        public static void ShowSaveCredentials()
        {
            FormLogin setCredentials = new FormLogin();
            setCredentials.StartPosition = FormStartPosition.CenterScreen;
            setCredentials.Show();
        }

        /// <summary>
        /// Login Method to open SetCredentials if not logged in
        /// </summary>
        /// <param name="callback"></param>
        public static void Login(Action success)
        {
            if (Logged)
            {
                success();
            }
            else
            {
                FormLogin setCredentials = new FormLogin();
                setCredentials.SetCallBacks(success);
                setCredentials.StartPosition = FormStartPosition.CenterScreen;
                setCredentials.Show();
            }
        }

        /// <summary>
        /// Show the new project dialog:
        /// </summary>
        public static void ShowNewProject()
        {
            Login(() =>
            {
                ShowInputBox("Create New Project", "Please enter the project name:", (projectName) => {

                    FormCreateProject form = new FormCreateProject();
                    form.ProjectName = projectName;
                    form.Show();

                }, null);
            });  
        }


        /// <summary>
        /// Show a generic input box and callbacks 
        /// </summary>
        public static void ShowInputBox(string title, string prompt, Action<string> okCallback, Action cancelCallback)
        {
            FormInputBox form = new FormInputBox();
            form.Set(title, prompt, okCallback, cancelCallback);
            form.StartPosition = FormStartPosition.CenterScreen;
            form.Show();
        }


        /// <summary>
        /// Show Open Projects form, save credentials to file
        /// </summary>
        public static void ShowProjects()
        {
            Login(() =>
            {
                FormOpenProject projectList = new FormOpenProject();
                projectList.StartPosition = FormStartPosition.CenterScreen;
                projectList.Show();
            });            
        }

        /// <summary>
        /// Delete the username and password from config.
        /// </summary>
        public static void ShowLogout()
        {
            FormLogout logout = new FormLogout();
            logout.StartPosition = FormStartPosition.CenterScreen;
            logout.Show();
        }

        /// <summary>
        /// Open the rate limit reached box
        /// </summary>
        public static void ShowRateLimit()
        {
            FormRateLimit rateLimit = new FormRateLimit();
            rateLimit.StartPosition = FormStartPosition.CenterScreen;
            rateLimit.Show();
        }

        /// <summary>
        /// Open the BacktestLoader Form:
        /// </summary>
        public static void ShowLoadBacktest()
        {
            FormLoadBacktest loadBacktest = new FormLoadBacktest();
            loadBacktest.StartPosition = FormStartPosition.CenterScreen;
            loadBacktest.Show();
        }

        /// <summary>
        /// Save encrypted username and password to the config
        /// </summary>
        public static void SaveCredentials(string email, string password)
        {
            var user = new User { Email = Encrypter.EncryptString(email), Password = Encrypter.EncryptString(password) };
            var jsonUser = JsonConvert.SerializeObject(user);
            System.IO.File.WriteAllText(Directory + "credentials.config", jsonUser);
            
            //Save Local Copy:
            Email = email;
            Password = password;
        }


        /// <summary>
        /// Load credentials from the config file
        /// </summary>
        public static string[] LoadCredentials()
        {
            try
            {
                var user = new User { Email = "", Password = "" };
                var credentialFile = Directory + "credentials.config";

                if (!System.IO.File.Exists(credentialFile))
                {
                    System.IO.File.WriteAllText(credentialFile, JsonConvert.SerializeObject(user));
                }

                var userData = System.IO.File.ReadAllText(credentialFile);
                var jsonUser = JsonConvert.DeserializeAnonymousType(userData, user);
                
                Email = Encrypter.DecryptString(jsonUser.Email);
                Password = Encrypter.DecryptString(jsonUser.Password);
            }
            catch
            {
                Email = "";
                Password = "";
            }
            string[] credentials = { Email, Password };
            return credentials;
        }

        /// <summary>
        /// Delete the credentials from the config file (leave empty)
        /// </summary>
        public static void RemoveCredentials()
        {
            System.IO.File.Delete(Directory + "credentials.config");
            Email = "";
            Password = "";
            Logged = false;
            SetButtonsState(false);
            ProjectLoaded = false;
        }

        /// <summary>
        /// Check if project is already saved in the computer
        /// </summary>
        /// <param name="projectName"></param>
        /// <param name="projectID"></param>
        public static bool ExistingFileChecker(string projectName, int projectID)
        {
            bool proceed = true;
            if (!System.IO.Directory.Exists(Directory + projectID + " - " + projectName))
            {
                System.IO.Directory.CreateDirectory(Directory + projectID+ " - " + projectName);
            }
            else
            {
                DialogResult dialogResult = MessageBox.Show("You already have this project saved in your computer. Are you sure you want to overwrite it?", "Overwrite Project", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    System.IO.Directory.Delete(Directory + projectID + " - " + projectName, true);
                    proceed = true;
                }
                else if (dialogResult == DialogResult.No)
                {
                    proceed = false;
                }
            }
            return proceed;
        }

        /// <summary>
        /// Open your QuantConnect project in Visual Studio
        /// </summary>
        /// <param name="selectedProject"></param>
        /// <param name="api"></param>
        /// <param name="projectName"></param>
        public static bool OpenProject(int selectedProject, string projectName, Action enableForm)
        {
            // Don't let user open the same project he's working in
            if (selectedProject == ProjectID)
            {
                DialogResult dialogResult = MessageBox.Show("You are currently working on this project.", "Invalid Operation", MessageBoxButtons.OK);
            }

            ProjectID = selectedProject;
            ProjectName = CleanInput(projectName);
            // Check if file already exists
            if (!ExistingFileChecker(ProjectName, ProjectID))
            {
                return false;
            }

            Async.Add(new APIJob(APICommand.OpenProject, (files, errors) =>
            {
                List<File> projectFiles = (List<File>)files;
                
                // Create Solution
                Solution4 soln = (Solution4)QCPluginPackage.ApplicationObject.Solution;
                soln.Create(Directory + ProjectID + " - " + ProjectName, ProjectName + ".sln");

                // Create Project
                soln.AddFromTemplate(Directory + @"QCTemplate\csQCTemplate.vstemplate", Directory + ProjectID + " - " + ProjectName, ProjectName, false);

                // Add Files to Project:
                foreach (EnvDTE.Project proj in soln.Projects)
                {   
                    foreach(var file in proj.ProjectItems)
                    {
                        EnvDTE.ProjectItem item = file as EnvDTE.ProjectItem;
                        if (item.Name == "Class1.cs")
                        {
                            item.Remove();
                        }
                    }

                    foreach (var file in projectFiles)
                    {
                        System.IO.File.WriteAllText(Directory + ProjectID + " - " + ProjectName + @"\" + file.Name, file.Code);

                        //Add files to project:
                        try
                        {
                            proj.ProjectItems.AddFromFile(Directory + ProjectID + " - " + ProjectName + @"\" + file.Name);
                        }

                        catch (SystemException ex)
                        {
                            MessageBox.Show("ERROR: " + ex);
                        }
                    }

                    //Add References
                    VSProject vsProject = (VSLangProj.VSProject)proj.Object;

                    var commonDLL = Directory + @"QCAlgorithm-master\QuantConnect.Algorithm\bin\Debug\QuantConnect.Common.dll";
                    if (!System.IO.File.Exists(commonDLL))
                    { 
                        //Download and unzip:
                        API.DownloadQCAlgorithm(Directory);
                    }

                    //Safe to add reference:
                    vsProject.References.Add(commonDLL);
                    vsProject.References.Add(Directory + @"QCAlgorithm-master\QuantConnect.Algorithm\bin\Debug\MathNet.Numerics.dll");
                    vsProject.References.Add(Directory + @"QCAlgorithm-master\QuantConnect.Algorithm\bin\Debug\QuantConnect.Algorithm.dll");
                    vsProject.References.Add(Directory + @"QCAlgorithm-master\QuantConnect.Algorithm\bin\Debug\QuantConnect.Algorithm.Interface.dll");

                }
                //Save & open solution
                soln.SaveAs(Directory + ProjectID + " - " + ProjectName + @"\" + ProjectName + ".sln");
                ProjectLoaded = true;
                if (enableForm != null) enableForm();

            }, selectedProject));
            return true;
        }

        /// <summary>
        /// Check if project compiles locally
        /// </summary>
        public static bool LocalCompile()
        {
            // Save Files to Project in QC
            bool success;

            // Compile Locally
            Microsoft.Build.Execution.BuildManager buildSolution = new Microsoft.Build.Execution.BuildManager();
            Solution4 soln = (Solution4)QCPluginPackage.ApplicationObject.Solution;
            soln.SolutionBuild.Build(true);
            if (soln.SolutionBuild.LastBuildInfo != 0)
            {
                success = false;
            }
            else { success = true; }
            return success;
        }

        /// <summary>
        /// Backtest project on QC
        /// </summary>
        /// <param name="compile"></param>
        /// <param name="projectID"></param>
        public static string GetBacktestID(PacketCompile compile, int projectID)
        {
            string backtestID = "";
            string projectName = DateTime.Now.ToString() + " " + ProjectName;

            PacketBacktest backtestResult = API.Backtest(projectID, compile.CompileId, projectName);
            if (backtestResult.Errors.Count > 0 && backtestResult.Errors[0].Contains("Please upgrade your account"))
            {
                RateLimitReached = true;
                return backtestID;
            }
            else
            {
                backtestID = backtestResult.BacktestId;
            }
            return backtestID;
        }

        
        /// <summary>
        /// Save your files to your QuantConnect account
        /// </summary>
        public static void SaveToCloud(bool message)
        {
            if (ProjectID == 0 && message)
            {
                DialogResult dialogResult = MessageBox.Show("Please open a project first", "Open Project", MessageBoxButtons.OK);
                return;
            }
            List<File> fileList = new List<File>();
            string directory2 = Directory + ProjectID + " - " + ProjectName;
            string[] files = System.IO.Directory.GetFiles(directory2, "*.cs");
            foreach (var file in files)
            {
                string fileName = System.IO.Path.GetFileName(file);
                fileList.Add(new File(fileName, System.IO.File.ReadAllText(file)));
            }
            //Upload Files
            API.ProjectUpdate(ProjectID, fileList);
            if (message)
            {
                DialogResult dialogResult2 = MessageBox.Show("Project '" + ProjectName + "' Saved Successfully", "Saved on QuantConnect", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }            
        }

        /// <summary>
        /// Delete the entire project from the server
        /// </summary>
        public static void DeleteProject()
        {
            if (ProjectID == 0)
            {
                DialogResult dialogResult = MessageBox.Show("Please open a project first", "Open Project", MessageBoxButtons.OK);
                return;
            }

            DialogResult dialogResult2 = MessageBox.Show("Warning! This will delete your project locally and from QuantConnect's servers. Are you sure you want to proceed?", "Delete Project From QuantConnect", MessageBoxButtons.YesNo);
            if (dialogResult2 == DialogResult.Yes)
            {
                //Close Visual Studio
                Solution4 soln = (Solution4)QCPluginPackage.ApplicationObject.Solution;
                soln.Close();

                if (ProjectName != "")
                {
                    System.IO.Directory.Delete(Directory + ProjectID + " - " + ProjectName, true);
                }

                //Delete project
                API.ProjectDelete(ProjectID);

                //Reset Project Details:
                ProjectName = "";
                ProjectID = 0;
                SetButtonsState(false);
            }
            else if (dialogResult2 == DialogResult.No)
            {
                return;
            }
            
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
                return Regex.Replace(strIn, @"[^\w\.-]", "",
                                     RegexOptions.None, TimeSpan.FromSeconds(1.5));
            }
            catch (RegexMatchTimeoutException)
            {
                return String.Empty;
            }
        }

        /// <summary>
        /// Enable each of the buttons.
        /// </summary>
        /// <param name="on"></param>
        public static void SetButtonsState(bool state)
        {
            foreach (var command in QCPluginPackage.Commands.Values)
            {
                command.Enabled = state;
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
}
