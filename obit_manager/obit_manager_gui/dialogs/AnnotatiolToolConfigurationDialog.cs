using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using obit_manager_settings.components;

namespace obit_manager_gui.dialogs
{
    public partial class AnnotatiolToolConfigurationDialog : Form
    {
        private Client mEditableClient;
        private Client mReturnClient = null;

        public AnnotatiolToolConfigurationDialog(Client client)
        {
            // Create a copy of the original client to be edited in this form
            this.mEditableClient = new Client(client);

            InitializeComponent();

            this.UpdateUI();

            // Assign the buttons
            this.AcceptButton = this.buttonAccept;
            this.CancelButton = this.buttonCancel;
        }

        /// <summary>
        /// Result
        /// </summary>
        public Client Result => this.mReturnClient;

        private void UpdateUI()
        {
            this.buttonUserDataDir.Text = this.mEditableClient.UserDataDir;
            this.textBoxHumanFriedlyMachineName.Text = this.mEditableClient.HumanFriendlyHostName;
            this.checkBoxCreateMarkerFile.Checked = this.mEditableClient.CreateMarkerFile;
        }

        private void buttonUserDataDir_Click(object sender, EventArgs e)
        {
            using (var dialog = new FolderBrowserDialog())
            {
                DialogResult result = dialog.ShowDialog();

                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(dialog.SelectedPath))
                {
                    this.mEditableClient.UserDataDir = dialog.SelectedPath;
                    this.buttonUserDataDir.Text = this.mEditableClient.UserDataDir;
                }
            }
        }

        private void textBoxHumanFriedlyMachineName_TextChanged(object sender, EventArgs e)
        {
            this.mEditableClient.HumanFriendlyHostName = this.textBoxHumanFriedlyMachineName.Text;
        }

        private void checkBoxCreateMarkerFile_CheckedChanged(object sender, EventArgs e)
        {
            this.mEditableClient.CreateMarkerFile = this.checkBoxCreateMarkerFile.Checked;
        }

        private void buttonAccept_Click(object sender, EventArgs e)
        {
            // Accept the changes
            this.mReturnClient = this.mEditableClient;
            this.DialogResult = DialogResult.OK;

            // Close the dialog
            this.Close();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            // Reject the changes
            this.mReturnClient = null;
            this.DialogResult = DialogResult.Cancel;

            // Close the dialog
            this.Close();
        }

        private void buttonNetworkHostName_Click(object sender, EventArgs e)
        {
            this.textBoxHumanFriedlyMachineName.Text = Environment.MachineName;
        }
    }
}
