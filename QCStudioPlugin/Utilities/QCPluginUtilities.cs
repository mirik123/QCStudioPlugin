﻿/*
* Mark Babayev (https://github.com/mirik123) - Visual Studio extension utilities
*/

using Company.QCStudioPlugin;
using EnvDTE;
using EnvDTE80;
using Microsoft.VisualStudio;
using Microsoft.VisualStudio.ExtensionManager;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;
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
        
        static internal string AppTitle;
        static internal DTE2 dte;
        static internal IVsThreadedWaitDialogFactory dialogFactory;
        static internal IVsOutputWindow outputWindow;
        static internal string InstallPath;
        static internal RichTextBox rchOutputWnd = null;

        public static void Initialize(string AppTitle, DTE2 dte, IVsThreadedWaitDialogFactory dialogFactory, IVsOutputWindow outputWindow)
        {
            QCPluginUtilities.AppTitle = AppTitle;
            QCPluginUtilities.dialogFactory = dialogFactory;
            QCPluginUtilities.dte = dte;
            QCPluginUtilities.outputWindow = outputWindow;
            QCPluginUtilities.InstallPath = RetrieveAssemblyDirectory();
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
        static internal void OutputCommandString(string text, string caption)
        {
            // --- Build the string to write on the debugger and Output window. 
            var outputText = new StringBuilder();
            outputText.AppendFormat("{0}: {1} ", caption, text);
            outputText.AppendLine();
            // --- Get a reference to IVsOutputWindow. 
            if (outputWindow == null)
            {
                Debug.WriteLine(string.Format(CultureInfo.CurrentCulture, "{0}: {1} ", caption, text));
                return;
            }

            // --- Get the window pane for the general output. 
            var guidGeneral = Microsoft.VisualStudio.VSConstants.GUID_OutWindowDebugPane;
            IVsOutputWindowPane windowPane;

            if (Microsoft.VisualStudio.ErrorHandler.Failed(outputWindow.GetPane(ref guidGeneral, out windowPane)))
            {
                Debug.WriteLine(string.Format(CultureInfo.CurrentCulture, "{0}: {1} ", caption, text));
                return;
            }

            // --- As the last step, write to the output window pane 
            windowPane.OutputString(outputText.ToString());
            windowPane.Activate();
        }

        static internal void OutputCommandString(string text, Severity severity = Severity.Error)
        {
            if (rchOutputWnd != null)
            {
                rchOutputWnd.DeselectAll();
                rchOutputWnd.SelectionColor = severity == Severity.Error ? Color.Red : severity == Severity.Warning ? Color.Orange : SystemColors.WindowText;
                rchOutputWnd.AppendText(Environment.NewLine + text);
                rchOutputWnd.SelectionColor = SystemColors.WindowText;
            }

            OutputCommandString(text, AppTitle);
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
                    OutputCommandString("Error adding files to local project: " + ex.ToString());
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
                    MessageBox.Show("The project " + proj.Name + " doesn't have a configuration manager");
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
                MessageBox.Show(ex.ToString());
            }

            return absoluteOutputPath;
        }       
    }
}