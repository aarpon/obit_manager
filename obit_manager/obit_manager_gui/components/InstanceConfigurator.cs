using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using obit_manager_gui.dialogs;
using obit_manager_settings;
using obit_manager_settings.components;

namespace obit_manager_gui.components
{
    public partial class InstanceConfigurator :  UserControl
    {
        // Reference to the SettingsManager
        private SettingsManager mSettingsManager;

        public InstanceConfigurator()
        {
            InitializeComponent();
        }

        public void SetConfiguration(SettingsManager settingsManager)
        {
            this.mSettingsManager = settingsManager;

            this.UpdateUI();
        }


        // Refresh the control
        public void Refresh()
        {
            this.UpdateUI();
        }

        private void UpdateUI()
        {
            // Fill the pull down menus
            this.comboBoxUserFolder.Items.Clear();
            foreach (string clientString in this.mSettingsManager.ClientStrings)
            {
                this.comboBoxUserFolder.Items.Add(clientString);
            }

            this.comboBoxDatamoverIncomingFolder.Items.Clear();
            foreach (string datamoverString in this.mSettingsManager.DatamoverStrings)
            {
                this.comboBoxDatamoverIncomingFolder.Items.Add(datamoverString);
            }

            this.comboBoxServer.Items.Clear();
            foreach (string serverString in this.mSettingsManager.ServerStrings)
            {
                this.comboBoxServer.Items.Add(serverString);
            }

            // Now choose the right ones for the selected Instance
            if (this.mSettingsManager.SelectedInstance != null)
            {
                this.groupBoxInstance.Text = this.mSettingsManager.SelectedInstance.ClientRef.ConfigurationName;
                this.comboBoxUserFolder.SelectedItem = this.mSettingsManager.SelectedInstance.ClientRef.UserDataDir;
                this.comboBoxDatamoverIncomingFolder.SelectedItem = this.mSettingsManager.SelectedInstance.DatamoverRef.IncomingTarget;
                this.comboBoxServer.SelectedItem = this.mSettingsManager.SelectedInstance.ServerRef.Label;
            }
        }

        private void buttonServerEdit_Click(object sender, EventArgs e)
        {
            using (var form = new ServerConfigurationDialog(this.mSettingsManager.SelectedInstance.ServerRef))
            {
                var result = form.ShowDialog();
                if (result == DialogResult.OK)
                {
                    // Get the updated Server
                    Server updatedServer = form.Result;

                    // Set it
                    this.mSettingsManager.SelectedInstance.ServerRef = updatedServer;

                    // Refresh the InstanceConfigurator
                    this.Refresh();
                }
            }
        }

        private void buttonUserFolderEdit_Click(object sender, EventArgs e)
        {
            using (var form = new AnnotatiolToolConfigurationDialog(this.mSettingsManager.SelectedInstance.ClientRef))
            {
                var result = form.ShowDialog();
                if (result == DialogResult.OK)
                {
                    // Get the updated Client
                    Client updatedClient = form.Result;

                    // Set it
                    this.mSettingsManager.SelectedInstance.ClientRef = updatedClient;

                    // Update the corresponding field
                    this.comboBoxUserFolder.Text = this.mSettingsManager.SelectedInstance.ClientRef.UserDataDir;

                    // Refresh the InstanceConfigurator
                    this.Refresh();
                }
            }
        }

        private void buttonDatamoverIncomingFolderEdit_Click(object sender, EventArgs e)
        {
            using (var form = new DatamoverConfigurationDialog(this.mSettingsManager.SelectedInstance.DatamoverRef))
            {
                var result = form.ShowDialog();
                if (result == DialogResult.OK)
                {
                    // Get the updated Client
                    Datamover updatedDatamover = form.Result;

                    // Set it
                    this.mSettingsManager.SelectedInstance.DatamoverRef = updatedDatamover;

                    // Update the corresponding field
                    this.comboBoxDatamoverIncomingFolder.Text = this.mSettingsManager.SelectedInstance.DatamoverRef.IncomingTarget;

                    // Refresh the InstanceConfigurator
                    this.Refresh();
                }
            }
        }

        private void comboBoxUserFolder_SelectedIndexChanged(object sender, EventArgs e)
        {
            string userDataDir = this.comboBoxUserFolder.SelectedItem.ToString();
            Client client = this.mSettingsManager.GetClientByUserDataDir(userDataDir);
            if (client == null)
            {
                throw new Exception(
                    "Client with user data dir " +
                    userDataDir + 
                    " not found in list of Instances!");
            }

            // Nothing to do if no change
            if (Object.ReferenceEquals(this.mSettingsManager.SelectedInstance.ClientRef, client))
            {
                return;
            }

            // Update the reference
            this.mSettingsManager.SelectedInstance.ClientRef = client;

            // Refresh the UI
            this.Refresh();
        }

        private void comboBoxDatamoverIncomingFolder_SelectedIndexChanged(object sender, EventArgs e)
        {
            string incomingDir = this.comboBoxDatamoverIncomingFolder.SelectedItem.ToString();
            Datamover datamover = this.mSettingsManager.GetDatamoverByIncomingDir(incomingDir);
            if (datamover == null)
            {
                throw new Exception(
                    "Datamover with incoming dir " +
                    datamover + 
                    " not found in list of Instances!");
            }

            // Nothing to do if no change
            if (Object.ReferenceEquals(this.mSettingsManager.SelectedInstance.DatamoverRef, datamover))
            {
                return;
            }

            // Update the reference
            this.mSettingsManager.SelectedInstance.DatamoverRef = datamover;

            // Refresh the UI
            this.Refresh();
        }

        private void comboBoxServer_SelectedIndexChanged(object sender, EventArgs e)
        {
            string label = this.comboBoxServer.SelectedItem.ToString();
            Server server = this.mSettingsManager.GetServerByLabel(label);
            if (server == null)
            {
                throw new Exception(
                    "Server with label " +
                    server + 
                    " not found in list of Instances!");
            }

            // Nothing to do if no change
            if (Object.ReferenceEquals(this.mSettingsManager.SelectedInstance.ServerRef, server))
            {
                return;
            }

            // Update the reference
            this.mSettingsManager.SelectedInstance.ServerRef = server;

            // Refresh the UI
            this.Refresh();
        }
    }
}
