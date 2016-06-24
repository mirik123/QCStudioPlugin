/*
* QUANTCONNECT.COM - Democratizing Finance, Empowering Individuals,
* QuantConnect Visual Studio Plugin
*/

using Microsoft.VisualStudio.Shell;
using QuantConnect.QCStudioPlugin.Actions;
using QuantConnect.RestAPI.Models;
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
        private string _UIBinaries = "QCTerminalControl.dll";
        private string _UIClassName = "QCTerminalControl.JSChartControl";
        public string _pathBinaries;
        
        [Category("Lean")]
        [DisplayName("Path to Lean binaries")]
        [Editor("System.Windows.Forms.Design.FolderNameEditor, System.Design, Version=1.0.5000.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
        public string PathBinaries {
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
        [DisplayName("Path to Lean Data")]
        [Editor("System.Windows.Forms.Design.FolderNameEditor, System.Design, Version=1.0.5000.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
        public string PathData { get; set;}

        [Category("UI")]
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
        [DisplayName("Full class name for UI library")]
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

        /// <summary>
        /// Initializes a new instance of the <see cref="OptionPageGrid"/> class.
        /// </summary>
        public OptionPageGrid()
            : base()
        {
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
