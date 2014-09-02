using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuantConnect.QCPlugin
{
    public partial class FormInputBox : Form
    {
        private Action<string> _okCallback = null;
        private Action _cancelCallback = null;

        public FormInputBox()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Set the prompt and call back for the dialog.
        /// </summary>
        /// <param name="title">Title of the input box</param>
        /// <param name="prompt">Prompt to show on the box</param>
        public void Set(string title, string prompt, Action<string> okCallback, Action cancelCallback)
        {
            this.labelPrompt.Text = prompt;
            this.Text = title;
            this._okCallback = okCallback;
            this._cancelCallback = cancelCallback;
        }

        /// <summary>
        /// Click the oK button
        /// </summary>
        private void buttonOK_Click(object sender, EventArgs e)
        {
            if (_okCallback != null) _okCallback(textInputData.Text);
            this.Close();
        }

        /// <summary>
        /// Cancel callback
        /// </summary>
        private void buttonCancel_Click(object sender, EventArgs e)
        {
            if (_cancelCallback != null) _cancelCallback();
            this.Close();
        }

        /// <summary>
        /// Scan the key for an enter key:
        /// </summary>
        private void textInputData_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((int)e.KeyChar == 13)
            {
                buttonOK_Click(new object(), new EventArgs());
            }
        }
    }
}
