using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using obit_manager_api.core;
using obit_manager_gui.components;
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

            // Initialize the Settings Manager
            this.mSettingsManager = new SettingsManager();

            // Initialize components
            InitializeComponent();

            // Register the event handlers
            this.RegisterEventHandlers();

            // Set defaults
            updateUI();

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
                    this.mSettingsManager.InstallationDir = dialog.SelectedPath;
                    buttonOBITInstallationDirectory.Text = this.mSettingsManager.InstallationDir;

                    // @ToDo: Update the application settings
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


        //private async Task<bool> downloadAndExtractJavaAsync(bool is64bit = true)
        //{
            //// Check that we have an installation folder 
            //if (this.mAppSettings.InstallationDir.Equals(""))
            //{
            //    return false;
            //}

            //// Prepare relevant variables
            //String downloadURL = "";
            //String targetFileName = "";
            //String jdkExtractPath = "";
            //String jdkFinalPath = "";

            //// Assign the correct values depending on the choice of the platform
            //if (is64bit)
            //{
            //    downloadURL = Constants.Jdk64bitURL;
            //    targetFileName = Path.Combine(InstallationFolder, Constants.Jdk64bitArchiveFileName);
            //    jdkExtractPath = Path.Combine(InstallationFolder, Constants.Jdk64bitExtractDirName);
            //    jdkFinalPath = Path.Combine(InstallationFolder, Constants.Jdk64bitFinalPath);
            //}
            //else
            //{
            //    downloadURL = Constants.Jdk32bitURL;
            //    targetFileName = Path.Combine(InstallationFolder, Constants.Jdk32bitArchiveFileName);
            //    jdkExtractPath = Path.Combine(InstallationFolder, Constants.Jdk32bitExtractDirName);
            //    jdkFinalPath = Path.Combine(InstallationFolder, Constants.Jdk32bitFinalPath);
            //}

            //// Does the destination folder already exist?
            //if (Directory.Exists(jdkFinalPath))
            //{
            //    return false;
            //}

            //// Download the file
            //await WebUtils.DownloadAsync(downloadURL, targetFileName);
            //if (!File.Exists(targetFileName))
            //{
            //    return false;
            //}

            //// Decompress the file
            //FileSystem.ExtractZIPFileToFolder(targetFileName, InstallationFolder);

            //// Check that the extract folder exists
            //if (!Directory.Exists(jdkExtractPath))
            //{
            //    return false;
            //}

            //// Move the JRE subfolder in the final location
            //Directory.Move(Path.Combine(jdkExtractPath, "jre"), jdkFinalPath);

            //// Delete temporary files and folders
            //File.Delete(targetFileName);
            //Directory.Delete(jdkExtractPath, recursive: true);

            //// Finally, return true if the jre folder is in the final location
            //return Directory.Exists(jdkFinalPath);
        //}

        private void updateUI()
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

            this.comboBoxInstances.SelectedItem = this.mSettingsManager.SelectedInstance.ClientRef.ConfigurationName;

            // Update The InstanceConfigurator with current Instance's settings
            this.mInstanceConfigurator.SetInstance(this.mSettingsManager.SelectedInstance);

            //// Set the platform bits
            //if (Environment.Is64BitOperatingSystem)
            //{
            //    radioButtonPlatform32bit.Checked = false;
            //    radioButtonPlatform64bit.Checked = true;
            //}
            //else
            //{
            //    radioButtonPlatform32bit.Checked = true;
            //    radioButtonPlatform64bit.Checked = false;
            //}
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
            // Get the instance name
            string name = ((ComboBox)sender).Text;

            // Get the instance
            Instance instance = this.mSettingsManager.GetInstanceByName(name);

            // Update the SettingsManager
            this.mSettingsManager.SelectedInstance = instance;

            // Update the configurator
            this.mInstanceConfigurator.SetInstance(instance);

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
                this.mSettingsManager.SelectedInstance.ClientRef.ConfigurationName))
            {
                var result = form.ShowDialog();
                if (result == DialogResult.OK)
                {
                    // Update the name
                    string newInstanceName = form.Result;

                    // Set it
                    this.mSettingsManager.SelectedInstance.ClientRef.ConfigurationName = newInstanceName;

                    // Update the dropdown menu
                    var lomm = this.comboBoxInstances.SelectedItem;
                    this.comboBoxInstances.Items[this.comboBoxInstances.SelectedIndex] = newInstanceName;

                    // Update the configurator
                    this.mInstanceConfigurator.Refresh();
                }
            }
        }
    }
}
