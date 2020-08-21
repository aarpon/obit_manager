using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using obit_manager_settings.components;

namespace obit_manager_settings
{
    public class InstancesConsolidator
    {
        public InstancesConsolidator()
        {
        }

        public bool Consolidate()
        {
            // Get the reference to the SettingsManager
            SettingsManager settingsManager = SettingsManager.Get();

            // Get references to the relevant objects
            Instance instance = settingsManager.SelectedInstance;
            Client client = settingsManager.GetClientFromSelectedInstance();
            Datamover datamover = settingsManager.GetDatamoverFromSelectedInstance();
            Server server = settingsManager.GetServerFromSelectedInstance();

            // Mirror the Instance name to the Client ConfigurationName property
            client.ConfigurationName = instance.Name;

            // Store the IncomingTarget property from the Datamover object in the
            // DatamoveIncomingDir property of the client.
            client.DatamoverIncomingDir = datamover.IncomingTarget;

            // @TODO Anything else?

            return true;
        }
    }
}
