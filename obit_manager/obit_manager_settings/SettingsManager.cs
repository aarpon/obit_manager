using obit_manager_settings.components;
using System.Collections.Generic;

namespace obit_manager_settings
{
    /// <summary>
    /// Manager of all oBIS settings.
    /// </summary>
    class SettingsManager
    {

        // Application settings
        private AppSettings mAppSettings;

        // List of instances
        private List<Instance> mInstances;

        public SettingsManager()
        {
            this.mAppSettings = new AppSettings();
            this.mInstances = new List<Instance>();
        }
    }
}
