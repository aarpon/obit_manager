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
            [Setting(Owner = "AnnotationTool")]
            public string UserFolder { get; set; } = @"D:\ToOpenBIS";

            // Local user account
            [Setting(Owner = "AnnotationTool")]
            public string LocalUserAccout { get; set; } = "openbis";

            // Computer hostname
            [Setting(Owner = "AnnotationTool")]
            public string ComputerHostName { get; } = Environment.MachineName;

            // Computer friendly name
            [Setting(Owner = "AnnotationTool")]
            public string ComputerFriendlyName { get; set; } = string.Empty;

            // Whether the marker file should be created in the Datamover incoming folder
            [Setting(Owner = "AnnotationTool")]
            public bool CreateMarkerFile { get; set; } = false;
        }
    }
}
