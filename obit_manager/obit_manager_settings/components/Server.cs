using System;
using System.Configuration;
using System.Net.Configuration;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Text.RegularExpressions;
using NLog;
using NLog.Config;
using obit_manager_api.core;
using obit_manager_settings.components.io;

namespace obit_manager_settings.components
{
    /// <summary>
    /// Server data class.
    /// </summary>
    public class Server
    {
        /// <summary>
        /// Logger
        /// </summary>
        private static Logger sLogger = LogManager.GetCurrentClassLogger();

        // Application Server host name
        [Setting(Configuration = "AnnotationTool", Component = "Server")]
        public string ApplicationServerHostname { get; set; } = string.Empty;

        // Application Server port number (it can be null)
        [Setting(Configuration = "AnnotationTool", Component = "Server")]
        public int? ApplicationServerPort { get; set; } = null;

        // Does the Application Server accept self-signed certificates?
        [Setting(Configuration = "AnnotationTool", Component = "Server")]
        public bool ApplicationServerAcceptSelfSignedCert { get; set; } = false;

        // DataStore Server host name
        [Setting(Configuration = "Datamover", Component = "Server")]
        public string DataStoreServerHostname { get; set; } = "localhost";

        // Unix account on the DataStore Server
        [Setting(Configuration = "Datamover", Component = "Server")]
        public string DataStoreServerUserName { get; set; } = "openbis";

        // Full path to the dropbox root folder on the DataStore Server
        [Setting(Configuration = "Datamover", Component = "Server")]
        public string DataStoreServerPathToRootDropboxFolder { get; set; } = "/home/openbis/data";

        [Setting(Configuration = "AnnotationTool", Component = "Server")]
        public string DataStoreServerHardwareClass { get; set; } = "";

        // (Optional) Full path to the lastchanged executable on the DataStore Server
        [Setting(Configuration = "Datamover", Component = "Server")]
        public string DataStoreServerPathToLastChangedExecutable { get; set; } = string.Empty;


        /// <summary>
        /// Default constructor.
        /// </summary>
        public Server()
        {
            // Keep the default values.
        }

        /// <summary>
        /// Alternative constructor.
        /// </summary>
        public Server(string datamoverIncomingDir, DatamoverSettingsParser datamoverSettingsParser)
        {
            // Keep track of whether the expected configuration is found.
            bool found = false;

            // Find the Datamover configuration that fits the client
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

                        // Inform
                        sLogger.Info(
                            "Filled server information from JSL configuration in folder '" + key + "'");

                        // Set found  to true
                        found = true;

                        break;
                    }
                }
            }

            // Was the expected configuration found!
            if (!found)
            {
                string msg = "No known Datamover configuration uses the incoming folder '" + datamoverIncomingDir + "'.";
                sLogger.Error(msg);
                throw new ConfigurationException(msg);
            }
        }


        /// <summary>
        /// Set the relevant settings from the DatamoverSettingsParser object.
        /// </summary>
        /// <param name="key">Key of the DatamoverSettingsParser map.</param>
        /// <param name="datamoverSettingsParser">DatamoverSettingsParser object.</param>
        private void Fill(string key, DatamoverSettingsParser datamoverSettingsParser)
        {
            // @Todo These are not in the Datamover configuration!
            sLogger.Warn("Some Server infomation must be retrieved from the Annotation Tool configuration!");
            this.ApplicationServerHostname = "";
            this.ApplicationServerPort = 0;
            this.ApplicationServerAcceptSelfSignedCert = false;

            // Get the DSS settings
            ServerStringComponents s = parseOutgoingTargetString(datamoverSettingsParser.Get(key, "outgoing-target"));
            this.DataStoreServerHostname = s.Server;
            this.DataStoreServerUserName = s.UserName;
            this.DataStoreServerPathToRootDropboxFolder = s.DropboxRoot;
            this.DataStoreServerHardwareClass = s.Hardware;

            // Get the last-changed executable
            this.DataStoreServerPathToLastChangedExecutable = datamoverSettingsParser.Get(key, "outgoing-host-lastchanged-executable");

        }

        /// <summary>
        /// Decompose the 'outgoing-target' setting into its components.
        /// </summary>
        /// <param name="outgoingTargetString">Value of the 'outgoing-target' setting from the Datamover configuration.</param>
        /// <returns></returns>
        private ServerStringComponents parseOutgoingTargetString(string outgoingTargetString)
        {
            // Initialize ServerStringComponents object
            ServerStringComponents s;
            s.UserName = "";
            s.Server = "";
            s.Port = -1;
            s.DropboxRoot = "";
            s.Hardware = "";

            string pattern = @"(?<username>.+)@(?<server>.+):(?<port>\d*)(?<dropbox>.+)incoming-(?<hardware>.+)";
            MatchCollection matches = Regex.Matches(outgoingTargetString, pattern);

            if (matches.Count == 0)
            {
                return s;
            }

            // Only one match is expected
            foreach (Match match in matches)
            {
                // Fill in the structure
                s.UserName = match.Groups["username"].Value;
                s.Server = match.Groups["server"].Value;
                s.Port = match.Groups["port"].Value.Equals("") ? -1 : Int32.Parse(match.Groups["port"].Value);
                s.DropboxRoot = match.Groups["dropbox"].Value;
                s.Hardware = match.Groups["hardware"].Value;
            }

            return s;
        }

        /// <summary>
        /// Build the 'outgoing-target' Datamover setting from its components (stored in a ServerStringComponents structure).
        /// </summary>
        /// <param name="outgoingTargetString">Value of the 'outgoing-target' setting from the Datamover configuration.</param>
        /// <returns></returns>
        private string BuildOutgoingTargetString(ServerStringComponents s)
        {
            StringBuilder builder = new StringBuilder();

            builder.Append(s.UserName);
            builder.Append("@");
            builder.Append(s.Server);
            builder.Append(":");
            if (s.Port != -1)
            {
                builder.Append(s.Port.ToString());
            }
            builder.Append(s.DropboxRoot);
            builder.Append("/incoming-");
            builder.Append(s.Hardware);

            return builder.ToString();
        }
    }

    /// <summary>
    /// Structure of all components of the 'outgoing-target' string.
    /// </summary>
    struct ServerStringComponents
    {
        public string UserName;
        public string Server;
        public int Port;
        public string DropboxRoot;
        public string Hardware;
    };
}
