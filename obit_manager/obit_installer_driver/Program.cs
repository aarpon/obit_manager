using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using obit_manager_api;

namespace obit_manager_driver
{
    class Program
    {
        static void Main(string[] args)
        {
            // Current user
            string username = System.Security.Principal.WindowsIdentity.GetCurrent().Name;

            //FileSystem.GrantFullAccessToEveryone(@"E:\eula.1028.txt");
            string fullPath = @"E:\temp";

            // Remove inherited permissions
            FileSystem.RemoveInheritedPermissionsForFolder(fullPath);

            // Remove all users' permissions
            FileSystem.removePermissionsForAllUsersButOne(fullPath, username);

            // Grant full access permissions to Everyone
            FileSystem.GrantFullAccessToEveryone(fullPath);

            // Grant full access permission to current user
            FileSystem.GrantFullAccessToUser(fullPath, username);

            // Set ownership to current user
            FileSystem.SetOwnership(fullPath, username);
        }
    }
}
