using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace obit_manager_settings.components
{
    public partial class Instance
    {
        #region events

        public static event EventHandler<string> ConfigurationNameChanged;

        public static void OnConfigurationNameChanged(Instance sender, string newName)
        {
            EventHandler<string> handler = ConfigurationNameChanged;
            handler?.Invoke(sender, newName);
        }

        #endregion events
    }

    
}
