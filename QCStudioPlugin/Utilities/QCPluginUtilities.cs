/*
* QUANTCONNECT.COM - Democratizing Finance, Empowering Individuals,
* QuantConnect Visual Studio Plugin
*/

using QuantConnect.QCStudioPlugin;
using EnvDTE;
using EnvDTE80;
using Microsoft.VisualStudio;
using Microsoft.VisualStudio.ExtensionManager;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using QuantConnect.QCStudioPlugin.Forms;
using QuantConnect.QCStudioPlugin.Actions;
using System.CodeDom.Compiler;
using Microsoft.CSharp;
using Microsoft.VisualStudio.Shell.Interop;
using Microsoft.VisualStudio.Shell;

namespace QuantConnect.QCStudioPlugin
{
    public static class QCPluginUtilities
    {
        public enum Severity
        {
            Info,
            Warning,
            Error
        }

        static internal string ChartTitle = "QuantConnect Lean Algorithmic Trading Engine";
        static internal string AppTitle;
        static internal DTE2 dte;
        static internal IVsThreadedWaitDialogFactory dialogFactory;
        static internal IVsOutputWindow outputWindow;
        static internal string InstallPath;
        static internal ChartPane chartWindowJSFrame;
        static internal QCClientPane chartWindowZedFrame;

        public static void Initialize(string AppTitle, DTE2 dte, IVsThreadedWaitDialogFactory dialogFactory, IVsOutputWindow outputWindow, 
                                        ChartPane chartWindowJSFrame, QCClientPane chartWindowZedFrame)
        {
            QCPluginUtilities.AppTitle = AppTitle;
            QCPluginUtilities.dialogFactory = dialogFactory;
            QCPluginUtilities.dte = dte;
            QCPluginUtilities.outputWindow = outputWindow;
            QCPluginUtilities.InstallPath = RetrieveAssemblyDirectory();
            QCPluginUtilities.chartWindowJSFrame = chartWindowJSFrame;
            QCPluginUtilities.chartWindowZedFrame = chartWindowZedFrame;
        }

        public async static void SaveLocalBacktest(string pluginsPath, string dataPath)
        {
            await QCStudioPluginActions.SaveLocalBacktest(pluginsPath, dataPath);
        }

        public async static void ShowBacktestJSRemote(string backtestId)
        {
            var frame = (IVsWindowFrame)chartWindowJSFrame.Frame;
            ErrorHandler.ThrowOnFailure(frame.Show());

            chartWindowJSFrame.control.GetBacktestResultsCallback = async () =>
            {
                var _results = await QCStudioPluginActions.GetBacktestResults(backtestId);

                QCPluginUtilities.OutputCommandString("GetBacktestResults succeded: " + _results.Success, QCPluginUtilities.Severity.Info);
                foreach (var err in _results.Errors)
                {
                    QCPluginUtilities.OutputCommandString(err, QCPluginUtilities.Severity.Error);
                }

                return _results;
            };

            await QCStudioPluginActions.Authenticate();

            string url = QCStudioPluginActions.GetTerminalUrl(backtestId);
            chartWindowJSFrame.control.Run(url);
        }

        public async static void ShowBacktestZEDRemote(string backtestId)
        {
            var frame = (IVsWindowFrame)chartWindowZedFrame.Frame;
            ErrorHandler.ThrowOnFailure(frame.Show());

            chartWindowZedFrame.control.GetBacktestResultsCallback = async () =>
            {
                var _results = await QCStudioPluginActions.GetBacktestResults(backtestId);

                QCPluginUtilities.OutputCommandString("GetBacktestResults succeded: " + _results.Success, QCPluginUtilities.Severity.Info);
                foreach (var err in _results.Errors)
                {
                    QCPluginUtilities.OutputCommandString(err, QCPluginUtilities.Severity.Error);
                }

                return _results;
            };

            await QCStudioPluginActions.Authenticate();

            chartWindowZedFrame.control.Run();
        }

        public async static void ShowBacktestJSLocal(string pluginsPath, string dataPath)
        {
            string algorithmPath, className;
            GetSelectedItem(out algorithmPath, out className);
            if (algorithmPath == null || className == null) return;
            
            var frame = (IVsWindowFrame)chartWindowJSFrame.Frame;
            ErrorHandler.ThrowOnFailure(frame.Show());

            chartWindowJSFrame.control.GetBacktestResultsCallback = async () =>
            {
                var _results = await QCStudioPluginActions.RunLocalBacktest(algorithmPath, className, pluginsPath, dataPath);

                foreach (var pair in _results.Results.Statistics)
                {
                    QCPluginUtilities.OutputCommandString("STATISTICS:: " + pair.Key + " " + pair.Value, QCPluginUtilities.Severity.Info);
                }

                return _results;
            };

            await QCStudioPluginActions.Authenticate();

            string url = QCStudioPluginActions.GetTerminalUrl(className);
            chartWindowJSFrame.control.Run(url);
        }

