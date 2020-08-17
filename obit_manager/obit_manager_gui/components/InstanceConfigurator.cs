using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using obit_manager_gui.dialogs;
using obit_manager_settings.components;

namespace obit_manager_gui.components
{
    public partial class InstanceConfigurator :  UserControl
    {
        private Instance mInstance;

        public InstanceConfigurator()
        {
            InitializeComponent();
            
        }

        public void SetInstance(Instance instance)
        {
            this.mInstance = instance;

            this.UpdateUI();
        }

        // Refresh the control
        public void Refresh()
        {
            this.UpdateUI();
        }

        private void UpdateUI()
        {
            if (this.mInstance == null)
            {
                this.groupBoxInstance.Text = "";
                this.comboBoxUserFolder.Text = "";
                this.comboBoxDatamoverIncomingFolder.Text = "";
                this.comboBoxServer.Text = "";

            }
            else
            {
                this.groupBoxInstance.Text = this.mInstance.ClientRef.ConfigurationName;
                this.comboBoxUserFolder.Text = this.mInstance.ClientRef.UserDataDir;
                this.comboBoxDatamoverIncomingFolder.Text = this.mInstance.DatamoverRef.IncomingTarget;
                this.comboBoxServer.Text = this.mInstance.ServerRef.Label;
            }
        }

        private void buttonServerEdit_Click(object sender, EventArgs e)
        {
            using (var form = new ServerConfigurationDialog(this.mInstance.ServerRef))
            {
                var result = form.ShowDialog();
                if (result == DialogResult.OK)
                {
                    // Get the updated Server
                    Server updatedServer = form.Result;

                    // Set it
                    this.mInstance.ServerRef = updatedServer;

                    // Update the corresponding field
                    this.comboBoxServer.Text = this.mInstance.ServerRef.Label;
                }
            }
        }

        private void buttonUserFolderEdit_Click(object sender, EventArgs e)
        {
            using (var form = new AnnotatiolToolConfigurationDialog(this.mInstance.ClientRef))
            {
                var result = form.ShowDialog();
                if (result == DialogResult.OK)
                {
                    // Get the updated Client
                    Client updatedClient = form.Result;

                    // Set it
                    this.mInstance.ClientRef = updatedClient;

                    // Update the corresponding field
                    this.comboBoxUserFolder.Text = this.mInstance.ClientRef.UserDataDir;
                }
            }
        }

        private void buttonDatamoverIncomingFolderEdit_Click(object sender, EventArgs e)
        {
            using (var form = new DatamoverConfigurationDialog(this.mInstance.DatamoverRef))
            {
                var result = form.ShowDialog();
                if (result == DialogResult.OK)
                {
                    // Get the updated Client
                    Datamover updatedDatamover = form.Result;

                    // Set it
                    this.mInstance.DatamoverRef = updatedDatamover;

                    // Update the corresponding field
                    this.comboBoxDatamoverIncomingFolder.Text = this.mInstance.DatamoverRef.IncomingTarget;
                }
            }
        }
    }
}
