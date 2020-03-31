using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace obit_manager_settings
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
    public class SettingAttribute : Attribute
    {
        // Define Owner read-only attribute
        public virtual string Owner { get; set; }
    }
}