        public static void ShowBacktestZEDLocal(string pluginsPath, string dataPath)
        {
            string algorithmPath, className;
            GetSelectedItem(out algorithmPath, out className);
            if (algorithmPath == null || className == null) return;
            
            var frame = (IVsWindowFrame)chartWindowZedFrame.Frame;
            ErrorHandler.ThrowOnFailure(frame.Show());

            chartWindowZedFrame.control.GetBacktestResultsCallback = async () =>
            {
                var _results = await QCStudioPluginActions.RunLocalBacktest(algorithmPath, className, pluginsPath, dataPath);
                return _results;
            };

            chartWindowZedFrame.control.Run();
        }

        public static string[] GetAlgorithmsList(string filePath, string classDll)
        {
            var source = File.ReadAllText(filePath, Encoding.UTF8);
            var algoass = Assembly.LoadFrom(classDll);

            var algorithms = algoass.GetTypes()
                .Where(x => x.BaseType.FullName == "QuantConnect.Algorithm.QCAlgorithm")
                .Select(x => x.Name)
                .Where(x => source.Contains(x))
                .ToArray();

            return algorithms;
        }

        private static string RetrieveAssemblyDirectory()
        {
            string codeBase = Assembly.GetExecutingAssembly().CodeBase;
            var uri = new UriBuilder(codeBase);
            string path = Uri.UnescapeDataString(uri.Path);
            return Path.GetDirectoryName(path);
        }

        //IVsSolution.GetSolutionInfo
        public static string GetActiveProject()
        {
            dynamic projects = dte.ActiveSolutionProjects;
            //var solution = GetGlobalService(typeof(SVsSolution)) as IVsSolution;

            if (!dte.Solution.IsOpen)
                return null;

            //IVsSolutionBuildManager.get_StartupProject
            var msg = (Array)(dte.Solution.SolutionBuild as SolutionBuild2).StartupProjects;
            var startupProj = dte.Solution.Item((string)msg.GetValue(0));
            var projdll = Path.Combine(GetProjectOutputBuildFolder(startupProj), startupProj.Name) + ".dll";

            //solution.
            //startupProj.Properties.Item("OutputPath")

            return System.IO.Path.GetDirectoryName(dte.Solution.FullName);
        }

        public static void SetProjectID(int projectId, string uniqueName)
        {
            if (string.IsNullOrEmpty(uniqueName)) return;

            IVsHierarchy hierarchy;
            IVsSolution solution = (IVsSolution)Package.GetGlobalService(typeof(SVsSolution));
            solution.GetProjectOfUniqueName(uniqueName, out hierarchy);
            IVsBuildPropertyStorage buildPropertyStorage = hierarchy as IVsBuildPropertyStorage;

            buildPropertyStorage.SetPropertyValue("QCProjectID", String.Empty, (uint)_PersistStorageType.PST_PROJECT_FILE, projectId.ToString());

        }

        public static int GetProjectID(string uniqueName)
        {
            if (string.IsNullOrEmpty(uniqueName)) return 0;
            
            IVsHierarchy hierarchy;
            IVsSolution solution = (IVsSolution)Package.GetGlobalService(typeof(SVsSolution));
            solution.GetProjectOfUniqueName(uniqueName, out hierarchy);
            IVsBuildPropertyStorage buildPropertyStorage = hierarchy as IVsBuildPropertyStorage;

            string projectId;
            buildPropertyStorage.GetPropertyValue("QCProjectID", String.Empty, (uint)_PersistStorageType.PST_PROJECT_FILE, out projectId);

            int iProjId = 0;
            int.TryParse(projectId, out iProjId);

            return iProjId;
        }

        public static IEnumerable<dynamic> GetAllProjects()
        {
            foreach(Project prj in dte.Solution.Projects)
            {
                yield return new {
                    Id = GetProjectID(prj.UniqueName),
                    name = prj.Name,
                    uniqueName = prj.UniqueName,
                    path = Path.GetDirectoryName(prj.FullName)
                    //binpath = GetProjectOutputBuildFolder(prj)
                };
            }
        }

