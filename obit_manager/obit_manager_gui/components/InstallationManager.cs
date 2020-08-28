using obit_manager_api.core;
using obit_manager_settings;

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

            // Send to operations
            this.SendToOperations("Prepare installation directory.");

            // First check if the installation directory exists.
            // If not create it.
            if (FileSystem.CreateIfDoesNotExist(settingsManager.InstallationDir))
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

            // Return success
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
        /// Send text to Operations output. If needed also to Log and OutputPane.
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
    }
}
