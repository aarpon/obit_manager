using System;
using System.Diagnostics;
using System.DirectoryServices;
using System.IO;
using System.Runtime.InteropServices;

namespace obit_manager_api
{
    public static class LocalUserManager
    {
        // Import DeleteProfile from userenv.dll
        [DllImport("userenv.dll", CharSet = CharSet.Unicode, ExactSpelling = false, SetLastError = true)]
        public static extern bool DeleteProfile(string sidString, string profilePath, string computerName);

        /// <summary>
        /// Check whether the current user is an administrator.
        /// </summary>
        /// <returns>True if current user is an administrator, false otherwise.
        ///     </returns>
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
        /// <returns>True if the user could be created successfully, false
        ///     otherwise.</returns>
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

        /// <summary>
        /// Check if a local user with given name exists.
        /// </summary>
        /// <param name="username">User name.</param>
        /// <returns>True if the user exists on the local machine, false
        ///     otherwise.</returns>
        public static bool UserExists(string username)
        {
            using (var ctx = new System.DirectoryServices.AccountManagement.PrincipalContext(
                System.DirectoryServices.AccountManagement.ContextType.Machine))
            {
                using (var up = System.DirectoryServices.AccountManagement.UserPrincipal.FindByIdentity(
                    ctx,
                    System.DirectoryServices.AccountManagement.IdentityType.SamAccountName, username))
                {
                    return (up != null);
                }
            }
        }

        /// <summary>
        /// Delete a local user with given name.
        /// </summary>
        /// It also deletes the local user folder and the profile in the
        /// registry.
        /// <param name="username">User name.</param>
        /// <returns>True if the user could be deleted successfully, false
        ///     otherwise.</returns>
        public static bool DeleteUser(string username)
        {
            using (var ctx = new System.DirectoryServices.AccountManagement.PrincipalContext(
                System.DirectoryServices.AccountManagement.ContextType.Machine))
            {
                using (var up = System.DirectoryServices.AccountManagement.UserPrincipal.FindByIdentity(
                    ctx, 
                    System.DirectoryServices.AccountManagement.IdentityType.SamAccountName, username))
                {
                    if (up != null)
                    {
                        // User SID
                        string upSid = up.Sid.ToString();

                        // Delete the user
                        up.Delete();

                        // Delete the info from the registry
                        DeleteProfile(upSid, null, null);

                        // Done
                        return true;
                    }

                    // User not found
                    return false;
                }
            }
        }
    }
}
