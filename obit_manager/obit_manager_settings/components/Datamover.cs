using System;
using System.Security.AccessControl;
using System.Security.Policy;
using obit_manager_settings.components.io;
using obit_manager_api.core;
using NLog;
using System.Configuration;
using System.IO;

namespace obit_manager_settings.components
{

    /// <summary>
    /// Datamover data class.
    /// </summary>
    public class Datamover
    {
        /// <summary>
        /// Logger
        /// </summary>
        private static Logger sLogger = LogManager.GetCurrentClassLogger();

        // Subfolder (relative to the installation directory) where this Datamover instance is installed.
        // This is not a Setting.
        public string InstallationSubDir { get; set; } = "obit_datamover_jsl";

        // Application name
        [Setting(Configuration = "Datamover_JSL", Component = "Datamover_JSL")]
        public string AppName { get; set; } = "Datamover";

        // Service name
        [Setting(Configuration = "Datamover_JSL", Component = "Datamover_JSL")]
        public string ServiceName { get; set; } = "Datamover";

        // Display name
        [Setting(Configuration = "Datamover_JSL", Component = "Datamover_JSL")]
        public string DisplayName { get; set; } = "Datamover";

        // Service description
        [Setting(Configuration = "Datamover_JSL", Component = "Datamover_JSL")]
        public string ServiceDescription { get; set; } = "Datamover as Windows Service";

        // Path to the private key to use to log in to the DataStore Server
        [Setting(Configuration = "Datamover_JSL", Component = "Datamover_JSL")]
        public string DataStoreServerLocalPrivateKeyPath { get; set; } = string.Empty;

        // Local user account to run Datamover as a Windows service 
        [Setting(Configuration = "Datamover_JSL", Component = "Datamover_JSL")]
        public string LocalUserAccount { get; set; } = "openbis";

        // Datamover incoming target
        [Setting(Configuration = "AnnotationTool;Datamover", Component = "Datamover")]
        public string IncomingTarget { get; set; } = @"D:/Datamover/incoming";

        // Skip accesibility test on incoming?
        [Setting(Configuration = "Datamover", Component = "Datamover")]
        public bool SkipAccessibilityTestOnIncoming { get; set; } = false;

        // Datamover buffer directory
        [Setting(Configuration = "Datamover", Component = "Datamover")]
        public string BufferDir { get; set; } = @"D:/Datamover/buffer";

        // Datamover buffer directory high watermark level
        [Setting(Configuration = "Datamover", Component = "Datamover")]
        public int BufferDirHighwaterMark { get; set; } = 1048576;

        // Datamover outgoing directory
        [Setting(Configuration = "Datamover", Component = "Server")]
        public string OutgoingTarget { get; set; } = string.Empty;

        // Datamover outgoing directory high watermark level
        [Setting(Configuration = "Datamover", Component = "Server")]
        public int OutgoingTargetHighwaterMark { get; set; } = 1048576;

        // Skip accesibility test on outgoing?
        [Setting(Configuration = "Datamover", Component = "Datamover")]
        public bool SkipAccessibilityTestOnOutgoing { get; set; } = true;

        // Relative path to the data completed script
        [Setting(Configuration = "Datamover", Component = "Datamover")]
        public string DataCompletedScript { get; set; } = "scripts/data_completed_script.bat";

        // Datamover manual intervention directory
        [Setting(Configuration = "Datamover", Component = "Datamover")]
        public string ManualInterventionDir { get; set; } = "D:/Datamover/manual_intervention";

        // Quiet period
        [Setting(Configuration = "Datamover", Component = "Datamover")]
        public int QuietPeriod { get; set; } = 60;

        // Check interval
        [Setting(Configuration = "Datamover", Component = "Datamover")]
        public int CheckInterval { get; set; } = 60;

        // (Optional) Full path tp the lastchanged executable on the DataStore Server
        [Setting(Configuration = "Datamover", Component = "Server")]
        public string OutgoingHostLastchangedExecutable { get; set; } = string.Empty;

