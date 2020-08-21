using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace obit_manager_settings
{
    public class InstancesValidator
    {
        public InstancesValidator()
        {
        }

        public bool Validate()
        {
            // Rules
            //
            // More than one Client can match with the same Datamover object

            // One Datamover object can only be linked to one Server object

            // @TODO Implement!
            return true;
        }

    }
}
