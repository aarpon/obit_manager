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

        public InstanceConfigurator(Instance instance)
        {
            this.mInstance = instance;

            InitializeComponent();

            this.UpdateUI();
        }

        private void UpdateUI()
        {
            this.groupBoxInstance.Text = this.mInstance.ClientRef.ConfigurationName;
            this.comboBoxUserFolder.Text = this.mInstance.ClientRef.UserDataDir;
            this.comboBoxDatamoverIncomingFolder.Text = this.mInstance.DatamoverRef.IncomingTarget;
            this.comboBoxServer.Text = this.mInstance.ServerRef.Label;
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
    }
}