        /// <summary>
        /// Alternative constructor.
        /// </summary>
        /// <param name="datamoverInstallationSubDir">Datamover installation subdirectory (relative to the oBIT installation directory.</param>
        /// <param name="DatamoverIncomingDir">Datamover incoming directory.</param>
        /// <param name="datamoverJslSettingsParser">A DatamoverJSLSettingsParser object.</param>
        /// <param name="datamoverSettingsParser">A DatamoverSettingsParser object.</param>
        public Datamover(
            string datamoverIncomingDir,
            DatamoverJSLSettingsParser datamoverJslSettingsParser,
            DatamoverSettingsParser datamoverSettingsParser)
        {
            // First, find the Datamover configuration that fits the client
            string datamoverJSLPath = "";
            foreach (string key in datamoverSettingsParser.Configurations)
            {
                string path1 = datamoverSettingsParser.Get(key, "incoming-target");
                if (path1 != null)
                {
                    string path2 = datamoverIncomingDir;

                    if (FileSystem.ComparePaths(path1, path2))
                    {
                        // Found the correct setting; fill the values
                        Fill(key, datamoverSettingsParser);

                        // Store the path
                        datamoverJSLPath = key;

                        // Inform
                        sLogger.Info("Processed Datamover configuration file from Datamover JSL parent folder '" + datamoverJSLPath + "'.");

                        break;
                    }
                }
            }

            // If we could not find the expected DatamoverJSL configuration that fits the client,
            // something is wrong in the configuration files.
            if (datamoverJSLPath.Equals(""))
            {
                string msg = "No known Datamover JSL configuration uses the incoming folder '" + datamoverIncomingDir + "'.";
                sLogger.Error(msg);
                throw new ConfigurationException(msg);
            }

            // Keep track of whether we find the DatamoverJSL configuration
            bool found = false;

            // Find the DatamoverJSL configuration that fits the client
            foreach (string key in datamoverJslSettingsParser.GetRelativeDatamoverJSLDirs())
            {
                if (FileSystem.ComparePaths(key, datamoverJSLPath))
                {
                    // Found the correct setting; fill the values
                    Fill(key, datamoverJslSettingsParser);

                    // Inform
                    sLogger.Info("Processed Datamover JSL configuration file from folder '" + key + "'.");

                    // Set found to true
                    found = true;

                    break;
                }
            }


            if (!found)
            {
                string msg = "No known Datamover JSL configuration uses the incoming folder '" + datamoverIncomingDir + "'.";
                sLogger.Error(msg);
                throw new ConfigurationException(msg);
            }
        }

        /// <summary>
        /// Copy constructor.
        /// </summary>
        /// <param name="other">Datamover object to copy.</param>
        public Datamover(Datamover other)
        {
            this.AppName = string.Copy(other.AppName);
            this.ServiceName = string.Copy(other.ServiceName);
            this.DisplayName = string.Copy(other.DisplayName);
            this.ServiceDescription = string.Copy(other.ServiceDescription);
            this.DataStoreServerLocalPrivateKeyPath = string.Copy(other.DataStoreServerLocalPrivateKeyPath);
            this.LocalUserAccount = string.Copy(other.LocalUserAccount);
            this.IncomingTarget = string.Copy(other.IncomingTarget);
            this.SkipAccessibilityTestOnIncoming = other.SkipAccessibilityTestOnIncoming;
            this.BufferDir = string.Copy(other.BufferDir);
            this.BufferDirHighwaterMark = other.BufferDirHighwaterMark;
            this.OutgoingTarget = other.OutgoingTarget;
            this.OutgoingTargetHighwaterMark = other.OutgoingTargetHighwaterMark;
            this.SkipAccessibilityTestOnOutgoing = other.SkipAccessibilityTestOnOutgoing;
            this.DataCompletedScript = string.Copy(other.DataCompletedScript);
            this.ManualInterventionDir = string.Copy(other.ManualInterventionDir);
            this.QuietPeriod = other.QuietPeriod;
            this.CheckInterval = other.CheckInterval;
            this.OutgoingHostLastchangedExecutable = other.OutgoingHostLastchangedExecutable;
        }

