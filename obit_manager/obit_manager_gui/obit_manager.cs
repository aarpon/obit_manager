using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using obit_manager_api.core;
using obit_manager_gui.dialogs;
using obit_manager_settings;
using obit_manager_settings.components;

namespace obit_manager_gui
{
    public partial class obit_manager : Form
    {
        // Logger
        private static readonly NLog.Logger sLogger = NLog.LogManager.GetCurrentClassLogger();

        // Private application settings
        private SettingsManager mSettingsManager;

        // Threads and locks
        private Thread freshInstallThread = null;

        public obit_manager()
        {
            sLogger.Info("oBIT Manager started.");

            // Store the reference to the Settings Manager
            this.mSettingsManager = SettingsManager.Get();

            // Initialize components
            InitializeComponent();

            // Fill UI with current values
            this.FillUI();

            // Log some information
            this.textBoxLogWindow.AppendText("Loaded " + this.mSettingsManager.NumInstances + " instance(s).\r\n");
            this.textBoxLogWindow.AppendText("Completed initialization.\r\n");
        }

        private void obit_manager_Resize(object sender, EventArgs e)
        {
            if (FormWindowState.Minimized == this.WindowState)
            {
                obitManagerNotifyIcon.Visible = true;
                obitManagerNotifyIcon.ShowBalloonTip(500);
                this.Hide();
            }

            else if (FormWindowState.Normal == this.WindowState)
            {
                obitManagerNotifyIcon.Visible = false;
            }
        }

        private void buttonFreshInstall_Click(object sender, EventArgs e)
        {
            if (this.mSettingsManager.InstallationDir.Equals(""))
            {
                MessageBox.Show(
                    "Please pick an installation directory!",
                    "Error: cannot continue!",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return;
            }

            this.freshInstallThread = new Thread(new ThreadStart(this.ThreadFreshInstall));

            this.freshInstallThread.Start();
        }

        private void buttonOBITInstallationDirectory_Click(object sender, EventArgs e)
        {
            using (var dialog = new FolderBrowserDialog())
            {
                DialogResult result = dialog.ShowDialog();

                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(dialog.SelectedPath))
                {
                    DialogResult dialogResult = MessageBox.Show(
                        "This will reset all current changes. Do you want to continue?",
                        "Are you sure?",
                        MessageBoxButtons.YesNo);
                    if(dialogResult == DialogResult.Yes)
                    {
                        this.mSettingsManager.ResetAll();
                        this.mSettingsManager.InstallationDir = dialog.SelectedPath;
                        buttonOBITInstallationDirectory.Text = this.mSettingsManager.InstallationDir;
                    }
                }
            }
        }

        /// <summary>
        /// Runs a fresh installation in a thread separate from the main UI thread.
        /// 
        /// The Thread itself will start several asynchronous calls.
        /// 
        /// </summary>
        private void ThreadFreshInstall()
        {
            //// Launch several asynchronous tasks
            //Task[] installationTasks = new Task[1];
            //installationTasks[0] = Task.Factory.StartNew(async () =>
            //{
            //    bool is64bit = radioButtonPlatform64bit.Checked;
            //    await downloadAndExtractJavaAsync(is64bit);
            //});
            //Task.WaitAll(installationTasks);
        }

        private void FillUI()
        {
            // Get the installation directory from the application settings
            string installationDir = this.mSettingsManager.InstallationDir;
            if (installationDir.Equals(""))
            {
                buttonOBITInstallationDirectory.Text = "Pick oBIT installation dir...";
                this.mSettingsManager.InstallationDir = "";
            }
            else
            {
                buttonOBITInstallationDirectory.Text = installationDir;
                this.mSettingsManager.InstallationDir = installationDir;
            }

            // Fill the Instance dropdown menu
            this.comboBoxInstances.Items.Clear();
            foreach (string name in this.mSettingsManager.GetInstanceNames())
            {
                this.comboBoxInstances.Items.Add(name);
            }

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

            // Point the comboBox to the selected instance
            this.comboBoxInstances.SelectedIndex = this.mSettingsManager.SelectedInstanceIndex;

            // Update all Instance configuration items
            this.UpdateUIForChangedInstance();
        }

