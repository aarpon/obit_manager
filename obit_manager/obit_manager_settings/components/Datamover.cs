using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace obit_manager_settings
{
    namespace components
    {
        /// <summary>
        /// Datamover data class.
        /// </summary>
        public class Datamover
        {
            // Application name
            [Setting(Owner = "Datamover_JSL")]
            public string AppName { get; set; } = "Datamover";

            // Service name
            [Setting(Owner = "Datamover_JSL")]
            public string ServiceName { get; set; } = "Datamover";

            // Display name
            [Setting(Owner = "Datamover_JSL")]
            public string DisplayName { get; set; } = "Datamover";

            // Service description
            [Setting(Owner = "Datamover_JSL")]
            public string ServiceDescription { get; set; } = "Datamover as Windows Service";

            // Path to the private key to use to log in to the DataStore Server
            [Setting(Owner = "Datamover_JSL")]
            public string DataStoreServerLocalPrivateKeyPath { get; set; } = string.Empty;

            // Local user account to run Datamover as a Windows service 
            [Setting(Owner = "Datamover_JSL")]
            public string LocalUserAccout { get; set; } = "openbis";

            // Datamover incoming target
            [Setting(Owner = "Datamover")]
            public string IncomingTarget { get; set; } = @"D:/Datamover/incoming";

            // Skip accesibility test on incoming?
            [Setting(Owner = "Datamover")]
            public bool SkipAccessibilityTestOnIncoming { get; set; } = false;

            // Datamover buffer directory
            [Setting(Owner = "Datamover")]
            public string BufferDir { get; set; } = @"D:/Datamover/buffer";

            // Datamover buffer directory high watermark level
            [Setting(Owner = "Datamover")]
            public int BufferDirHighwaterMark { get; set; } = 1048576;

            // Datamover outgoing directory
            [Setting(Owner = "Datamover")]
            public string OutgoingTarget { get; set; } = string.Empty;

            // Datamover outgoing directory high watermark level
            [Setting(Owner = "Datamover")]
            public int OutgoingTargetHighwaterMark { get; set; } = 1048576;

            // Skip accesibility test on outgoing?
            [Setting(Owner = "Datamover")]
            public bool SkipAccessibilityTestOnOutgoing { get; set; } = true;

            // Relative path to the data completed script
            [Setting(Owner = "Datamover")]
            public string DataCompletedScript { get; set; } = "scripts/data_completed_script.bat";

            // Datamover manual intervention directory
            [Setting(Owner = "Datamover")]
            public string ManualInterventionDir { get; set; } = "D:/Datamover/manual_intervention";

            // Quiet period
            [Setting(Owner = "Datamover")]
            public int QuietPeriod { get; set; } = 60;

            // Check interval
            [Setting(Owner = "Datamover")]
            public int CheckInterval { get; set; } = 60;

            // (Optional) Full path tp the lastchanged executable on the DataStore Server
            [Setting(Owner = "Datamover")]
            public string OutgoingHostLastchangedExecutable { get; set; } = string.Empty;

            /// <summary>
            /// Constructor.
            /// </summary>
            public Datamover()
            {
            }
        }

    }
}
