/*
* QUANTCONNECT.COM - Democratizing Finance, Empowering Individuals,
* QuantConnect Visual Studio Plugin
*/

using QuantConnect.QCStudioPlugin.Properties;
using EnvDTE80;
using Microsoft.VisualStudio;
using Microsoft.VisualStudio.ExtensionManager;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;
using QuantConnect.QCStudioPlugin;
using QuantConnect.QCStudioPlugin.Actions;
using QuantConnect.QCStudioPlugin.Forms;
using System;
using System.ComponentModel.Design;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.InteropServices;
using QCInterfaces;



namespace QuantConnect.QCStudioPlugin
{
    [PackageRegistration(UseManagedResourcesOnly = true)]
    [InstalledProductRegistration("#110", "#112", "1.0", IconResourceID = 400)]
    [ProvideMenuResource("Menus.ctmenu", 1)]
    [ProvideToolWindow(typeof(AdminPane), MultiInstances = false, Style = VsDockStyle.Tabbed, Transient = true, Orientation = ToolWindowOrientation.Bottom)]
    [ProvideToolWindow(typeof(QCClientPane), MultiInstances = false, Style = VsDockStyle.Tabbed, Transient = true, Orientation = ToolWindowOrientation.Top)]
    [ProvideOptionPage(typeof(OptionPageGrid), "QuantConnect Client", "General", 0, 0, true)]
    [Guid(GuidList.guidQCStudioPluginPkgString)]
    public sealed class QCStudioPluginPackage : Package
    {
        public QCStudioPluginPackage()
        {
            Debug.WriteLine(string.Format(CultureInfo.CurrentCulture, "Entering constructor for: {0}", this.ToString()));
        }

        private IVsWindowFrame GetToolWindowFrame<T>()
        {
            ToolWindowPane window = this.FindToolWindow(typeof(T), 0, true);
            if ((null == window) || (null == window.Frame))
            {
                QCPluginUtilities.OutputCommandString("Failed to initialize " + Resources.ToolWindowTitle, QCPluginUtilities.Severity.Error);
                throw new NotSupportedException(Resources.CanNotCreateWindow);
            }
            
            return (IVsWindowFrame)window.Frame;            
        }

        private void CustomInitialize()
        {
            string AppTitle = Resources.ToolWindowTitle;
            var dte = (DTE2)GetService(typeof(EnvDTE.DTE));
            var dialogFactory = GetService(typeof(SVsThreadedWaitDialogFactory)) as IVsThreadedWaitDialogFactory;
            var outputWindow = GetService(typeof(SVsOutputWindow)) as IVsOutputWindow;

            var page = (OptionPageGrid)GetDialogPage(typeof(OptionPageGrid));
            page.PropertyChanged += page_PropertyChanged;

            QCPluginUtilities.Initialize(AppTitle, dte, dialogFactory, outputWindow, this.GetPaneWindow);
        }

        void page_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case "UIBinaries":
                case "UIClassName":
                    var chartWindowFrame = (QCClientPane)this.FindToolWindow(typeof(QCClientPane), 0, false);
                    if (chartWindowFrame != null) chartWindowFrame.control = null;
                    break;
                case "PathBinaries2":
                    LeanActions.ResetLeanAndComposer();
                    break;
            }
        }

        internal ChartControl GetPaneWindow()
        {
            var chartWindowFrame = (QCClientPane)this.FindToolWindow(typeof(QCClientPane), 0, true);
            var frame = (IVsWindowFrame)chartWindowFrame.Frame;
            ErrorHandler.ThrowOnFailure(frame.Show());

            return chartWindowFrame.control;
        }


        /////////////////////////////////////////////////////////////////////////////
        // Overridden Package Implementation
        #region Package Members

        protected override void Initialize()
        {
            Debug.WriteLine (string.Format(CultureInfo.CurrentCulture, "Entering Initialize() of: {0}", this.ToString()));
            base.Initialize();

            // Add our command handlers for menu (commands must exist in the .vsct file)
            OleMenuCommandService mcs = GetService(typeof(IMenuCommandService)) as OleMenuCommandService;
            if ( null != mcs )
            {
                // Create the command for the tool window
                var toolwndCommandID = new CommandID(GuidList.guidQCStudioPluginCmdSet, (int)PkgCmdIDList.cmdidQCLocal);
                var menuToolWin = new OleMenuCommand((sender, e) => { QCPluginUtilities.ShowBacktestLocal(); }, toolwndCommandID);
                mcs.AddCommand(menuToolWin);

                toolwndCommandID = new CommandID(GuidList.guidQCStudioPluginCmdSet, (int)PkgCmdIDList.cmdidQCSaveLocal);
                menuToolWin = new OleMenuCommand(async (sender, e) =>
                {
                    OptionPageGrid page = (OptionPageGrid)GetDialogPage(typeof(OptionPageGrid));
                    await QCStudioPluginActions.SaveLocalBacktest(page.PathBinaries2, page.PathData2);
                }, toolwndCommandID);
                mcs.AddCommand(menuToolWin);

                toolwndCommandID = new CommandID(GuidList.guidQCStudioPluginCmdSet, (int)PkgCmdIDList.cmdidQCRemote);
                menuToolWin = new OleMenuCommand((sender, e) =>
                {
                    var windowFrame = GetToolWindowFrame<AdminPane>();
                    ErrorHandler.ThrowOnFailure(windowFrame.Show());
                }, toolwndCommandID);
                mcs.AddCommand(menuToolWin);
            }

            CustomInitialize();
        }
        #endregion

        //IVsShellPropertyEvents
        /*public int OnShellPropertyChange(int propid, object var)
        {
            if ((int)__VSSPROPID.VSSPROPID_Zombie == propid)
            {
                if ((bool)var == false)
                {
                    //Visual Studio is now ready and loaded up
                    var shellService = GetService(typeof(SVsShell)) as IVsShell;
                    if (shellService != null)
                        ErrorHandler.ThrowOnFailure(shellService.UnadviseShellPropertyChanges(cookie));

                    cookie = 0;

                    CustomInitialize();
                }
            }
            return VSConstants.S_OK;
        }*/
    }
}
