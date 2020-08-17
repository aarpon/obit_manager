using System;
using System.Configuration;
using System.Net.Configuration;
using System.Runtime.Remoting.Messaging;
using System.Security.Policy;
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

        // Applicatiion Server protocol (http or https)
        [Setting(Configuration = "AnnotationTool", Component = "Server")] 
        public string ApplicationServerProtocol { get; set; } = "https";

        // Application Server host name
        [Setting(Configuration = "AnnotationTool", Component = "Server")]
        public string ApplicationServerHostname { get; set; } = string.Empty;

        // Application Server port number (it can be null)
        [Setting(Configuration = "AnnotationTool", Component = "Server")]
        public int? ApplicationServerPort { get; set; } = null;

        [Setting(Configuration = "AnnotationTool", Component = "Server")]
        public string ApplicationServerPath { get; set; } = "openbis";

        // Does the Application Server accept self-signed certificates?
        [Setting(Configuration = "AnnotationTool", Component = "Server")]
        public string ApplicationServerAcceptSelfSignedCert { get; set; } = "no";

        // DataStore Server host name
        [Setting(Configuration = "Datamover", Component = "Server")]
        public string DataStoreServerHostname { get; set; } = "localhost";

        // Unix account on the DataStore Server
        [Setting(Configuration = "Datamover", Component = "Server")]
        public string DataStoreServerUserName { get; set; } = "openbis";

        // Full path to the dropbox root folder on the DataStore Server
        [Setting(Configuration = "Datamover", Component = "Server")]
        public string DataStoreServerPathToRootDropboxFolder { get; set; } = "/home/openbis/data/";

        [Setting(Configuration = "AnnotationTool", Component = "Server")]
        public string DataStoreServerHardwareClass { get; set; } = "";

        [Setting(Configuration = "AnnotationTool", Component = "Server")]
        public string DataStoreServerHardwareSubClass { get; set; } = "";

        // (Optional) Full path to the lastchanged executable on the DataStore Server
        [Setting(Configuration = "Datamover", Component = "Server")]
        public string DataStoreServerPathToLastChangedExecutable { get; set; } = string.Empty;

        // This property is not stored: it is built on the fly on request
        public string DataStoreServerFullTargetString
        {
            get => this.BuildOutgoingTargetString();
        }

        // This property is not stored: it is built on the fly on request
        public string ApplicationServerURL
        {
            get => this.BuildApplicationServerString();
        }

        // This property is not stored: it is built on the fly on request
        public string Label {
            get
            {
                if (this.DataStoreServerHardwareClass.ToLower().Equals("microscopy"))
                {
                    return this.DataStoreServerHostname + " (" + this.DataStoreServerHardwareClass + ")";
                }
                else
                {
                    return this.DataStoreServerHostname + " (" + this.DataStoreServerHardwareClass +
                           ": " + Hardware.Inventory[this.DataStoreServerHardwareSubClass].Name +
                           ")";
                }
            }
        } 

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
        public Server(Client client, DatamoverSettingsParser datamoverSettingsParser)
        {
            // Keep track of whether the expected configuration is found.
            bool found = false;

            // Find the Datamover configuration that fits the client
            foreach (string key in datamoverSettingsParser.Configurations)
            {
                string path1 = datamoverSettingsParser.Get(key, "incoming-target");
                if (path1 != null)
                {
                    string path2 = client.DatamoverIncomingDir;

                    if (FileSystem.ComparePaths(path1, path2))
                    {
                        // Found the correct setting; fill the values
                        Fill(key, client, datamoverSettingsParser);

                        // Inform
                        sLogger.Info(
                            "Filled server information from JSL configuration in folder '" + key + "'");

                        // Set found  to true
                        found = true;

                        break;
                    }
                }
            }

            // Was the expected configuration found?
            if (!found)
            {
                string msg = "No known Datamover configuration uses the incoming folder '" + client.DatamoverIncomingDir + "'.";
                sLogger.Error(msg);
                throw new ConfigurationException(msg);
            }
        }

        // Copy constructor.
        public Server(Server other)
        {
            // Copy all properties
            this.ApplicationServerProtocol = String.Copy(other.ApplicationServerProtocol);
            this.ApplicationServerHostname = String.Copy(other.ApplicationServerHostname);
            this.ApplicationServerPort = other. ApplicationServerPort;
            this.ApplicationServerPath = String.Copy(other.ApplicationServerPath);
            this.ApplicationServerAcceptSelfSignedCert = other.ApplicationServerAcceptSelfSignedCert;
            this.DataStoreServerHostname = String.Copy(other.DataStoreServerHostname);
            this.DataStoreServerUserName = String.Copy(other.DataStoreServerUserName);
            this.DataStoreServerPathToRootDropboxFolder = String.Copy(other.DataStoreServerPathToRootDropboxFolder);
            this.DataStoreServerHardwareClass = String.Copy(other.DataStoreServerHardwareClass);
            this.DataStoreServerHardwareSubClass = String.Copy(other.DataStoreServerHardwareSubClass);
            this.DataStoreServerPathToLastChangedExecutable = String.Copy(other.DataStoreServerPathToLastChangedExecutable);
        }

        /// <summary>
        /// Set the relevant settings from the DatamoverSettingsParser object.
        /// </summary>
        /// <param name="key">Key of the DatamoverSettingsParser map.</param>
        /// <param name="datamoverSettingsParser">DatamoverSettingsParser object.</param>
        private void Fill(string key, Client client, DatamoverSettingsParser datamoverSettingsParser)
        {
            // @Todo These are not in the Datamover configuration!
            sLogger.Warn("Some Server infomation must be retrieved from the Annotation Tool configuration!");
            ApplicationServerStringComponents asc = parseOpenBISURL(client.OpenBISURL);
            this.ApplicationServerProtocol = asc.Protocol;
            this.ApplicationServerHostname = asc.Server;
            this.ApplicationServerPort = asc.Port;
            this.ApplicationServerPath = asc.Path;
            this.ApplicationServerAcceptSelfSignedCert = client.AcceptSelfSignedCertificates;

            // Get the DSS settings
            DataStoreServerStringComponents dssc = parseOutgoingTargetString(datamoverSettingsParser.Get(key, "outgoing-target"));
            this.DataStoreServerHostname = dssc.Server;
            this.DataStoreServerUserName = dssc.UserName;
            this.DataStoreServerPathToRootDropboxFolder = dssc.DropboxRoot;
            this.DataStoreServerHardwareClass = dssc.HardwareClass;
            this.DataStoreServerHardwareSubClass = dssc.HardwareSubClass;

            // Get the last-changed executable
            this.DataStoreServerPathToLastChangedExecutable = datamoverSettingsParser.Get(key, "outgoing-host-lastchanged-executable");
        }

        /// <summary>
        /// Decompose the 'outgoing-target' setting into its components.
        /// </summary>
        /// <param name="outgoingTargetString">Value of the 'outgoing-target' setting from the Datamover configuration.</param>
        /// <returns></returns>
        private DataStoreServerStringComponents parseOutgoingTargetString(string outgoingTargetString)
        {
            // Initialize DataStoreServerStringComponents object
            DataStoreServerStringComponents s;
            s.UserName = "";
            s.Server = "";
            s.DropboxRoot = "";
            s.HardwareClass = "";
            s.HardwareSubClass = "";

            string pattern = @"(?<username>.+)@(?<server>.+):(?<port>\d*)(?<dropbox>.+)incoming-(?<hardware>.+)";
            MatchCollection matches = Regex.Matches(outgoingTargetString, pattern);

            if (matches.Count == 0)
            {
                return s;
            }

            // Only one match is expected
            foreach (Match match in matches)
            {
                // Fill in the structure (we ignore the port, that shouldn't be there anyway)
                s.UserName = match.Groups["username"].Value;
                s.Server = match.Groups["server"].Value;
                s.DropboxRoot = match.Groups["dropbox"].Value;
                s.HardwareClass = match.Groups["hardware"].Value.ToLower().Equals("microscopy")
                    ? "Microscopy"
                    : "Flow cytometry";
                s.HardwareSubClass = match.Groups["hardware"].Value;
            }

            return s;
        }

        /// <summary>
        /// Decompose the 'openBISURL' setting into its components.
        /// </summary>
        /// <param name="openBISURL">Value of the openBISURL setting from the Annotation Tool configuration.</param>
        /// <returns></returns>
        private ApplicationServerStringComponents parseOpenBISURL(string openBISURL)
        {
            // Initialize DataStoreServerStringComponents object
            ApplicationServerStringComponents s;
            s.Protocol = "";
            s.Server = "";
            s.Port = -1;
            s.Path = "";

            string pattern = @"^(?<protocol>http|https)\:\/\/(?<server>[-.\w]*[0-9a-zA-Z])(\:(?<port>\d{4})*)*\/(?<path>.+)?$";
            MatchCollection matches = Regex.Matches(openBISURL, pattern);

            if (matches.Count == 0)
            {
                return s;
            }

            // Only one match is expected
            foreach (Match match in matches)
            {
                // Fill in the structure
                s.Protocol = match.Groups["protocol"].Value;
                s.Server = match.Groups["server"].Value;
                s.Port = match.Groups["port"].Value.Equals("") ? -1 : Int32.Parse(match.Groups["port"].Value);
                s.Path = match.Groups["path"].Value;
            }

            return s;
        }

        /// <summary>
        /// Build the 'outgoing-target' Datamover setting from its components (stored in a DataStoreServerStringComponents structure).
        /// </summary>
        /// <param name="s">ApplicationServerStringComponents structure.</param>
        /// <returns></returns>
        private string BuildApplicationServerString()
        {
            StringBuilder builder = new StringBuilder();

            builder.Append(this.ApplicationServerProtocol);
            builder.Append("://");
            builder.Append(this.ApplicationServerHostname);
            if (this.ApplicationServerPort != -1)
            {
                builder.Append(":");
                builder.Append(ApplicationServerPort.ToString());
            }

            if (!this.ApplicationServerPath.StartsWith("/"))
            {
                builder.Append("/");
            }

            builder.Append(this.ApplicationServerPath);

            return builder.ToString();
        }

        /// <summary>
        /// Build the 'outgoing-target' Datamover setting from its components (stored in a DataStoreServerStringComponents structure).
        /// </summary>
        /// <param name="s">DataStoreServerStringComponents structure.</param>
        /// <returns></returns>
        private string BuildOutgoingTargetString()
        {
            StringBuilder builder = new StringBuilder();

            builder.Append(this.DataStoreServerUserName);
            builder.Append("@");
            builder.Append(this.DataStoreServerHostname);
            builder.Append(":");
            if (!this.DataStoreServerPathToRootDropboxFolder.StartsWith("/"))
            {
                builder.Append("/");
            }
            builder.Append(this.DataStoreServerPathToRootDropboxFolder);
            builder.Append("/incoming-");
            builder.Append(this.DataStoreServerHardwareSubClass);

            return builder.ToString();
        }
    }

    /// <summary>
    /// Structure of all components of the 'outgoing-target' string.
    /// </summary>
    struct DataStoreServerStringComponents
    {
        public string UserName;
        public string Server;
        public string DropboxRoot;
        public string HardwareClass;
        public string HardwareSubClass;
    };

    /// <summary>
    /// Structure of all components of the 'outgoing-target' string.
    /// </summary>
    struct ApplicationServerStringComponents
    {
        public string Protocol;
        public string Server;
        public int Port;
        public string Path;
    };

}
