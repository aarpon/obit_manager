using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace obit_manager_settings
{
    namespace components
    {
        public class Client
        {
            // User folder base path on the local machine
            public string UserFolder { get; set; } = @"D:\ToOpenBIS";
            
            // Local user account
            public string LocalUserAccout { get; set; } = "openbis";

            // Computer hostname
            public string ComputerHostName { get; } = Environment.MachineName;

            // Computer friendly name
            public string ComputerFriendlyName { get; set; } = string.Empty;

        }
    }
}
