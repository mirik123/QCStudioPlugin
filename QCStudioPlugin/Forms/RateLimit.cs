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
using System.Diagnostics;

namespace QuantConnect.QCPlugin
{
    /******************************************************** 
    * CLASS DEFINITIONS
    *********************************************************/
    public partial class FormRateLimit : Form
    {
        public FormRateLimit()
        {
            InitializeComponent();
        }

        private void UpgradeBtn_Click(object sender, EventArgs e)
        {
            Process.Start("https://www.quantconnect.com/upgrade");
            Close();
        }

        private void CancelBtn_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void RateLimit_Load(object sender, EventArgs e)
        {

        }
    }
}
