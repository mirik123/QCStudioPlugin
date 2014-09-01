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

namespace QuantConnect.QCPlugin
{
    /******************************************************** 
    * CLASS DEFINITIONS
    *********************************************************/
    public partial class FormLogout : Form
    {
        public FormLogout()
        {
            InitializeComponent();
        }

        public void DeleteSavedCredentials(object sender, EventArgs e)
        {
            QuantConnectPlugin.RemoveCredentials();
            Close();
        }

        private void CloseWindow(object sender, EventArgs e)
        {
            Close();
        }

        private void Logout_Load(object sender, EventArgs e)
        {

        }
    }
}
