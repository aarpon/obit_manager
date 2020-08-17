using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics.Eventing.Reader;
using System.Windows.Forms;
using obit_manager_settings;
using obit_manager_settings.components;

namespace obit_manager_gui.dialogs
{
    public partial class ServerConfigurationDialog : Form
    {
        private Server mEditableServer;
        private Server mReturnServer = null;

        public ServerConfigurationDialog(Server server)
        {
            // Create a copy of the original server to be edited in this form
            this.mEditableServer = new Server(server);

            InitializeComponent();

            this.UpdateUI();

            // Assign the buttons
            this.AcceptButton = this.buttonAccept;
            this.CancelButton = this.buttonCancel;
        }

        public void UpdateUI()
        {
            // openBIS (Application) Server
            this.comboBoxOpenBISServerProtocol.SelectedItem = this.mEditableServer.ApplicationServerProtocol;
            this.textBoxOpenBISServerHostName.Text = this.mEditableServer.ApplicationServerHostname;
            if (this.mEditableServer.ApplicationServerPort != -1)
            {
                this.textBoxOpenBISServerPort.Text = this.mEditableServer.ApplicationServerPort.ToString();
            }
            else
            {
                this.textBoxOpenBISServerPort.Text = "";
            }
            this.checkBoxOpenBISSErverAcceptSelfSignedCert.Checked = this.mEditableServer.ApplicationServerAcceptSelfSignedCert.Equals("yes") ? true: false;;

            // DataStore Server
            this.textBoxDSSHostName.Text = this.mEditableServer.DataStoreServerHostname;
            this.textBoxDSSUnixUser.Text = this.mEditableServer.DataStoreServerUserName;
            this.textBoxDSSRootDropboxFolder.Text = this.mEditableServer.DataStoreServerPathToRootDropboxFolder;
            this.textBoxDSSPathToLastChangedExecutable.Text = this.mEditableServer.DataStoreServerPathToLastChangedExecutable;

            // Hardware
            HardwareMapEntry hardwareMapEntry =
                Hardware.Inventory[this.mEditableServer.DataStoreServerHardwareSubClass];
            if (hardwareMapEntry.Key.Equals("microscopy"))
            {
                this.comboBoxHardwareSubCategory.Enabled = false;
            }
            else
            {
                this.comboBoxHardwareSubCategory.Enabled = true;
            }
            this.comboBoxHardwareCategory.SelectedItem = hardwareMapEntry.Category;
            this.comboBoxHardwareSubCategory.SelectedItem = hardwareMapEntry.Name;
            this.labelHardwareDescription.Text = hardwareMapEntry.Description;
        }

        private void buttonGenerateCryptoKey_Click(object sender, EventArgs e)
        {

        }

        private void buttonUseCryptoKey_Click(object sender, EventArgs e)
        {

        }

        private void comboBoxOpenBISServerProtocol_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.mEditableServer.ApplicationServerProtocol = this.comboBoxOpenBISServerProtocol.SelectedItem.ToString();
        }

        private void textBoxOpenBISServerHostName_TextChanged(object sender, EventArgs e)
        {
            this.mEditableServer.ApplicationServerHostname = this.textBoxOpenBISServerHostName.Text;
        }

        private void textBoxOpenBISServerPort_TextChanged(object sender, EventArgs e)
        {
            if (this.textBoxOpenBISServerPort.Text == "")
            {
                this.mEditableServer.ApplicationServerPort = -1;
                return;
            }

            if (Int32.TryParse(this.textBoxOpenBISServerPort.Text, out int value))
            {
                this.mEditableServer.ApplicationServerPort = value;
            }
            else
            {
                if (this.mEditableServer.ApplicationServerPort != -1)
                {
                    this.textBoxOpenBISServerPort.Text = this.mEditableServer.ApplicationServerPort.ToString();
                }
                else
                {
                    this.textBoxOpenBISServerPort.Text = "";
                }
            }
        }

