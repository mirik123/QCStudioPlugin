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
using System.Configuration;

namespace QuantConnect.QCPlugin
{
    /******************************************************** 
    * CLASS DEFINITIONS
    *********************************************************/
    public partial class FormOpenProject : Form
    {      
        static string projectName = "";   

        public FormOpenProject()
        {
            InitializeComponent();
        }

        public void ShowLogin(Action callback)
        {
            FormLogin form = new FormLogin();
            form.SetCallBacks(callback);
            form.Show();
        }

        private void OpenProjects_Load(object sender, EventArgs e)
        {
            // Load projects
            buttonOpen.Enabled = false;
            this.UseWaitCursor = true;
            List<Project> projectList = new List<Project>();

            try
            {
                Async.Add(new APIJob(APICommand.ProjectList, (projectfiles, errors) =>
                {
                    //Handle login and API errors:
                    switch (QuantConnectPlugin.HandleErrors(errors))
                    {
                        //Handle project specific actions with a login error:
                        case APIErrors.NotLoggedIn:
                            this.SafeInvoke(d => d.ShowLogin(() => { FormOpenProject form = new FormOpenProject(); form.StartPosition = FormStartPosition.CenterScreen; form.Show(); }));
                            this.SafeInvoke(d => d.Close());
                            return;
                    }

                    listViewProjects.SafeInvoke(d => d.Items.Clear());
                    listViewProjects.SafeInvoke(d => d.BeginUpdate());

                    foreach (var project in (List<Project>)projectfiles)
                    {
                        ListViewItem item = new ListViewItem();
                        item.Text = project.Id.ToString();

                        ListViewItem.ListViewSubItem subitem = new ListViewItem.ListViewSubItem();
                        subitem.Text = project.Name.ToString();
                        item.SubItems.Add(subitem);

                        subitem = new ListViewItem.ListViewSubItem();
                        subitem.Text = project.Modified.ToString();
                        item.SubItems.Add(subitem);

                        listViewProjects.SafeInvoke(d => d.Items.Add(item));
                    }
                    listViewProjects.SafeInvoke(d => d.EndUpdate());
                    buttonOpen.SafeInvoke(d => d.Enabled = true);
                    this.SafeInvoke(d => d.UseWaitCursor = false);
                    this.SafeInvoke(d => d.SizeLastColumn(listViewProjects));
                }));
            }
            catch
            {
                MessageBox.Show("Connection timeout.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
                        
        }


        /// <summary>
        /// Auto resize the columns
        /// </summary>
        /// <param name="lv"></param>
        private void SizeLastColumn(ListView lv)
        {
            lv.Columns[lv.Columns.Count - 1].Width = -2;
        }



        /// <summary>
        /// Download the file and open a new visual studio solution
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OpenBtn_Click(object sender, EventArgs e)
        {
            int selectedProject = 0;
            if (listViewProjects.SelectedItems.Count == 0)
            {
                return;
            }

            selectedProject = Convert.ToInt16(listViewProjects.SelectedItems[0].Text);
            projectName = listViewProjects.FocusedItem.SubItems[1].Text;

            buttonOpen.Enabled = false;
            this.UseWaitCursor = true;

            //Run the function that downloads and handles the projects
            bool success = QuantConnectPlugin.OpenProject(selectedProject, projectName, () => {
                buttonOpen.SafeInvoke((d)=> d.Enabled = true);
                this.SafeInvoke((d)=>d.UseWaitCursor = false);
                this.SafeInvoke((d) => d.Close());
                QuantConnectPlugin.SetButtonsState(true);
            });
            if (!success)
            {
                buttonOpen.Enabled = true;
                this.UseWaitCursor = false;
                QuantConnectPlugin.ProjectID = 0;
            }
        }


        /// <summary>
        /// Handle a resize event of the project columns:
        /// </summary>
        private void listViewProjects_Resize(object sender, EventArgs e)
        {
            SizeLastColumn((ListView)sender);
        }
    }
}
