using System;
using System.Security.AccessControl;
using System.Security.Policy;
using obit_manager_settings.components.io;
using obit_manager_api.core;


namespace obit_manager_settings.components
{

    /// <summary>
    /// Datamover data class.
    /// </summary>
    public class Datamover
    {
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
        public string LocalUserAccout { get; set; } = "openbis";

        // Datamover incoming target
        [Setting(Configuration = "Datamover", Component = "Datamover")]
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
        /// Constructor.
        /// </summary>
        public Datamover()
        {
        }

        /// <summary>
        /// Alternative constructor.
        /// </summary>
        /// <param name="DatamoverIncomingDir">Datamover incoming directory.</param>
        /// <param name="datamoverJslSettingsParser">A DatamoverJSLSettingsParser object.</param>
        /// <param name="datamoverSettingsParser">A DatamoverSettingsParser object.</param>
        public Datamover(string datamoverIncomingDir,
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

                        break;
                    }
                }
            }

            // Find the DatamoverJSL configuration that fits the client
            if (!datamoverJSLPath.Equals(""))
            {
                foreach (string key in datamoverJslSettingsParser.GetRelativeDatamoverJSLDirs())
                {
                    if (FileSystem.ComparePaths(key, datamoverJSLPath))
                    {
                        // Found the correct setting; fill the values
                        Fill(key, datamoverJslSettingsParser);

                        break;
                    }
                }
            }
        }

        /// <summary>
        /// Set the relevant settings from the DatamoverSettingsParser object.
        /// </summary>
        /// <param name="key">Key of the DatamoverSettingsParser map.</param>
        /// <param name="datamoverSettingsParser">DatamoverSettingsParser object.</param>
        private void Fill(string key, DatamoverSettingsParser datamoverSettingsParser)
        {
            this.IncomingTarget =
                FileSystem.ChangeBackwardToForwardSlashesInPath(datamoverSettingsParser.Get(key, "incoming-target"));
            this.SkipAccessibilityTestOnIncoming = StringToBool(datamoverSettingsParser.Get(key, "skip-accessibility-test-on-incoming"));
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
            this.SkipAccessibilityTestOnOutgoing = StringToBool(datamoverSettingsParser.Get(key, "skip-accessibility-test-on-outgoing"));
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
            this.LocalUserAccout = datamoverJSLSettingsParser.Get(key, "service", "account");
        }

        /// <summary>
        /// Boolean to string representation.
        /// </summary>
        /// <param name="value">Boolean value.</param>
        /// <returns>String representation of the boolean.</returns>
        private string BoolToString(bool value)
        {
            if (value == true)
            {
                return "true";
            }

            return "false";
        }

        /// <summary>
        /// String to boolean representation.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        private bool StringToBool(string value)
        {
            if (value.ToLowerInvariant() == "true")
            {
                return true;
            }

            return false;
        }
    }
}