        private void UpdateUIForChangedInstance()
        {
            // Now choose the right ones for the selected Instance
            if (this.mSettingsManager.SelectedInstance != null &&
                this.mSettingsManager.SelectedInstanceIndex != -1)
            {

                this.groupBoxInstance.Text = this.mSettingsManager.SelectedInstance.Name;
                this.comboBoxUserFolder.SelectedIndex = this.mSettingsManager.SelectedInstance.ClientIndex;
                this.comboBoxDatamoverIncomingFolder.SelectedIndex = this.mSettingsManager.SelectedInstance.DatamoverIndex;
                this.comboBoxServer.SelectedIndex = this.mSettingsManager.SelectedInstance.ServerIndex;
            }

        }

        private void download32bitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DownloadOnly(is64bit: false);
        }

        private void download64bitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DownloadOnly(is64bit: true);
        }

        /// <summary>
        /// Download all packages to the requested folder, either 64 or 32 bit.
        /// </summary>
        /// <param name="downloadFolder">Folder in which to saved the download archives.</param>
        /// <param name="is64bit">True if the 64-bit vMersion of packages should be downloaded, false otherwise.</param>
        /// 
        private async void DownloadOnly(bool is64bit = true)
        {
            string downloadFolder;

            using (var dialog = new FolderBrowserDialog())
            {
                DialogResult result = dialog.ShowDialog();

                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(dialog.SelectedPath))
                {
                    downloadFolder = dialog.SelectedPath;
                }
                else
                {
                    // Stop here
                    return;
                }
            }

            // Make sure the folder exists
            Directory.CreateDirectory(downloadFolder);

            string jdkURL;
            string annotationToolURL;
            string datamoverURL = Constants.DatamoverURL;
            string datamoverJslURL = Constants.DatamoverJslURL;
            string jdkMD5URL;
            string jdkArchiveFileName;
            string jdkMD5FileName;
            string annotationToolArchiveFileName;
            string datamoverArchiveFileName = Path.Combine(downloadFolder, Constants.DatamoverArchiveFileName);
            string datamoverJslArchiveFileName = Path.Combine(downloadFolder, Constants.DatamoverJslArchiveFileName);

            if (is64bit)
            {
                jdkURL = Constants.Jdk64bitURL;
                annotationToolURL = Constants.AnnotationTool64bitURL;
                jdkMD5URL = Constants.Jdk64bitMD5URL;
                jdkArchiveFileName = Path.Combine(downloadFolder, Constants.Jdk64bitArchiveFileName);
                annotationToolArchiveFileName = Path.Combine(downloadFolder, Constants.AnnotationTool64bitArchiveFileName);
                jdkMD5FileName = Path.Combine(downloadFolder, Constants.Jdk64bitMD5FileName);
            }
            else
            {
                jdkURL = Constants.Jdk32bitURL;
                annotationToolURL = Constants.AnnotationTool32bitURL;
                jdkMD5URL = Constants.Jdk32bitMD5URL;
                jdkArchiveFileName = Path.Combine(downloadFolder, Constants.Jdk32bitArchiveFileName);
                annotationToolArchiveFileName = Path.Combine(downloadFolder, Constants.AnnotationTool32bitArchiveFileName);
                jdkMD5FileName = Path.Combine(downloadFolder, Constants.Jdk32bitMD5FileName);
            }

            // Download all archives in parallel to the selected folder
            var taskJDK = WebUtils.DownloadAsync(jdkURL, jdkArchiveFileName);
            var taskAT = WebUtils.DownloadAsync(annotationToolURL, annotationToolArchiveFileName);
            var taskDM = WebUtils.DownloadAsync(datamoverURL, datamoverArchiveFileName);
            var taskDMJSL = WebUtils.DownloadAsync(datamoverJslURL, datamoverJslArchiveFileName);

            await Task.WhenAll(taskJDK, taskAT, taskDM, taskDMJSL);

            // Check the checksum of the JDK
            await WebUtils.DownloadAsync(jdkMD5URL, jdkMD5FileName);
            string JdkMD5Checksum = FileSystem.CalculateMD5Checksum(jdkArchiveFileName);

            // Read  the checksum
            string JdkMD5ChecksumFromFile = FileSystem.ReadMD5ChecksumFromFile(jdkMD5FileName);
            File.Delete(jdkMD5FileName);

            // Calculate and compare MD5 checksum
            if (!JdkMD5Checksum.Equals(JdkMD5ChecksumFromFile))
            {
                MessageBox.Show("The MD5 checksum of the JDK does not match the expected string!",
                    "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            MessageBox.Show("All packages were downloaded to " + downloadFolder + ".",
                "All done!", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void loadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.mSettingsManager.Reload();

        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.mSettingsManager.Save();
        }

        private void viewLogToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(@"C:/ProgramData/obit/AnnotationTool/obit_manager.log");
        }

        private void comboBoxInstances_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Update the SettingsManager
            this.mSettingsManager.SelectedInstanceIndex = comboBoxInstances.SelectedIndex;

            // Update all Instance configuration items
            this.UpdateUIForChangedInstance();

            // Update the default Instance label
            if (this.comboBoxInstances.SelectedIndex == 0)
            {
                this.labelSelectedInstanceIsDefault.Text = "This is the default Instance.";
            }
            else
            {
                this.labelSelectedInstanceIsDefault.Text = "The default Instance is '" +
                                                           this.comboBoxInstances.Items[0] +
                                                           "'.";
            }
        }

        private void buttonEditInstanceName_Click(object sender, EventArgs e)
        {
            using (var form = new SingleStringEditor(
                "Edit Instance name", 
                "Instance name",
                this.mSettingsManager.SelectedInstance.Name))
            {
                var result = form.ShowDialog();
                if (result == DialogResult.OK)
                {
                    // Update the name
                    string newInstanceName = form.Result;

                    // Set it
                    this.mSettingsManager.SelectedInstance.Name = newInstanceName;

                    // Update the dropdown menu
                    this.comboBoxInstances.Items[this.comboBoxInstances.SelectedIndex] = newInstanceName;

                    // Update all Instance configuration items  
                    this.UpdateUIForChangedInstance();
                }
            }
        }

        private void buttonInstanceUp_Click(object sender, EventArgs e)
        {
            if (this.comboBoxInstances.SelectedIndex == 0)
            {
                return;
            }

            // Swap the selected Instance with the one immediately preceding it
            int index2 = this.comboBoxInstances.SelectedIndex;
            int index1 = this.comboBoxInstances.SelectedIndex - 1;
            this.mSettingsManager.SwapInstances(index1, index2);

            // Update the selected instance
            this.mSettingsManager.SelectedInstanceIndex = index1;

            // Update all Instance configuration items
            this.UpdateUIForChangedInstance();
        }

        private void buttonInstanceDown_Click(object sender, EventArgs e)
        {
            if (this.comboBoxInstances.SelectedIndex == this.mSettingsManager.NumInstances - 1)
            {
                return;
            }

            // Swap the selected Instance with the one immediately preceding it
            int index2 = this.comboBoxInstances.SelectedIndex + 1;
            int index1 = this.comboBoxInstances.SelectedIndex;
            this.mSettingsManager.SwapInstances(index1, index2);

            // Update the selected instance
            this.mSettingsManager.SelectedInstanceIndex = index2;

            // Update all Instance configuration items
            this.UpdateUIForChangedInstance();
        }

        private void buttonApplyAllChanges_Click(object sender, EventArgs e)
        {
            this.mSettingsManager.Save();

            // @TODO All the rest!
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

            // Consolidate the Instances
            this.ConsolidateInstances();

            // Validate the Instances
            this.ValidateInstances();
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

            // Consolidate the Instances
            this.ConsolidateInstances();

            // Validate the Instances
            this.ValidateInstances();
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

            // Consolidate the Instances
            this.ConsolidateInstances();

            // Validate the Instances
            this.ValidateInstances();
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

        public bool ConsolidateInstances()
        {
            // Get the reference to the SettingsManager
            SettingsManager settingsManager = SettingsManager.Get();

            // Get references to the relevant objects
            Instance instance = settingsManager.SelectedInstance;
            Client client = settingsManager.GetClientFromSelectedInstance();
            Datamover datamover = settingsManager.GetDatamoverFromSelectedInstance();
            Server server = settingsManager.GetServerFromSelectedInstance();

            // Mirror the Instance name to the Client ConfigurationName property
            client.ConfigurationName = instance.Name;

            // Store the IncomingTarget property from the Datamover object in the
            // DatamoveIncomingDir property of the client.
            client.DatamoverIncomingDir = datamover.IncomingTarget;

            // @TODO Anything else?

            return true;
        }

        public bool ValidateInstances()
        {
            return true;
        }

    }
}