        private void buttonAccept_Click(object sender, EventArgs e)
        {
            // Accept the changes
            this.mReturnServer = this.mEditableServer;
            this.DialogResult = DialogResult.OK;

            // Close the dialog
            this.Close();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            // Reject the changes
            this.mReturnServer = null;
            this.DialogResult = DialogResult.Cancel;

            // Close the dialog
            this.Close();
        }

        #region properties

        /// <summary>
        /// Result
        /// </summary>
        public Server Result => this.mReturnServer;

        #endregion

        private void textBoxDSSUnixUser_TextChanged(object sender, EventArgs e)
        {
            this.mEditableServer.DataStoreServerUserName = this.textBoxDSSUnixUser.Text;
        }

        private void textBoxDSSHostName_TextChanged(object sender, EventArgs e)
        {
            this.mEditableServer.DataStoreServerHostname = this.textBoxDSSHostName.Text;
        }

        private void textBoxDSSRootDropboxFolder_TextChanged(object sender, EventArgs e)
        {
            this.mEditableServer.DataStoreServerPathToRootDropboxFolder = this.textBoxDSSRootDropboxFolder.Text;
        }

        private void textBoxDSSPathToLastChangedExecutable_TextChanged(object sender, EventArgs e)
        {
            this.mEditableServer.DataStoreServerPathToLastChangedExecutable =
                this.textBoxDSSPathToLastChangedExecutable.Text;
        }

        private void checkBoxOpenBISSErverAcceptSelfSignedCert_CheckedChanged(object sender, EventArgs e)
        {
            this.mEditableServer.ApplicationServerAcceptSelfSignedCert =
                this.checkBoxOpenBISSErverAcceptSelfSignedCert.Checked ? "yes" : "no";
        }

        private void comboBoxHardwareCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            HardwareMapEntry map;

            if (this.comboBoxHardwareCategory.SelectedItem.Equals("Microscopy"))
            {
                map = Hardware.Inventory["microscopy"];

                this.comboBoxHardwareSubCategory.SelectedItem = null;
                this.comboBoxHardwareSubCategory.Enabled = false;
            }
            else
            {
                // Is there a subcategory selected?
                if (this.comboBoxHardwareSubCategory.SelectedItem == null)
                {
                    // Default to BD LSR Fortessa
                    map = Hardware.Inventory["lsrfortessa"];
                }
                else
                {
                    map = Hardware.Inventory[this.comboBoxHardwareSubCategory.SelectedItem.ToString()];
                }
                
                this.comboBoxHardwareSubCategory.SelectedItem = map.Name;
                this.comboBoxHardwareSubCategory.Enabled = true;

            }

            this.labelHardwareDescription.Text = map.Description;

            this.mEditableServer.DataStoreServerHardwareClass = map.Category;
            this.mEditableServer.DataStoreServerHardwareSubClass = map.Key;
        }

        private void comboBoxHardwareSubCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.comboBoxHardwareSubCategory.SelectedItem == null)
            {
                return;
            }

            foreach (var entry in Hardware.Inventory)
            {
                if (entry.Value.Name.Equals(this.comboBoxHardwareSubCategory.SelectedItem.ToString()))
                {
                    this.labelHardwareDescription.Text = entry.Value.Description;

                    this.mEditableServer.DataStoreServerHardwareClass = this.GetHardwareClassFromSubclass(entry.Key);
                    this.mEditableServer.DataStoreServerHardwareSubClass = entry.Key;
                }
            }
        }

        /// <summary>
        /// Return the hardware class from the specialized subclass.
        /// </summary>
        /// <param name="subclass"></param>
        /// <returns></returns>
        public string GetHardwareClassFromSubclass(string subclass)
        {
            if (subclass.ToLower().Equals("microscopy"))
            {
                return "Microscopy";
            }
            else
            {
                return "Flow cytometry";
            }
        }

    }
}
