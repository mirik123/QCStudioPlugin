using Microsoft.VisualStudio.Shell;
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
        [Category("Lean")]
        [DisplayName("Path to binaries")]
        [Editor("System.Windows.Forms.Design.FolderNameEditor, System.Design, Version=1.0.5000.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
        public string pathBinaries { get; set;}

        [Category("Lean")]
        [DisplayName("Path to local Data")]
        [Editor("System.Windows.Forms.Design.FolderNameEditor, System.Design, Version=1.0.5000.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
        public string pathData { get; set;}

        /// <summary>
        /// Initializes a new instance of the <see cref="OptionPageGrid"/> class.
        /// </summary>
        public OptionPageGrid()
            : base()
        {
        }
    }
}
