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
using QuantConnect.RestAPI;
using QuantConnect.RestAPI.Models;
using EnvDTE100;
using System.Threading;

namespace QuantConnect.QCPlugin
{
    public partial class FormCreateProject: Form
    {
        /// Load this Result 
        public int NewProjectID = 0;
        public string ProjectName = "";

        /// <summary>
        /// Create the form:
        /// </summary>
        public FormCreateProject()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Set a callback and show the login form.
        /// </summary>
        /// <param name="callback"></param>
        public void ShowLogin(Action callback)
        {
            FormLogin form = new FormLogin();
            form.SetCallBacks(callback);
            form.Show();
        }

        /// <summary>
        /// Create a new project.
        /// </summary>
        public void CreateProject(bool retry = false)
        {
            progressBar.Value = 10;
            labelMessage.Text = "Creating Project on QuantConnect...";

            // Create New Project on QC
            Async.Add(new APIJob(APICommand.NewProject, (projectIdObj, errors) =>
            {
                // Handle login and API errors:
                switch (QuantConnectPlugin.HandleErrors(errors))
                {
                    // Handle project specific actions with a login error:
                    case APIErrors.NotLoggedIn:
                        this.SafeInvoke(d => d.ShowLogin(() => { FormCreateProject form = new FormCreateProject(); form.StartPosition = FormStartPosition.CenterScreen; form.Show(); }));
                        this.SafeInvoke(d => d.Close());
                        return;
                }

                int projectId = projectIdObj == null ? 0 : Convert.ToInt32(projectIdObj);
                if (projectId == 0)
                {
                    //Show the error form
                    labelMessage.SafeInvoke(d => d.Text = "Sorry there was an error with your request. ");
                    pictureError.SafeInvoke(d => d.Visible = true);
                }
                else
                { 
                    //Else we have a new id, time to load the template:
                    labelMessage.SafeInvoke(d => d.Text = "Project Created, Opening...");
                    progressBar.SafeInvoke(d => d.Value = 90);

                    //Open the project and close the loading dialog.
                    QuantConnectPlugin.OpenProject(projectId, ProjectName, () => {
                        this.SafeInvoke(d => d.Close());
                        QuantConnectPlugin.SetButtonsState(true);
                    });
                }
            }, ProjectName));
        }


        /// <summary>
        /// Launch the backtest result form.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timer_Tick(object sender, EventArgs e)
        {
            if (this.NewProjectID != 0)
            {
                MessageBox.Show("Project Created: Load Project now --> " + NewProjectID);
            }
        }


        /// <summary>
        /// Create a new project on form load.
        /// </summary>
        private void FormCreateProject_Load(object sender, EventArgs e)
        {
            CreateProject();
        }
    }
}