        public static void GetSelectedItem(out string classDll, out string className)
        {
            classDll = "";
            className = "";
            if (dte.SelectedItems.Count != 1) return;
            var selitem = dte.SelectedItems.Cast<SelectedItem>().FirstOrDefault();

            var startupProjDir = GetProjectOutputBuildFolder(selitem.ProjectItem.ContainingProject);
            classDll = Path.Combine(startupProjDir, selitem.ProjectItem.ContainingProject.Name) + ".dll";

            className = GetAlgorithmsList(selitem.ProjectItem.Document.FullName, classDll).FirstOrDefault();

            if (!File.Exists(classDll))
            {
                QCPluginUtilities.OutputCommandString("The algorithm binary not found: " + classDll, QCPluginUtilities.Severity.Error);
                classDll = null;
            }
            else
                QCPluginUtilities.OutputCommandString("Using algorithm binary: " + classDll, QCPluginUtilities.Severity.Info);

            if (className == null)
            {
                QCPluginUtilities.OutputCommandString("The algorithm class not found. Check that all relevant classes implement QuantConnect.Algorithm.QCAlgorithm.", QCPluginUtilities.Severity.Error);
                //TODO: show dialog with 2 dropdowns to choose correct algorithm class
            }
            else
                QCPluginUtilities.OutputCommandString("Using algorithm class: " + className, QCPluginUtilities.Severity.Info);
        }

        public static string GetStartupProjectOutputBinary()
        {
            var msg = (Array)(dte.Solution.SolutionBuild as SolutionBuild2).StartupProjects;
            var startupProjObj = dte.Solution.Item((string)msg.GetValue(0));
            var startupProjDir = GetProjectOutputBuildFolder(startupProjObj);

            var projdll = Path.Combine(startupProjDir, startupProjObj.Name) + ".dll";

            return projdll;
        }

        public static string GetStartupProjectName()
        {
            var msg = (Array)(dte.Solution.SolutionBuild as SolutionBuild2).StartupProjects;
            var startupProjObj = dte.Solution.Item((string)msg.GetValue(0));

            return startupProjObj.Name;
        }

        public static string GetStartupProjectDir()
        {
            var msg = (Array)(dte.Solution.SolutionBuild as SolutionBuild2).StartupProjects;
            var startupProjObj = dte.Solution.Item((string)msg.GetValue(0));
            string projectFolder = Path.GetDirectoryName(startupProjObj.FullName);

            return System.IO.Path.GetDirectoryName(dte.Solution.FullName);
        }

        /// <summary>
        /// Outputs the command string.
        /// Writes logs to OutputWindow pane, see <b>Microsoft.VisualStudio.Shell.Interop.SVsOutputWindow</b>
        /// </summary>
        /// <param name="text">The text.</param>
        /// <param name="caption">The caption.</param>
        static internal void OutputCommandString(string text, string caption, Severity severity)
        {
            // --- Get a reference to IVsOutputWindow. 
            if (outputWindow == null)
            {
                Debug.WriteLine(string.Format("[{0}] {1}: {2} ", severity, caption, text));
                return;
            }

            // --- Get the window pane for the general output. 
            var guidGeneral = Microsoft.VisualStudio.VSConstants.GUID_OutWindowDebugPane;
            IVsOutputWindowPane windowPane;

            if (Microsoft.VisualStudio.ErrorHandler.Failed(outputWindow.GetPane(ref guidGeneral, out windowPane)))
            {
                Debug.WriteLine(string.Format("[{0}] {1}: {2} ", severity, caption, text));
                return;
            }

            // --- As the last step, write to the output window pane 
            windowPane.SetName(AppTitle);
            windowPane.OutputString(string.Format("[{0}] {1} " + Environment.NewLine, severity, text));
            windowPane.Activate();
        }

        static internal void OutputCommandString(string text, Severity severity)
        {
            OutputCommandString(text, AppTitle, severity);
        }

        /// <summary>
        /// Creates the threaded wait dialog, see <b>Microsoft.VisualStudio.Shell.Interop.IVsThreadedWaitDialog2</b>
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="progress">The progress.</param>
        /// <param name="statustext">The statustext.</param>
        /// <param name="total">The total.</param>
        /// <returns>"Microsoft.VisualStudio.Shell.Interop.IVsThreadedWaitDialog2" instance</returns>
        static internal IVsThreadedWaitDialog2 CreateThreadedWaitDialog(string message, string progress, string statustext, int total)
        {
            //dlg = Utilities.CreateThreadedWaitDialog("Collecting information about changesets", "Starting to process changests...", "status", 100);
            //dlg.UpdateProgress("Collecting information about changesets", "Starting to process changesets...", "status", 0, 100, true, out bcanceled);
            //dlg.EndWaitDialog(out icanceled);

            IVsThreadedWaitDialog2 dlg = null;
            ErrorHandler.ThrowOnFailure(dialogFactory.CreateInstance(out dlg));

            ErrorHandler.ThrowOnFailure(
                dlg.StartWaitDialogWithPercentageProgress(AppTitle, message, progress, null, statustext, true, 0, total, 0));

            return dlg;
        }