        /// <summary>
        /// Set the relevant settings from the DatamoverSettingsParser object.
        /// </summary>
        /// <param name="key">Key of the DatamoverSettingsParser map.</param>
        /// <param name="datamoverSettingsParser">DatamoverSettingsParser object.</param>
        private void Fill(string key, DatamoverSettingsParser datamoverSettingsParser)
        {
            this.InstallationSubDir = GetDatamoverJSLSubDirName(key);
            this.IncomingTarget =
                FileSystem.ChangeBackwardToForwardSlashesInPath(datamoverSettingsParser.Get(key, "incoming-target"));
            this.SkipAccessibilityTestOnIncoming = Utils.StringToBool(datamoverSettingsParser.Get(key, "skip-accessibility-test-on-incoming"));
            this.BufferDir = FileSystem.ChangeBackwardToForwardSlashesInPath(datamoverSettingsParser.Get(key, "buffer-dir"));
            if (Int32.TryParse(datamoverSettingsParser.Get(key, "buffer-dir-highwater-mark"), out int tmpBufferDirHighwaterMark))
            {
                this.BufferDirHighwaterMark = tmpBufferDirHighwaterMark;
            }
            this.OutgoingTarget = datamoverSettingsParser.Get(key, "outgoing-target");
            if (Int32.TryParse(datamoverSettingsParser.Get(key, "outgoing-target-highwater-mark"), out int tmpOutgoingTargetHighwaterMark))
            {
                this.OutgoingTargetHighwaterMark = tmpOutgoingTargetHighwaterMark;
            }
            this.SkipAccessibilityTestOnOutgoing = Utils.StringToBool(datamoverSettingsParser.Get(key, "skip-accessibility-test-on-outgoing"));
            this.DataCompletedScript = FileSystem.ChangeBackwardToForwardSlashesInPath(datamoverSettingsParser.Get(key, "data-completed-script"));
            this.ManualInterventionDir = FileSystem.ChangeBackwardToForwardSlashesInPath(datamoverSettingsParser.Get(key, "manual-intervention-dir"));
            if (Int32.TryParse(datamoverSettingsParser.Get(key, "quiet-period"), out int tmpQuietPeriod))
            {
                this.QuietPeriod = tmpQuietPeriod;
            }
            if (Int32.TryParse(datamoverSettingsParser.Get(key, "check-interval"), out int tmpCheckInterval))
            {
                this.CheckInterval = tmpCheckInterval;
            }
            this.OutgoingHostLastchangedExecutable = this.DataCompletedScript = datamoverSettingsParser.Get(key, "outgoing-host-lastchanged-executable"); ;
        }

        /// <summary>
        /// Set the relevant settings from the DatamoverJSLSettingsParser object.
        /// </summary>
        /// <param name="key">Key of the DatamoverJSLSettingsParser map.</param>
        /// <param name="datamoverJSLSettingsParser">DatamoverJSLSettingsParser object.</param>
        private void Fill(string key, DatamoverJSLSettingsParser datamoverJSLSettingsParser)
        {
            this.AppName = datamoverJSLSettingsParser.Get(key, "service", "appname");
            this.ServiceName = datamoverJSLSettingsParser.Get(key, "service", "servicename");
            this.DisplayName = datamoverJSLSettingsParser.Get(key, "service", "displayname");
            this.ServiceDescription = datamoverJSLSettingsParser.Get(key, "service", "servicedescription");
            //this.DataStoreServerLocalPrivateKeyPath = string.Empty;
            this.LocalUserAccount = datamoverJSLSettingsParser.Get(key, "service", "account");
        }

        /// <summary>
        /// Return the DatamoverJSL subfolder from the full path.
        /// </summary>
        /// <param name="fullPath">Full path to the DatamoverJSL folder.</param>
        private string GetDatamoverJSLSubDirName(string fullPath)
        {
            // Make sure to have a clean Windows full path with no trailing separator
            String tmp = FileSystem.NormalizePath(fullPath);

            // Now extract and return the last part of the path
            return Path.GetFileName(tmp);
        }
    }
}
