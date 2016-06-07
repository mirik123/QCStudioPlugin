/*
* QUANTCONNECT.COM - Democratizing Finance, Empowering Individuals,
* QuantConnect Visual Studio Plugin
*/

using QuantConnect.QCStudioPlugin;
/**********************************************************
* USING NAMESPACES
**********************************************************/
using System;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuantConnect.QCPlugin
{
    /******************************************************** 
    * CLASS DEFINITIONS
    *********************************************************/
    public partial class FormLogin : Form
    {
        public Func<string, string, string, string, bool, Task> SuccessCallback;

        public FormLogin(string email, string password, string uid, string authtoken)
        {
            InitializeComponent();
            textBoxEmail.Text = email;
            textBoxPassword.Text = password;
            txtUID.Text = uid;
            txtAuthToken.Text = authtoken;
        }

        private async void ButtonSave_Click(object sender, EventArgs e)
        {
            buttonLogin.Enabled = false;
            UseWaitCursor = true;
            progressBar1.Visible = true;

            if (SuccessCallback != null)
            {
                try
                {
                    await SuccessCallback(textBoxEmail.Text, textBoxPassword.Text, txtUID.Text, txtAuthToken.Text, checkBoxRememberCredentials.Checked);

                    Close();
                }
                catch(Exception ex)
                {
                    progressBar1.Visible = false;
                    buttonLogin.Enabled = true;
                    QCPluginUtilities.OutputCommandString("Authentication Error: " + ex.ToString(), QCPluginUtilities.Severity.Error);
                }
            }
            else
                Close();
        }

        private void CloseWindow(object sender, EventArgs e)
        {
            Close();
        }
    }
}
