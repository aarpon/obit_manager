using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace obit_manager_api
{
    public class Instance
    {
        // Minimum accepted Java Runtime version
        private int _MinJavaMajorVersion = 8;

        // Public members
        public enum ATAcqType { FLOW_CYTOMETRY, MICROSCOPY };

        // Public properties

        // Application server host name
        public string ApplicationServerHostname { get; set; } = "localhost";

        // Application server port number (it can be null)
        public int? ApplicationServerPort { get; set; } = null;

        // Data store server host name
        public string DataStoreServerHostname { get; set; } = "localhost";

        // Data store server user
        public string DataStoreServerUser { get; set; } = "openbis";

        // Data store server user
        public string DataStoreServerLastChangedBinPath { get; set; } = string.Empty;

        // Dropbox path on the DataStore server
        public string DataStoreServerDropboxPath { get; set; } = string.Empty;

        // Local Windows user that runs Datamover as a Windows service
        public string LocalUser { get; set; } = "openbis";

        // openBIS Importer Toolset installation dir
        public string InstallationDir { get; set; } = "C:\\oBIT";

        // Annotation Tool acquisition instrument type (default is FLOW_CYTOMETRY)
        public ATAcqType AnnotationToolAcqType { get; set; } = ATAcqType.FLOW_CYTOMETRY;

        // Computer hostname
        public string ComputerHostName { get; } = Environment.MachineName;

        // Computer friendly name
        public string ComputerFriendlyName { get; set; } = string.Empty;

        // Is the platform 64 bits? Otherwise, it is 32 bits
        public bool IsPlatform64Bits { get; set; } = true;

        // User folder base path on the local machine
        public string UserFolder { get; set; } = "D:\\toOpenBIS";

        // Datamover folder
        public string DatamoverDataFolder { get; set; } = "D:\\Datamover";

        // Name of the Datamover service
        public string DatamoverServiceName { get; set; } = "Datamover";

        // Use already installed Java Runtime?
        public bool UseExistingJavaRuntime { get; set; } = false;

        // Path to local Java Runtime folder
        public string JavaRuntimePath { get; set; } = "C:\\oBIT\\jre";

        // Accept self-signed certificates?
        public bool AcceptSelfSignedCerts { get; set; } = false;
    }
}
