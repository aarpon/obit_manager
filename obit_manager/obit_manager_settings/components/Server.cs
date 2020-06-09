using System;
using System.Runtime.Remoting.Messaging;
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
            // @Todo Implement me!

            /*
            this.ApplicationServerHostname = "";
            this.ApplicationServerPort = 0;
            this.ApplicationServerAcceptSelfSignedCert = false;
            this.DataStoreServerHostname = "localhost";
            this.DataStoreServerUserName = "openbis";
            this.DataStoreServerPathToRootDropboxFolder = "/home/openbis/data";
            this.DataStoreServerPathToLastChangedExecutable = string.Empty;
            */
        }

    }
}
