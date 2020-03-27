using obit_manager_settings.components;
using System.Collections.Generic;

namespace obit_manager_settings
{
    /// <summary>
    /// Manager of all oBIS settings.
    /// </summary>
    class SettingsManager
    {
        // List of instances
        private List<Instance> mInstances = new List<Instance>();

        // openBIS Importer Toolset installation dir
        public string InstallationDir { get; set; } = @"C:\oBIT";

        // Minimum accepted Java Runtime version
        public int MinJavaMajorVersion { get; } = 8;

        // Is the platform 64 bits? Otherwise, it is 32 bits
        public bool IsPlatform64Bits { get; set; } = true;

        // Use already installed Java Runtime?
        public bool UseExistingJavaRuntime { get; set; } = false;

        // Path to local Java Runtime folder
        public string JavaRuntimePath { get; set; } = @"C:\oBIT\jre";

    }
}
