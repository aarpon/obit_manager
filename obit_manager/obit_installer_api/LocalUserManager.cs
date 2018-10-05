using System;
using System.DirectoryServices;


namespace obit_manager_api
{
    public static class LocalUserManager
    {
        /// <summary>
        /// Check whether the current user is an administrator.</summary>
        /// <returns>
        /// True if current user is an administrator, false otherwise.</returns>
        public static bool IsAdministrator()
        {
            return (new System.Security.Principal.WindowsPrincipal(
                System.Security.Principal.WindowsIdentity.GetCurrent()))
                .IsInRole(System.Security.Principal.WindowsBuiltInRole.Administrator);
        }

        /// <summary>
        /// Create a local user with given name and password.
        /// </summary>
        /// <param name="username">User name.</param>
        /// <param name="password">User password.</param>
        /// <returns>True if the user could be created successfully, false otherwise.</returns>
        public static bool CreateUser(string username, string password,
            out string errorMessage, string description = "",
            string groupname = "Users", bool passwordNeverExpires = true)
        {
            // Initialize error message
            errorMessage = "";

            // Try adding a local user `username` to the 'Users' group
            try
            {
                // Get root directory entry
                DirectoryEntry AD = new DirectoryEntry("WinNT://" + Environment.MachineName + ",computer");

                // Create a new user
                DirectoryEntry NewUser = AD.Children.Add(username, "user");

                // Set user properties
                NewUser.Invoke("SetPassword", new object[] { password });
                NewUser.Invoke("Put", new object[] { "Description", description });
                //NewUser.Invoke("Put", new object[] { "DisplayName", displayname});

                // Commit the changes -- otherwise the flags cannot be retrieved or set.
                NewUser.CommitChanges();

                // Set password to never expire
                if (passwordNeverExpires == true)
                {
                    int NON_EXPIRE_FLAG = 0x10000;
                    int val = (int)NewUser.Properties["UserFlags"].Value;
                    NewUser.Invoke("Put", new object[] { "UserFlags", val | NON_EXPIRE_FLAG });
                }

                // Commit the changes again
                NewUser.CommitChanges();

                // Try getting the group
                DirectoryEntry grp;
                grp = AD.Children.Find(groupname, "group");
                if (grp != null)
                {
                    // Add the newly created user to the group
                    grp.Invoke("Add", new object[] { NewUser.Path.ToString() });
                }

                // Close
                NewUser.Close();
            }
            catch (Exception ex)
            {
                // Return the error message
                errorMessage = ex.Message;
                return false;
            }

            // Return success
            return true;
        }
    }
}
