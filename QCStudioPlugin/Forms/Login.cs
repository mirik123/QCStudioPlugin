/*
* QUANTCONNECT.COM - Democratizing Finance, Empowering Individuals,
* QuantConnect Visual Studio Plugin
*/

/**********************************************************
* USING NAMESPACES
**********************************************************/
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
using QuantConnect.RestAPI;
using QuantConnect.RestAPI.Models;

namespace QuantConnect.QCPlugin
{
    /******************************************************** 
    * CLASS DEFINITIONS
    *********************************************************/
    public partial class FormLogin : Form
    {
        public Action SuccessCallback;
        public string Email = QuantConnectPlugin.Email;
        public string Password = QuantConnectPlugin.Password;

        public FormLogin()
        {
            InitializeComponent();
        }

        public void SetCallBacks(Action success)
        {
            this.SuccessCallback = success;
        }
      
        private void SetCredentials_Load(object sender, EventArgs e)
        {
            string[] credentials = QuantConnectPlugin.LoadCredentials();
            textBoxEmail.Text = Email;
            textBoxPassword.Text = Password;
        }

        private void ButtonSave_Click(object sender, EventArgs e)
        {
            buttonLogin.Enabled = false;
            this.UseWaitCursor = true;
            Email = textBoxEmail.Text;
            Password = textBoxPassword.Text;

            if (checkBoxRememberCredentials.Checked)
            {
                QuantConnectPlugin.SaveCredentials(Email, Password);
            }

            try
            {
                Async.Add(new APIJob(APICommand.Authenticate, (loggedIn, errors) =>
                {
                    buttonLogin.SafeInvoke(d => d.Enabled = true);
                    this.SafeInvoke(d => d.UseWaitCursor = false);

                    if (!(bool)loggedIn)
                    {
                        MessageBox.Show("Wrong user name or password", "QuantConnect Login", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        QuantConnectPlugin.Email = Email;
                        QuantConnectPlugin.Password = Password;
                        this.SafeInvoke(d =>
                        {
                            if (d.SuccessCallback != null)
                            {
                                d.SuccessCallback();
                            }
                        });
                        this.SafeInvoke(d => d.Close());
                    }
                }, Email, Password));
            }
            catch
            {
                MessageBox.Show("Connection timeout.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
        }

        private void CloseWindow(object sender, EventArgs e)
        {
            Close();
        }
    }
}
