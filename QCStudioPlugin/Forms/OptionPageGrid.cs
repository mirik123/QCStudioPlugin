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

namespace QuantConnect.QCStudioPlugin
{

    /// <summary>
    /// Extensions DialogPage
    /// </summary>
    [ClassInterface(ClassInterfaceType.AutoDual)]
    [CLSCompliant(false), ComVisible(true)]
    public class OptionPageGrid : DialogPage
    {
        private string _UIBinaries = "QCTerminalControl.dll";
        private string _UIClassName = "QCTerminalControl.JSChartControl";
        public string _pathBinaries;
        
        [Category("Lean")]
        [DisplayName("Path to Lean binaries")]
        [Editor("System.Windows.Forms.Design.FolderNameEditor, System.Design, Version=1.0.5000.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
        public string pathBinaries {
            get {
                return _pathBinaries;
            }
            set {
                if (_pathBinaries != value)
                {
                    _pathBinaries = value;
                    QCStudioPluginActions.UpdateLeanAndComposer(value);
                }
            }
        }

        [Category("Lean")]
        [DisplayName("Path to Lean Data")]
        [Editor("System.Windows.Forms.Design.FolderNameEditor, System.Design, Version=1.0.5000.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
        public string pathData { get; set;}

        [Category("Lean")]
        [DisplayName("Path to UI library")]
        [Editor("System.Windows.Forms.Design.FileNameEditor, System.Design, Version=1.0.5000.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
        public string UIBinaries {
            get { 
                return _UIBinaries;
            }
            set {
                if (string.IsNullOrEmpty(value)) value = "QCTerminalControl.dll";
                if (_UIBinaries != value)
                {
                    _UIBinaries = value;
                    UpdateChartControl();
                }
            } 
        }

        [Category("Lean")]
        [DisplayName("Full class name for UI library")]
        public string UIClassName {
            get {
                return _UIClassName;
            }
            set {
                if (string.IsNullOrEmpty(value)) value = "QCTerminalControl.JSChartControl";
                if (_UIClassName != value)
                {
                    _UIClassName = value;
                    UpdateChartControl();
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

        private void UpdateChartControl()
        {
            if (string.IsNullOrEmpty(_UIClassName) || string.IsNullOrEmpty(_UIBinaries))
                QCPluginUtilities.chartWindowFrame.control = null;
            else
                QCPluginUtilities.chartWindowFrame.control = Activator.CreateInstanceFrom(_UIBinaries, _UIClassName).Unwrap() as ChartControl;
        }
    }
}
