using obit_manager_api.core;
using obit_manager_config;
using obit_manager_settings;
using obit_manager_settings.components;
using System;
using System.IO;


namespace obit_manager_gui.components
{
    internal partial class InstallationManager
    {
        // Logger
        private static readonly NLog.Logger sLogger = NLog.LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Last error message.
        /// </summary>
        public string ErrorMessage { get; private set; } = "";

        /// <summary>
        /// Constructor.
        /// </summary>
        public InstallationManager()
        {
        }

        /// <summary>
        /// Applies all changes to bring current Settings in sync with a final,
        /// working installation.
        /// </summary>
        public bool Apply()
        {
            // Update notifications
            this.SendToLogAndOutputPane("Installation/update operation started.");

            // Prepare final status
            bool success = false;

            // Start with the installation directory
            success = this.PrepareInstallationDir();

            if (success == false)
            {
                // Send failure
                this.SendToLogAndOutputPane("Operation failed: " + this.ErrorMessage);
            }

            // Check if the expected tools subdirectories exist in the Installation Dir,
            // otherwise download and setup.
            success = this.SearchAndPrepareTools();

            if (success == true)
            {
                // Send success
                this.SendToLogAndOutputPane("Operation completed successfully.");
            }
            else
            {
                // Send failure
                this.SendToLogAndOutputPane("Operation failed: " + this.ErrorMessage);
            }

            // Reset the error message
            this.ErrorMessage = "";

            // Return success status
            return success;
        }

        /// <summary>
        /// Apply all operations to
        /// </summary>
        /// <returns></returns>
        private bool PrepareInstallationDir()
        {
            // Set an alias to the SettingsManager
            SettingsManager settingsManager = SettingsManager.Get();

            // Send to operations bar
            this.SendToOperations("Prepare installation directory.");

            // First check if the installation directory exists.
            // If not create it.
            bool created;
            try
            {
                created = FileSystem.CreateIfDoesNotExist(settingsManager.InstallationDir);
            }
            catch (Exception e)
            {
                this.ErrorMessage = e.Message;
                created = false;
            }

            if (!created)
            {
                // Log
                this.SendToLogOnly("Installation directory '" +
                    settingsManager.InstallationDir + "' already exists.");
            }
            else
            {
                // Log
                this.SendToLogOnly("Created installation directory '" +
                    settingsManager.InstallationDir + "'.");
            }

            // Clear operations bar
            this.ClearOperations();

            // Return success
            return true;
        }

        /// <summary>
        /// Check if the expected tools subdirectories exist in the Installation Dir,
        // otherwise download and configure them.
        /// </summary>
        /// <returns></returns>
        private bool SearchAndPrepareTools()
        {
            // Set an alias to the SettingsManager
            SettingsManager settingsManager = SettingsManager.Get();

            // Send to operations bar
            this.SendToOperations("Look for oBIT tools.", true);

            // Set some flags
            bool downloadAnnotationTool = false;
            bool downloadDatamover = false;
            bool downloadJRE = false;

            // Check for the existence of the Annotation Tool
            string annotationToolPath = Path.Combine(
                settingsManager.InstallationDir,
                "obit_annotation_tool"
            );
            if (Directory.Exists(annotationToolPath))
            {
                this.SendToLogAndOutputPane("Annotation Tool found in '" +
                    annotationToolPath + "'.");
            }
            else
            {
                this.SendToLogAndOutputPane("Annotation Tool not installed yet.");

                // Does the expected archive exist?
                string annotationToolArchiveFileName;
                if (Environment.Is64BitOperatingSystem)
                {
                    annotationToolArchiveFileName = Constants.AnnotationTool64bitArchiveFileName;
                }
                else
                {
                    annotationToolArchiveFileName = Constants.AnnotationTool32bitArchiveFileName;
                }
                if (! File.Exists(Path.Combine(
                    settingsManager.InstallationDir,
                    annotationToolArchiveFileName)))
                {
                    downloadAnnotationTool = true;
                }

            }

            // Get the list of Datamover installations
            for (int i = 0; i < settingsManager.NumInstances; i++)
            {
                // Get current Datamover
                Datamover datamover = settingsManager.GetDatamoverFromInstanceWithIndex(i);

                // Does the folder exist?
                string datamoverJSLPath = Path.Combine(
                    settingsManager.InstallationDir,
                    datamover.InstallationSubDir
                );
                if (Directory.Exists(datamoverJSLPath))
                {
                    this.SendToLogAndOutputPane("Datamover '" + datamover.ServiceName + "' found in '" +
                        datamoverJSLPath + "'.");
                }
                else
                {
                    this.SendToLogAndOutputPane("Datamover '" + datamover.ServiceName +
                        "' not installed yet.");
                }
            }

            // Check for the existance of a Java runtime
            string jrePath = Path.Combine(
                settingsManager.InstallationDir,
                "jre"
            );
            if (Directory.Exists(jrePath))
            {
                this.SendToLogAndOutputPane("Java runtime found in '" +
                    jrePath + "'.");
            }
            else
            {
                this.SendToLogAndOutputPane("Java runtime not found in '" +
                    settingsManager.InstallationDir + "'.");
            }

            // Process the instances
            for (int i = 0; i < settingsManager.NumInstances; i++)
            {
                // Get the client
                Client client = settingsManager.GetClientFromInstanceWithIndex(i);

                // Check for existance of the
            }

            return true;
        }

        /// <summary>
        /// Send text to Log file and to main window output pane.
        /// </summary>
        /// <param name="text">Text to be sent.</param>
        private void SendToLogAndOutputPane(string text)
        {
            // Send to log
            InstallationManager.sLogger.Info(text);

            // Send to output pane
            InstallationManager.OnTextToDisplayToOutputPaneReady(this, text);
        }

        /// <summary>
        /// Send text to Operations bar. If needed also to Log and OutputPane.
        /// </summary>
        ///
        /// Operations are meant to have higher granularity than the rest of the
        /// notifications; therefore, other types of reports are optional here.
        ///
        /// <param name="text">Text to be sent.</param>
        /// <param name="toAll">Set to true to send to Log and Output pane as well.</param>
        private void SendToOperations(string text, bool toAll = false)
        {
            // Sent to Operations field
            InstallationManager.OnOperationDescriptionChange(this, text);

            // If needed, send to All
            if (toAll == true)
            {
                this.SendToLogAndOutputPane(text);
            }
        }

        /// <summary>
        /// Send text to Log file only.
        /// </summary>
        /// <param name="text">Text to be sent.</param>
        private void SendToLogOnly(string text)
        {
            // Send to log
            InstallationManager.sLogger.Info(text);
        }

        /// <summary>
        /// Clear the operations bar.
        /// </summary>
        private void ClearOperations()
        {
            // Send an empty string to operations.
            this.SendToOperations("", false);
        }
    }
}