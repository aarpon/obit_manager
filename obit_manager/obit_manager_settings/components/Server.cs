﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace obit_manager_settings
{
    namespace components
    {
        /// <summary>
        /// Server data class.
        /// </summary>
        public class Server
        {
            // Application Server host name
            public string ApplicationServerHostname { get; set; } = string.Empty;
            
            // Application Server port number (it can be null)
            public int? ApplicationServerPort { get; set; } = null;

            // Does the Application Server accept self-signed certificates?
            public bool ApplicationServerAcceptSelfSignedCert { get; set; } = false;

            // DataStore Server host name
            public string DataStoreServerHostname { get; set; } = "localhost";

            // Unix account on the DataStore Server
            public string DataStoreServerUserName { get; set; } = "openbis";

            // Full path to the dropbox root folder on the DataStore Server
            public string DataStoreServerPathToRootDropboxFolder { get; set; } = "/home/openbis/data";

            // (Optional) Full path to the lastchanged executable on the DataStore Server
            public string DataStoreServerPathToLastChangedExecutable { get; set; } = string.Empty;

            /// <summary>
            /// Constructor.
            /// </summary>
            public Server()
            {

            }
        }
    }
}