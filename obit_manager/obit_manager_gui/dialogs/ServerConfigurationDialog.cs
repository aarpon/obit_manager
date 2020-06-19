using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Windows.Forms;
using obit_manager_settings.components;

namespace obit_manager_gui.dialogs
{
    public partial class ServerConfigurationDialog : Form
    {
        private Server mEditableServer;
        private Server mReturnServer = null;

        private static Dictionary<string, HardwareMapEntry> HardwareMap = new Dictionary<string, HardwareMapEntry>()
        {
            {
                "microscopy", 
                new HardwareMapEntry()
                {
                    Key = "microscopy",
                    Category = "Microscopy",
                    Name = "All microscopes",
                    Description = ""
                }
            },
            {
                "lsrfortessa",
                new HardwareMapEntry()
                {
                    Key = "lsrfortessa",
                    Category = "Flow cytometry",
                    Name = "BD LSR Fortessa",
                    Description = ""
                }
            },
            {
                "facsaria", 
                new HardwareMapEntry()
                {
                    Key = "facsaria",
                    Category = "Flow cytometry",
                    Name = "BD FACS Aria",
                    Description = ""
                }
            },
            {
                "influx",
                new HardwareMapEntry()
                {
                    Key = "influx",
                    Category = "Flow cytometry",
                    Name = "BD Influx",
                    Description = ""
                }
            },
            {
                "s3e",
                new HardwareMapEntry()
                {
                    Key = "s3e",
                    Category = "Flow cytometry",
                    Name = "Bio-Rad S3e",
                    Description = ""
                }
            },
            {
                "mofloxdp",
                new HardwareMapEntry()
                {
                    Key = "mofloxdp",
                    Category = "Flow cytometry",
                    Name = "BC MoFlo XDP",
                    Description = ""
                }
            },
            {
                "sonysh800s",
                new HardwareMapEntry()
                {
                    Key = "sonysh800s",
                    Category = "Flow cytometry",
                    Name = "Sony SH800S",
                    Description = ""
                }
            },
            {
                "sonyma900",
                new HardwareMapEntry()
                {
                    Key = "sonyma900",
                    Category = "Flow cytometry",
                    Name = "Sony MA900",
                    Description = ""
                }
            }
        };

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
            this.checkBoxOpenBISSErverAcceptSelfSignedCert.Checked = this.mEditableServer.ApplicationServerAcceptSelfSignedCert;

            // DataStore Server
            this.textBoxDSSHostName.Text = this.mEditableServer.DataStoreServerHostname;
            this.textBoxDSSUnixUser.Text = this.mEditableServer.DataStoreServerUserName;
            this.textBoxDSSRootDropboxFolder.Text = this.mEditableServer.DataStoreServerPathToRootDropboxFolder;
            this.textBoxDSSPathToLastChangedExecutable.Text = this.mEditableServer.DataStoreServerPathToLastChangedExecutable;

            // Hardware
            HardwareMapEntry hardwareMapEntry =
                ServerConfigurationDialog.HardwareMap[this.mEditableServer.DataStoreServerHardwareClass];
            if (hardwareMapEntry.Key.Equals("microscopy"))
            {
                this.comboBoxHardwareCategory.SelectedItem = "Microscopy";
                this.comboBoxHardwareSubCategory.Enabled = false;
            }
            else
            {
                this.comboBoxHardwareCategory.SelectedItem = "Flow cytometry";
                this.comboBoxHardwareSubCategory.SelectedItem = hardwareMapEntry.Name;
                this.comboBoxHardwareSubCategory.Enabled = true;
            }
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

        public Server Result
        {
            get => this.mReturnServer; 
        }

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
                this.checkBoxOpenBISSErverAcceptSelfSignedCert.Checked;
        }

        private void comboBoxHardwareCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.comboBoxHardwareCategory.SelectedItem.Equals("Microscopy"))
            {
                this.comboBoxHardwareSubCategory.SelectedItem = null;
                this.mEditableServer.DataStoreServerHardwareClass = "microscopy";
                this.comboBoxHardwareSubCategory.Enabled = false;
            }
            else
            {
                // Default to BD LSR Fortessa
                this.comboBoxHardwareSubCategory.SelectedItem = "BD LSR Fortessa";
                this.mEditableServer.DataStoreServerHardwareClass = "lsr-fortessa";
                this.comboBoxHardwareSubCategory.Enabled = true;
            }
        }

        private void comboBoxHardwareSubCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.comboBoxHardwareSubCategory.SelectedItem == null)
            {
                return;
            }

            foreach (var entry in ServerConfigurationDialog.HardwareMap)
            {
                if (entry.Value.Name.Equals(this.comboBoxHardwareSubCategory.SelectedItem.ToString()))
                {
                    this.mEditableServer.DataStoreServerHardwareClass = entry.Key;
                }
            }
        }
    }

    public class HardwareMapEntry
    {
        public string Key { get; set; }
        public string Category { get; set; }
        public string Name { get; set; }
        public String Description { get; set; }
    }
}
