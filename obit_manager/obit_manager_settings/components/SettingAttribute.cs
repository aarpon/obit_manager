using System;

namespace obit_manager_settings.components
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
    public class SettingAttribute : Attribute
    {
        // Define Owner read-only attribute
        public virtual string Configuration { get; set; }

        public virtual string Component { get; set; }
    }
}
