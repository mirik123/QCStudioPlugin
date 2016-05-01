/*
* QUANTCONNECT.COM - Democratizing Finance, Empowering Individuals,
* QuantConnect Visual Studio Plugin
*/

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
        public Func<string, string, Task> SuccessCallback;

        public FormLogin(string email, string password)
        {
            InitializeComponent();
            textBoxEmail.Text = email;
            textBoxPassword.Text = password;
        }

        private async void ButtonSave_Click(object sender, EventArgs e)
        {
            buttonLogin.Enabled = false;
            UseWaitCursor = true;
            progressBar1.Visible = true;

            string Email = textBoxEmail.Text;
            string Password = textBoxPassword.Text;

            if (SuccessCallback != null)
            {
                try
                {
                    await SuccessCallback(textBoxEmail.Text, textBoxPassword.Text);

                    Close();
                }
                catch(Exception ex)
                {
                    progressBar1.Visible = false;
                    buttonLogin.Enabled = true;
                    MessageBox.Show(this, ex.ToString(), "Authentication Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
