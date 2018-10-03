using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace obit_manager_api
{
    class Utils
    {
        public static bool IsAdministrator()
        {
            return (new System.Security.Principal.WindowsPrincipal(System.Security.Principal.WindowsIdentity.GetCurrent()))
                      .IsInRole(System.Security.Principal.WindowsBuiltInRole.Administrator);
        }
    }
}
