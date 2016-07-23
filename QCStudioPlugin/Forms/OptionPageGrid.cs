/*
* QUANTCONNECT.COM - Democratizing Finance, Empowering Individuals,
* QuantConnect Visual Studio Plugin
*/

using Microsoft.VisualStudio.Shell;
using QuantConnect.QCStudioPlugin.Actions;

using System;
using System.ComponentModel;
using System.Drawing.Design;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace QuantConnect.QCStudioPlugin
{

    /// <summary>
    /// Extensions DialogPage
    /// </summary>
    [ClassInterface(ClassInterfaceType.AutoDual)]
    [CLSCompliant(false), ComVisible(true)]
    public class OptionPageGrid : DialogPage, INotifyPropertyChanged
    {
        private string _UIBinaries;
        private string _UIClassName;
        public string _pathBinaries;

        [Category("Lean")]
        [DefaultValue("")]
        [DisplayName("Path to Lean binaries")]
        [Editor("QuantConnect.QCStudioPlugin.VistaFolderBrowserEditor", typeof(UITypeEditor))]
        public string PathBinaries2 {
            get {
                return _pathBinaries;
            }
            set {
                if (_pathBinaries != value)
                {
                    _pathBinaries = value;
                    OnPropertyChanged("PathBinaries");
                }
            }
        }

        [Category("Lean")]
        [DefaultValue("")]
        [DisplayName("Path to Lean Data")]
        [Editor("QuantConnect.QCStudioPlugin.VistaFolderBrowserEditor", typeof(UITypeEditor))]
        public string PathData2 { get; set;}

        [Category("UI")]
        [DefaultValue("QCTerminalControl.dll")]
        [DisplayName("Path to UI library")]
        [Editor("System.Windows.Forms.Design.FileNameEditor, System.Design, Version=1.0.5000.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
        public string UIBinaries {
            get { 
                return _UIBinaries;
            }
            set {
                if (_UIBinaries != value)
                {
                    _UIBinaries = value;
                    OnPropertyChanged("UIBinaries");
                }
            } 
        }

        [Category("UI")]
        [DefaultValue("QCTerminalControl.JSChartControl")]
        [DisplayName("Full class name for UI library")]
        [Editor("QuantConnect.QCStudioPlugin.ClassListEditor", typeof(UITypeEditor))]
        public string UIClassName {
            get {
                return _UIClassName;
            }
            set {
                if (_UIClassName != value)
                {
                    _UIClassName = value;
                    OnPropertyChanged("UIClassName");
                }
            } 
        }

        [Category("Configuration")]
        [DisplayName("algorithm-language")]
        [DefaultValue("CSharp")]
        public string AlgorithmLanguage { get; set; }

        [Category("Configuration")]
        [DisplayName("live-mode")]
        [DefaultValue(false)]
        public bool LiveMode { get; set; }

        [Category("Configuration")]
        [DisplayName("debug-mode")]
        [DefaultValue(true)]
        public bool DebugMode { get; set; }

        [Category("Configuration")]
        [DisplayName("messaging-handler")]
        [DefaultValue("QuantConnect.Messaging.EventMessagingHandler")]
        public string MessagingHandler { get; set; }

        [Category("Configuration")]
        [DisplayName("job-queue-handler")]
        [DefaultValue("QuantConnect.Queues.JobQueue")]
        public string JobQueueHandler { get; set; }

        [Category("Configuration")]
        [DisplayName("api-handler")]
        [DefaultValue("QuantConnect.Api.Api")]
        public string ApiHandler { get; set; }

        [Category("Configuration")]
        [DisplayName("result-handler")]
        [DefaultValue("QuantConnect.Lean.Engine.Results.BacktestingResultHandler")]
        public string ResultHandler { get; set; }

        [Category("Configuration")]
        [DisplayName("setup-handler")]
        [DefaultValue("QuantConnect.Lean.Engine.Setup.ConsoleSetupHandler")]
        public string SetupHandler { get; set; }

        [Category("Configuration")]
        [DisplayName("data-feed-handler")]
        [DefaultValue("QuantConnect.Lean.Engine.DataFeeds.FileSystemDataFeed")]
        public string DataFeedHandler { get; set; }

        [Category("Configuration")]
        [DisplayName("real-time-handler")]
        [DefaultValue("QuantConnect.Lean.Engine.RealTime.BacktestingRealTimeHandler")]
        public string RealTimeHandler { get; set; }

        [Category("Configuration")]
        [DisplayName("transaction-handler")]
        [DefaultValue("QuantConnect.Lean.Engine.TransactionHandlers.BacktestingTransactionHandler")]
        public string TransactionHandler { get; set; }

        [Category("Configuration")]
        [DisplayName("log-handler")]
        [DefaultValue("QuantConnect.Logging.QueueLogHandler")]
        public string LogHandler { get; set; }

        [Category("Configuration")]
        [DisplayName("plugin-directory")]
        [DefaultValue("")]
        [Editor("QuantConnect.QCStudioPlugin.VistaFolderBrowserEditor", typeof(UITypeEditor))]
        public string PluginDirectory { get; set; }

        [Category("Configuration")]
        [DisplayName("history-provider")]
        [DefaultValue("SubscriptionDataReaderHistoryProvider")]
        public string HistoryProvider { get; set; }

        [Category("Configuration")]
        [DisplayName("command-queue-handler")]
        [DefaultValue("EmptyCommandQueueHandler")]
        public string CommandQueueHandler { get; set; }

        [Category("Configuration")]
        [DisplayName("map-file-provider")]
        [DefaultValue("LocalDiskMapFileProvider")]
        public string MapFileProvider { get; set; }

        [Category("Configuration")]
        [DisplayName("factor-file-provider")]
        [DefaultValue("LocalDiskFactorFileProvider")]
        public string FactorFileProvider { get; set; }

        [Category("Configuration")]
        [DisplayName("version-id")]
        [DefaultValue("")]
        public string VersionId { get; set; }

        [Category("Configuration")]
        [DisplayName("algorithm-manager-time-loop-maximum")]
        [DefaultValue(10)]
        public int AlgorithmManagerTimeLoopMaximum { get; set; }

        [Category("Configuration")]
        [DisplayName("ironpython-location")]
        [DefaultValue("../ironpython/Lib")]
        [Editor("QuantConnect.QCStudioPlugin.VistaFolderBrowserEditor", typeof(UITypeEditor))]
        public string IronpythonLocation { get; set; }

        [Category("Configuration")]
        [DisplayName("forward-console-messages")]
        [DefaultValue(true)]
        public bool ForwardConsoleMessages { get; set; }

        [Category("Configuration")]
        [DisplayName("send-via-api")]
        [DefaultValue(false)]
        public bool SendViaApi { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="OptionPageGrid"/> class.
        /// </summary>
        public OptionPageGrid()
            : base()
        {
            foreach (PropertyDescriptor property in TypeDescriptor.GetProperties(this))
                property.ResetValue(this);
        }

        /*protected override IWin32Window Window
        {
            get
            {
                MyUserControl page = new MyUserControl();
                page.optionsPage = this;
                page.Initialize();
                return page;
            }
        }*/

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }
    }
}
