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

namespace QuantConnect.QCStudioPlugin
{
    [PackageRegistration(UseManagedResourcesOnly = true)]
    [InstalledProductRegistration("#110", "#112", "1.0", IconResourceID = 400)]
    [ProvideMenuResource("Menus.ctmenu", 1)]
    [ProvideToolWindow(typeof(AdminPane), MultiInstances = false, Style = VsDockStyle.Tabbed, Transient = true, Orientation = ToolWindowOrientation.Bottom)]
    [ProvideToolWindow(typeof(ChartPane), MultiInstances = false, Style = VsDockStyle.Tabbed, Transient = true, Orientation = ToolWindowOrientation.Top)]
    [ProvideToolWindow(typeof(QCClientPane), MultiInstances = false, Style = VsDockStyle.Tabbed, Transient = true, Orientation = ToolWindowOrientation.Top)]
    [Guid(GuidList.guidQCStudioPluginPkgString)]
    public sealed class QCStudioPluginPackage : Package
    {
        uint cookie;

        public QCStudioPluginPackage()
        {
            Debug.WriteLine(string.Format(CultureInfo.CurrentCulture, "Entering constructor for: {0}", this.ToString()));
        }

        private IVsWindowFrame GetToolWindowFrame<T>()
        {
            ToolWindowPane window = this.FindToolWindow(typeof(T), 0, true);
            if ((null == window) || (null == window.Frame))
            {
                QCPluginUtilities.OutputCommandString("Failed to initialize " + Resources.ToolWindowTitle);
                throw new NotSupportedException(Resources.CanNotCreateWindow);
            }
            
            return (IVsWindowFrame)window.Frame;            
        }

        private string GetVSIXInstalledLocation()
        {
            //return "";
            
            // get ExtensionManager
            IVsExtensionManager manager = ServiceProvider.GlobalProvider.GetService(typeof(SVsExtensionManager)) as IVsExtensionManager;
            //foreach (IInstalledExtension extension in manager.GetInstalledExtensions())
            //    if(extension.Header.Name == "MyExtensionName")
            //        return extension.InstallPath;

            // get your extension by Product Id
            IInstalledExtension myExtension = manager.GetInstalledExtension(GuidList.guidQCStudioPluginPkgString);
            // get current version
            return myExtension.InstallPath;
        }

        private void CustomInitialize()
        {
            string AppTitle = Resources.ToolWindowTitle;
            string AppVersion = Resources.QuantConnectVersion;
            var dte = (DTE2)GetService(typeof(EnvDTE.DTE));
            var dialogFactory = GetService(typeof(SVsThreadedWaitDialogFactory)) as IVsThreadedWaitDialogFactory;
            var outputWindow = GetService(typeof(SVsOutputWindow)) as IVsOutputWindow;
            var chartWindowJSFrame = (ChartPane)this.FindToolWindow(typeof(ChartPane), 0, true);
            var chartWindowZedFrame = (QCClientPane)this.FindToolWindow(typeof(QCClientPane), 0, true);

            QCPluginUtilities.Initialize(AppTitle, AppVersion, dte, dialogFactory, outputWindow, chartWindowJSFrame, chartWindowZedFrame);
            QCStudioPluginActions.Initialize();
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
                CommandID toolwndCommandID = new CommandID(GuidList.guidQCStudioPluginCmdSet, (int)PkgCmdIDList.cmdidQCPane);
                MenuCommand menuToolWin = new MenuCommand((sender,e) => {
                    var windowFrame = GetToolWindowFrame<AdminPane>();
                    ErrorHandler.ThrowOnFailure(windowFrame.Show());
                }, toolwndCommandID);
                mcs.AddCommand( menuToolWin );
            }

            CustomInitialize();
        }
        #endregion

        //IVsShellPropertyEvents
        public int OnShellPropertyChange(int propid, object var)
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
        }
    }
}
