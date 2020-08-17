using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using NLog;

namespace obit_manager_settings.components
{
    public partial class Client
    {
        /// <summary>
        /// Logger
        /// </summary>
        private static Logger sLogger = LogManager.GetCurrentClassLogger();

        // Configuration name 
        private string mConfiguratioName = "Default";

        // Configuration name
        [Setting(Configuration = "AnnotationTool", Component = "Client")]
        public string ConfigurationName
        {
            get
            {
                return this.mConfiguratioName;
            }
            set
            {
                // Set the new configuration name
                this.mConfiguratioName = value;

                // Raise the event
                Client.OnConfigurationNameChanged(this, value);
            }
        }

        // OpenBIS AS URL
        [Setting(Configuration = "AnnotationTool", Component = "Server")]
        public string OpenBISURL { get; set; } = "https://localhost:8443/openbis";

        // Datamover incoming-dir
        [Setting(Configuration = "AnnotationTool", Component = "Datamover")]
        public string DatamoverIncomingDir { get; } = @"D:\Datamover\incoming";

        // Acquisition station type
        [Setting(Configuration = "AnnotationTool", Component = "Client")]
        public string AcquisitionStation { get; set; } = "Microscopy";

        // Client human-friendly name
        [Setting(Configuration = "AnnotationTool", Component = "Client")]
        public string HumanFriendlyHostName { get; set; } = Environment.MachineName;

        // Accept self-signed certificates
        [Setting(Configuration = "AnnotationTool", Component = "Server")]
        public string AcceptSelfSignedCertificates { get; set; } = "no";

        // User folder base path on the local machine
        [Setting(Configuration = "AnnotationTool", Component = "Client")]
        public string UserDataDir { get; set; } = @"D:\ToOpenBIS";

        // Create marker file
        [Setting(Configuration = "AnnotationTool", Component = "Client")]
        public string CreateMarkerFile { get; set; } = "no";

        /// <summary>
        /// Default constructor
        /// </summary>
        public Client()
        {
            // Keep default values.
        }

        /// <summary>
        /// Constructor that takes a configuration dictionary to update its values.
        /// </summary>
        /// <param name="name">Name of the configuration.</param>
        /// <param name="conf">Configuration dictionary.</param>
        public Client(Dictionary<string, string> conf)
        {
            // Set the values
            this.ConfigurationName = conf["ConfigurationName"];
            this.OpenBISURL = conf["OpenBISURL"];
            this.DatamoverIncomingDir = conf["DatamoverIncomingDir"];
            this.AcquisitionStation = conf["AcquisitionStation"];
            this.HumanFriendlyHostName = conf["HumanFriendlyHostName"];
            this.AcceptSelfSignedCertificates = conf["AcceptSelfSignedCertificates"];
            this.UserDataDir = conf["UserDataDir"];
            this.CreateMarkerFile = conf["CreateMarkerFile"];
        }

        /// <summary>
        /// Copy constructor.
        /// </summary>
        /// <param name="other">Client object to be copied.</param>
        public Client(Client other)
        {
            // Set the values
            this.ConfigurationName = string.Copy(other.ConfigurationName);
            this.OpenBISURL = string.Copy(other.OpenBISURL);
            this.DatamoverIncomingDir = string.Copy(other.DatamoverIncomingDir);
            this.AcquisitionStation = string.Copy(other.AcquisitionStation);
            this.HumanFriendlyHostName = string.Copy(other.HumanFriendlyHostName);
            this.AcceptSelfSignedCertificates = other.AcceptSelfSignedCertificates;
            this.UserDataDir = string.Copy(other.UserDataDir);
            this.CreateMarkerFile = other.CreateMarkerFile;
        }

        public Dictionary<string, string> ToConfiguration()
        {
            // Initialize
            var conf = new Dictionary<string, string>();
            
            // Set the values
            conf["ConfigurationName"] = this.ConfigurationName;
            conf["OpenBISURL"] = this.OpenBISURL;
            conf["DatamoverIncomingDir"] = this.DatamoverIncomingDir;
            conf["AcquisitionStation"] = this.AcquisitionStation;
            conf["HumanFriendlyHostName"] = this.HumanFriendlyHostName;
            conf["AcceptSelfSignedCertificates"] = this.AcceptSelfSignedCertificates;
            conf["UserDataDir"] = this.UserDataDir;
            conf["CreateMarkerFile"] = this.CreateMarkerFile;

            return conf;
        }

    }
}
