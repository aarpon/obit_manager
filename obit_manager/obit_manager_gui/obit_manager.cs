﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using obit_manager_api.core;
using obit_manager_settings;

namespace obit_manager
{

    public partial class obit_manager : Form
    {
        // Private settings
        private INISettings appSettings;

        private string oBITInstallationFolder = "";

        // Threads and locks
        private Thread freshInstallThread = null;

        public string InstallationFolder { get => oBITInstallationFolder; }

        public obit_manager()
        {
            // Initialize the Settings Manager
            appSettings = new INISettings();

            // Initialize components
            InitializeComponent();

            // Set defaults
            setUIDefaults();
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
            if (InstallationFolder.Equals(""))
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
                    oBITInstallationFolder = dialog.SelectedPath;
                    buttonOBITInstallationDirectory.Text = oBITInstallationFolder;

                    // Update the application settings
                }
                else
                {
                    oBITInstallationFolder = dialog.SelectedPath;
                    buttonOBITInstallationDirectory.Text = "Pick oBIT installation dir...";
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
            // Launch several asynchronous tasks
            Task[] installationTasks = new Task[1];
            installationTasks[0] = Task.Factory.StartNew(async () =>
            {
                bool is64bit = radioButtonPlatform64bit.Checked;
                await downloadAndExtractJavaAsync(is64bit);
            });
            Task.WaitAll(installationTasks);
        }


        private async Task<bool> downloadAndExtractJavaAsync(bool is64bit = true)
        {
            // Check that we have an installation folder 
            if (InstallationFolder.Equals(""))
            {
                return false;
            }

            // Prepare relevant variables
            String downloadURL = "";
            String targetFileName = "";
            String jdkExtractPath = "";
            String jdkFinalPath = "";

            // Assign the correct values depending on the choice of the platform
            if (is64bit)
            {
                downloadURL = Constants.Jdk64bitURL;
                targetFileName = Path.Combine(InstallationFolder, Constants.Jdk64bitArchiveFileName);
                jdkExtractPath = Path.Combine(InstallationFolder, Constants.Jdk64bitExtractDirName);
                jdkFinalPath = Path.Combine(InstallationFolder, Constants.Jdk64bitFinalPath);
            }
            else
            {
                downloadURL = Constants.Jdk32bitURL;
                targetFileName = Path.Combine(InstallationFolder, Constants.Jdk32bitArchiveFileName);
                jdkExtractPath = Path.Combine(InstallationFolder, Constants.Jdk32bitExtractDirName);
                jdkFinalPath = Path.Combine(InstallationFolder, Constants.Jdk32bitFinalPath);
            }

            // Does the destination folder already exist?
            if (Directory.Exists(jdkFinalPath))
            {
                return false;
            }

            // Download the file
            await WebUtils.DownloadAsync(downloadURL, targetFileName);
            if (!File.Exists(targetFileName))
            {
                return false;
            }

            // Decompress the file
            FileSystem.ExtractZIPFileToFolder(targetFileName, InstallationFolder);

            // Check that the extract folder exists
            if (!Directory.Exists(jdkExtractPath))
            {
                return false;
            }

            // Move the JRE subfolder in the final location
            Directory.Move(Path.Combine(jdkExtractPath, "jre"), jdkFinalPath);

            // Delete temporary files and folders
            File.Delete(targetFileName);
            Directory.Delete(jdkExtractPath, recursive: true);

            // Finally, return true if the jre folder is in the final location
            return Directory.Exists(jdkFinalPath);
        }

        private void setUIDefaults()
        {
            // Get the installation directory from the application settings
            string installationDir = (string)appSettings.Get("Paths", "InstallationDir");
            if (installationDir.Equals(""))
            {
                buttonOBITInstallationDirectory.Text = "Pick oBIT installation dir...";
                oBITInstallationFolder = "";
            }
            else
            {
                buttonOBITInstallationDirectory.Text = installationDir;
                oBITInstallationFolder = installationDir;
            }

            // Set the platform bits
            if (Environment.Is64BitOperatingSystem)
            {
                radioButtonPlatform32bit.Checked = false;
                radioButtonPlatform64bit.Checked = true;
            }
            else
            {
                radioButtonPlatform32bit.Checked = true;
                radioButtonPlatform64bit.Checked = false;
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
        /// <param name="is64bit">True if the 64-bit version of packages should be downloaded, false otherwise.</param>
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
    }
}
