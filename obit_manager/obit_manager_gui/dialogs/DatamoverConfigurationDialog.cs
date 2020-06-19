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
    public partial class DatamoverConfigurationDialog : Form
    {
        private Datamover mEditableDatamover;
        private Datamover mReturnDatamover = null;

        public DatamoverConfigurationDialog(Datamover datamover)
        {
            // Create a copy of the original Datamover object to be edited in this form
            this.mEditableDatamover = new Datamover(datamover);

            InitializeComponent();

            this.UpdateUI();

            // Assign the buttons
            this.AcceptButton = this.buttonAccept;
            this.CancelButton = this.buttonCancel;
        }

        public void UpdateUI()
        {
            this.textBoxDatamoverServiceName.Text = this.mEditableDatamover.ServiceName;
            this.textBoxDatamoverServiceUser.Text = this.mEditableDatamover.LocalUserAccount;
            this.buttonDatamoverDataDir.Text = this.mEditableDatamover.IncomingTarget;
        }

        /// <summary>
        /// Result
        /// </summary>
        public Datamover Result => this.mReturnDatamover;

        private void buttonAccept_Click(object sender, EventArgs e)
        {
            // Accept the changes
            this.mReturnDatamover = this.mEditableDatamover;
            this.DialogResult = DialogResult.OK;

            // Close the dialog
            this.Close();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            // Reject the changes
            this.mReturnDatamover = null;
            this.DialogResult = DialogResult.Cancel;

            // Close the dialog
            this.Close();
        }

        private void textBoxDatamoverServiceName_TextChanged(object sender, EventArgs e)
        {
            string serviceName = this.textBoxDatamoverServiceName.Text;
            this.mEditableDatamover.ServiceName = serviceName;
            this.mEditableDatamover.DisplayName = serviceName;
            this.mEditableDatamover.AppName = serviceName;
            this.mEditableDatamover.ServiceDescription = "Custom Datamover installation (" + serviceName + ")";
        }

        private void buttonDatamoverDataDir_Click(object sender, EventArgs e)
        {
            using (var dialog = new FolderBrowserDialog())
            {
                DialogResult result = dialog.ShowDialog();

                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(dialog.SelectedPath))
                {
                    this.mEditableDatamover.IncomingTarget= dialog.SelectedPath;
                    this.buttonDatamoverDataDir.Text = this.mEditableDatamover.IncomingTarget;
                }
            }
        }

        private void buttonDatamoverServiceUser_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Implement me!");
        }

        private void textBoxDatamoverServiceUser_TextChanged(object sender, EventArgs e)
        {
            this.mEditableDatamover.LocalUserAccount = this.textBoxDatamoverServiceUser.Text;
        }
    }
}
