using System;
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
                this.groupBoxInstance.Text = this.mSettingsManager.GetClientFromSelectedInstance().ConfigurationName;
                this.comboBoxUserFolder.SelectedIndex = this.mSettingsManager.SelectedInstance.ClientIndex;
                this.comboBoxDatamoverIncomingFolder.SelectedIndex = this.mSettingsManager.SelectedInstance.DatamoverIndex;
                this.comboBoxServer.SelectedIndex = this.mSettingsManager.SelectedInstance.ServerIndex;
            }
        }

        private void buttonServerEdit_Click(object sender, EventArgs e)
        {
            Server server = this.mSettingsManager.GetServerFromInstance(this.mSettingsManager.SelectedInstance);

            using (var form = new ServerConfigurationDialog(server))
            {
                var result = form.ShowDialog();
                if (result == DialogResult.OK)
                {
                    // Get the updated Server
                    Server updatedServer = form.Result;

                    // Set it
                    this.mSettingsManager.ReplaceServerObjectForInstance(
                        this.mSettingsManager.SelectedInstance,
                        updatedServer
                    );

                    // Refresh the InstanceConfigurator
                    this.Refresh();
                }
            }
        }

        private void buttonUserFolderEdit_Click(object sender, EventArgs e)
        {
            Client client = this.mSettingsManager.GetClientFromInstance(this.mSettingsManager.SelectedInstance);

            using (var form = new AnnotatiolToolConfigurationDialog(client))
            {
                var result = form.ShowDialog();
                if (result == DialogResult.OK)
                {
                    // Get the updated Client
                    Client updatedClient = form.Result;

                    // Set it
                    this.mSettingsManager.ReplaceClientObjectForInstance(
                        this.mSettingsManager.SelectedInstance,
                        updatedClient);

                    // Update the corresponding field
                    this.comboBoxUserFolder.Text = updatedClient.UserDataDir;

                    // Refresh the InstanceConfigurator
                    this.Refresh();
                }
            }
        }

        private void buttonDatamoverIncomingFolderEdit_Click(object sender, EventArgs e)
        {
            Datamover datamover = this.mSettingsManager.GetDatamoverFromInstance(this.mSettingsManager.SelectedInstance);

            using (var form = new DatamoverConfigurationDialog(datamover))
            {
                var result = form.ShowDialog();
                if (result == DialogResult.OK)
                {
                    // Get the updated Client
                    Datamover updatedDatamover = form.Result;

                    // Set it
                    this.mSettingsManager.ReplaceDatamoverObjectForInstance(
                        this.mSettingsManager.SelectedInstance,
                        updatedDatamover
                    );

                    // Update the corresponding field
                    this.comboBoxDatamoverIncomingFolder.Text = updatedDatamover.IncomingTarget;

                    // Refresh the InstanceConfigurator
                    this.Refresh();
                }
            }
        }

        private void comboBoxUserFolder_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Check that new and old indices are indeed different -- otherwise we will create a stack overflow
            if (this.mSettingsManager.SelectedInstance.ClientIndex == this.comboBoxUserFolder.SelectedIndex)
            {
                return;
            }

            // Update the reference
            this.mSettingsManager.ChangeClientIndexForCurrentInstance(this.comboBoxUserFolder.SelectedIndex);

            // Refresh the UI
            this.Refresh();
        }

        private void comboBoxDatamoverIncomingFolder_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Check that new and old indices are indeed different -- otherwise we will create a stack overflow
            if (this.mSettingsManager.SelectedInstance.DatamoverIndex == this.comboBoxDatamoverIncomingFolder.SelectedIndex)
            {
                return;
            }

            // Update the reference
            this.mSettingsManager.ChangeDatamoverIndexForCurrentInstance(this.comboBoxDatamoverIncomingFolder.SelectedIndex);

            // Refresh the UI
            this.Refresh();
        }

        private void comboBoxServer_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Check that new and old indices are indeed different -- otherwise we will create a stack overflow
            if (this.mSettingsManager.SelectedInstance.ServerIndex == this.comboBoxServer.SelectedIndex)
            {
                return;
            }

            // Update the reference
            this.mSettingsManager.ChangeServerIndexForCurrentInstance(this.comboBoxServer.SelectedIndex);

            // Refresh the UI
            this.Refresh();
        }
    }
}
