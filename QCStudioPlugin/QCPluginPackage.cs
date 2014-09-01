/*
* QUANTCONNECT.COM - Democratizing Finance, Empowering Individuals,
* QuantConnect Visual Studio Plugin
*/

/**********************************************************
* USING NAMESPACES
**********************************************************/
using System;
using System.Diagnostics;
using System.Globalization;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.ComponentModel.Design;
using Microsoft.Win32;
using Microsoft.VisualStudio;
using Microsoft.VisualStudio.Shell.Interop;
using Microsoft.VisualStudio.OLE.Interop;
using Microsoft.VisualStudio.Shell;
using System.Windows.Forms;
using System.Configuration;
using QuantConnect.RestAPI;
using QuantConnect.RestAPI.Models;
using EnvDTE80;
using EnvDTE;

namespace QuantConnect.QCPlugin
{

    [PackageRegistration(UseManagedResourcesOnly = true)]
    [InstalledProductRegistration("#110", "#112", "1.0", IconResourceID = 400)]
    [ProvideMenuResource("Menus.ctmenu", 1)]
    [Guid(GuidList.guidQCPluginPkgString)]
    public sealed class QCPluginPackage : Package
    {
        /// Reference to Visual Studio
        public static DTE2 ApplicationObject;

        /// Reference to our Menu Buton Objects
        public static Dictionary<string, MenuCommand> Commands;

        /// <summary>
        /// Plugin Constructor:
        /// </summary>
        public QCPluginPackage()
        {
            Debug.WriteLine(string.Format(CultureInfo.CurrentCulture, "Entering constructor for: {0}", this.ToString()));
            Commands = new Dictionary<string, MenuCommand>();
            // If the username and password files exist, login:
            QuantConnectPlugin.Initialize();
        }


        /////////////////////////////////////////////////////////////////////////////
        // Overridden Package Implementation
        #region Package Members

        /// <summary>
        /// Initialization of the package; this method is called right after the package is sited, so this is the place
        /// where you can put all the initialization code that rely on services provided by VisualStudio.
        /// </summary>
        protected override void Initialize()
        {
            Debug.WriteLine (string.Format(CultureInfo.CurrentCulture, "Entering Initialize() of: {0}", this.ToString()));
            base.Initialize();

            // IDE
            ApplicationObject = (DTE2)GetService(typeof(DTE));

            Commands.Clear();

            // Add our command handlers for menu (commands must exist in the .vsct file)
            OleMenuCommandService mcs = GetService(typeof(IMenuCommandService)) as OleMenuCommandService;
            if ( null != mcs )
            {
                // Create the command for each menu item.
                CommandID menuCommandID = new CommandID(GuidList.guidQCPluginCmdSet, (int)PkgCmdIDList.cmdIdQuantConnect);
                MenuCommand menuItem = new MenuCommand(MenuItemCallback, menuCommandID);
                mcs.AddCommand(menuItem);

                CommandID setCredentialsID = new CommandID(GuidList.guidQCPluginCmdSet, (int)PkgCmdIDList.setCredentials);
                Commands.Add("SetCredentials", new MenuCommand(SetCredentialsItemCallback, setCredentialsID));
                mcs.AddCommand(Commands["SetCredentials"]);

                CommandID newProjectID = new CommandID(GuidList.guidQCPluginCmdSet, (int)PkgCmdIDList.newProject);
                Commands.Add("NewProject", new MenuCommand(NewProjectCallback, newProjectID));
                mcs.AddCommand(Commands["NewProject"]);

                CommandID openProjectID = new CommandID(GuidList.guidQCPluginCmdSet, (int)PkgCmdIDList.openProject);
                MenuCommand openProjectItem = new MenuCommand(OpenProjectItemCallback, openProjectID);
                mcs.AddCommand(openProjectItem);

                CommandID backtestID = new CommandID(GuidList.guidQCPluginCmdSet, (int)PkgCmdIDList.backtest);
                Commands.Add("Backtest", new MenuCommand(BacktestItemCallback, backtestID));
                mcs.AddCommand(Commands["Backtest"]);

                CommandID logoutID = new CommandID(GuidList.guidQCPluginCmdSet, (int)PkgCmdIDList.logout);
                Commands.Add("Logout", new MenuCommand(LogoutItemCallback, logoutID));
                mcs.AddCommand(Commands["Logout"]);

                CommandID deleteID = new CommandID(GuidList.guidQCPluginCmdSet, (int)PkgCmdIDList.delete);
                Commands.Add("Delete", new MenuCommand(DeleteItemCallback, deleteID));
                mcs.AddCommand(Commands["Delete"]);

                CommandID saveID = new CommandID(GuidList.guidQCPluginCmdSet, (int)PkgCmdIDList.save);
                Commands.Add("Save", new MenuCommand(SaveItemCallback, saveID));
                mcs.AddCommand(Commands["Save"]);
            }
            QuantConnectPlugin.SetButtonsState(false);
        }
        #endregion

        /// <summary>
        /// Execute Menu Button Click:
        /// </summary>
        private void MenuItemCallback(object sender, EventArgs e)
        {
            // Show a Message Box to prove we were here
            IVsUIShell uiShell = (IVsUIShell)GetService(typeof(SVsUIShell));
            Guid clsid = Guid.Empty;
            int result;
            Microsoft.VisualStudio.ErrorHandler.ThrowOnFailure(uiShell.ShowMessageBox(
                       0,
                       ref clsid,
                       "QCPlugin",
                       string.Format(CultureInfo.CurrentCulture, "Inside {0}.MenuItemCallback()", this.ToString()),
                       string.Empty,
                       0,
                       OLEMSGBUTTON.OLEMSGBUTTON_OK,
                       OLEMSGDEFBUTTON.OLEMSGDEFBUTTON_FIRST,
                       OLEMSGICON.OLEMSGICON_INFO,
                       0,        // false
                       out result));
        }

        /// <summary>
        /// Click on Set Credentials (Currently hidden).
        /// </summary>
        private void SetCredentialsItemCallback(object sender, EventArgs e)
        {
            QuantConnectPlugin.ShowSaveCredentials();
        }

        /// <summary>
        /// Shwo the new project dialog:
        /// </summary>
        private void NewProjectCallback(object sender, EventArgs e)
        {
            QuantConnectPlugin.ShowNewProject();
        }

        /// <summary>
        /// Click on Open Projects
        /// </summary>
        private void OpenProjectItemCallback(object sender, EventArgs e)
        {
            QuantConnectPlugin.ShowProjects();
        }

        /// <summary>
        /// Click on Backtest.
        /// </summary>
        private void BacktestItemCallback(object sender, EventArgs e)
        {
            QuantConnectPlugin.ShowLoadBacktest();
        }

        /// <summary>
        /// Click on Save Project
        /// </summary>
        private void SaveItemCallback(object sender, EventArgs e)
        {
            QuantConnectPlugin.SaveToQC(true);
        }

        /// <summary>
        /// Click on Delete Project
        /// </summary>
        private void DeleteItemCallback(object sender, EventArgs e)
        {
            QuantConnectPlugin.DeleteProject();
        }

        /// <summary>
        /// Logout Menu Option
        /// </summary>
        private void LogoutItemCallback(object sender, EventArgs e)
        {
            QuantConnectPlugin.ShowLogout();
        }
    }
}
