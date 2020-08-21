using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using obit_manager_settings.components;

namespace obit_manager_gui
{
    public partial class obit_manager
    {
        #region event_handlers

        private void RegisterEventHandlers()
        {
            Instance.ConfigurationNameChanged += ConfigurationNameChangeHandler;
        }

        // Event handlers
        void ConfigurationNameChangeHandler(object sender, string e)
        {
            // Cast the sender
            Instance instance = (Instance) sender;

            // Refresh the Instance Configurator
            this.mInstanceConfigurator.Refresh();
        }

        #endregion event_handlers
    }
}
