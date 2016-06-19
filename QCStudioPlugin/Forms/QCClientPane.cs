﻿/*
* QUANTCONNECT.COM - Democratizing Finance, Empowering Individuals,
* QuantConnect Visual Studio Plugin
*/

using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;
using QuantConnect.RestAPI.Models;
using System;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace QuantConnect.QCStudioPlugin.Forms
{
    /// <summary>
    /// This class inherites form ToolWindowPane and adds to it <see cref="QCClientControl"/>.
    /// </summary>
    [Guid("CFA1BDE6-3695-4B39-BBDE-65224B17681A")]
    public class QCClientPane : ToolWindowPane
    {
        // Control that will be hosted in the tool window
        private ChartControl _control = null;

        // Caching our output window pane
        //private IVsOutputWindowPane outputWindowPane = null;

        /// <summary>
        /// Constructor for ToolWindowPane.
        /// Initialization that depends on the package or that requires access
        /// to VS services should be done in OnToolWindowCreated.
        /// </summary>
        public QCClientPane()
            : base(null)
        {
            this.Caption = QCPluginUtilities.ChartTitle;
            this.BitmapResourceID = 700;
            this.BitmapIndex = 1;
        }

        /// <summary>
        /// This property returns the handle to the user control that should
        /// be hosted in the Tool Window.
        /// </summary>
        public override IWin32Window Window
        {
            get { return (IWin32Window)_control; }
        }

        /// <summary>
        /// This is called after our control has been created and sited.
        /// This is a good place to initialize the control with data gathered
        /// from Visual Studio services.
        /// </summary>
        public override void OnToolWindowCreated()
        {
            base.OnToolWindowCreated();

            
            ((IVsWindowFrame)this.Frame).SetProperty((int)__VSFPROPID.VSFPROPID_BitmapResource, 700);
            ((IVsWindowFrame)this.Frame).SetProperty((int)__VSFPROPID.VSFPROPID_BitmapIndex, 1);
            ((IVsWindowFrame)this.Frame).SetProperty((int)__VSFPROPID.VSFPROPID_FrameMode, VSFRAMEMODE.VSFM_Dock);


            // Display the pane as the first tab inside the VS IDE.
            //int result = ((IVsWindowFrame)this.Frame).SetProperty((int)__VSFPROPID.VSFPROPID_FrameMode, VSFRAMEMODE.VSFM_MdiChild);
            //if (result != VSConstants.S_OK)
            //{
            //MessageBox.Show(error);
            //}

            // Register to the window events
            //handle window pane events - WindowStatus class should implement IVsWindowFrameNotify3
            //WindowStatus windowFrameEventsHandler = new WindowStatus();
            //ErrorHandler.ThrowOnFailure(((IVsWindowFrame)this.Frame).SetProperty((int)__VSFPROPID.VSFPROPID_ViewHelper, (IVsWindowFrameNotify3)windowFrameEventsHandler));
        }

        /// <summary>
        /// Called when pane is closed.
        /// </summary>
        protected override void OnClose()
        {
            base.OnClose();

            _control.Dispose();
            _control = null;
        }

        /// <summary>
        /// Gets or sets the UserControl object.
        /// </summary>
        /// <value>
        /// The <see cref="QCClientControl"/> control object.
        /// </value>
        public ChartControl control
        {
            get
            {
                return _control;
            }
            set
            {
                _control = value;
            }
        }
    }
}