        public static void UpdateLocalProject(List<QuantConnect.RestAPI.Models.QCFile> projectFiles, string ProjectName)
        {
            var proj = dte.Solution.Projects.Cast<Project>().FirstOrDefault(x => x.Name == ProjectName);
            if (proj == null)
                throw new Exception("Loacal project not found for name: " + ProjectName);

            var projectDir = Path.GetDirectoryName(proj.FullName);
            foreach (var file in projectFiles)
            {
                // For some reason the rest sevice introduces leading / in front of folders... Should be removed... work around for now.
                string fileName = file.Name.Replace('/', '\\').Trim('\\');
                string filePath = System.IO.Path.Combine(projectDir, fileName);
                string fileDir = System.IO.Path.GetDirectoryName(filePath);

                if (!System.IO.Directory.Exists(fileDir))
                {
                    System.IO.Directory.CreateDirectory(fileDir);
                }
                System.IO.File.WriteAllText(filePath, file.Code);

                //Add files to project:
                try
                {
                    proj.ProjectItems.AddFromFile(filePath);
                }
                catch (SystemException ex)
                {
                    OutputCommandString("Error adding files to local project: " + ex.ToString(), QCPluginUtilities.Severity.Error);
                }
            }

            proj.Save();
        }

        public static string GetProjectOutputBuildFolder(EnvDTE.Project proj)
        {
            string absoluteOutputPath = null;
            string projectFolder = Path.GetDirectoryName(proj.FullName);

            try
            {
                //Get the configuration manager of the project
                EnvDTE.ConfigurationManager configManager = proj.ConfigurationManager;

                if (configManager == null)
                    QCPluginUtilities.OutputCommandString("The project " + proj.Name + " doesn't have a configuration manager", QCPluginUtilities.Severity.Error);
                else
                {
                    //Get the active project configuration
                    EnvDTE.Configuration activeConfiguration = configManager.ActiveConfiguration;

                    //Get the output folder
                    string outputPath = activeConfiguration.Properties.Item("OutputPath").Value.ToString();

                    //The output folder can have these patterns:
                    //1) "\\server\folder"
                    //2) "drive:\folder"
                    //3) "..\..\folder"
                    //4) "folder"
                    if (outputPath.StartsWith(Path.DirectorySeparatorChar.ToString() + Path.DirectorySeparatorChar.ToString()))
                        //This is the case 1: "\\server\folder"
                        absoluteOutputPath = outputPath;
                    else if (outputPath.Length >= 2 && outputPath[1] == Path.VolumeSeparatorChar)
                        //This is the case 2: "drive:\folder"
                        absoluteOutputPath = outputPath;
                    else if (outputPath.IndexOf("..\\") > -1)
                    {
                        //This is the case 3: "..\..\folder"
                        while (outputPath.StartsWith("..\\"))
                        {
                            outputPath = outputPath.Substring(3);
                            projectFolder = Path.GetDirectoryName(projectFolder);
                        }

                        absoluteOutputPath = Path.Combine(projectFolder, outputPath);
                    }
                    else
                    {
                        //This is the case 4: "folder"
                        projectFolder = Path.GetDirectoryName(proj.FullName);
                        absoluteOutputPath = Path.Combine(projectFolder, outputPath);
                    }
                }
            }
            catch (Exception ex)
            {
                QCPluginUtilities.OutputCommandString("Get project output build folder error: " + ex.ToString(), QCPluginUtilities.Severity.Error);
            }

            return absoluteOutputPath;
        }

        public static IEnumerable<Tuple<K, dynamic, dynamic>>
            FullOuterJoin<K>(IEnumerable<KeyValuePair<K, dynamic>> coll1, IEnumerable<KeyValuePair<K, dynamic>> coll2)
        {
            var alookup = coll1.ToLookup(x => x.Key);
            var blookup = coll2.ToLookup(y => y.Key);

            var keys = new HashSet<K>(alookup.Select(p => p.Key));
            keys.UnionWith(blookup.Select(p => p.Key));

            var combproj = 
                from key in keys
                from xa in alookup[key].DefaultIfEmpty(new KeyValuePair<K, dynamic>())
                from xb in blookup[key].DefaultIfEmpty(new KeyValuePair<K, dynamic>())
                select new Tuple<K, dynamic, dynamic>(key, xa.Value, xb.Value);

            return combproj.ToList();
        }
    }
}
