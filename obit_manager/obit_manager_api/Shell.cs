
namespace obit_manager_api
{
    public class Shell
    {
        /// <summary>
        /// Run a command as the current user and returns the standard output when finished.
        /// </summary>
        /// <param name="command">Command to be executed.</param>
        /// <param name="arguments">Arguments for the command.</param>
        /// <returns>Output of the command.</returns>
        public static string Run(string command, string arguments)
        {
            // Set up the parameters of the process
            System.Diagnostics.ProcessStartInfo info = new System.Diagnostics.ProcessStartInfo(command)
            {
                UseShellExecute = false,
                RedirectStandardInput = true,
                RedirectStandardError = true,
                RedirectStandardOutput = true,
                Domain = System.Environment.MachineName,
                WorkingDirectory = @"C:\temp",
                Arguments = arguments,
            };

            // Run the process and wait for it to finish. Collect the standard output.
            string output = "";
            using (System.Diagnostics.Process proc = System.Diagnostics.Process.Start(info))
            {
                output = proc.StandardOutput.ReadToEnd();
                proc.WaitForExit();
            }

            // Return the output
            return output;
        }

        /// <summary>
        /// Run a command as the current user and returns the standard output when finished.
        /// </summary>
        /// <param name="command">Command to be executed.</param>
        /// <param name="arguments">Arguments for the command.</param>
        /// <param name="username">Name of the user that runs the command.</param>
        /// <param name="password">Password of the user that runs the command.</param>
        /// <param name="domain">Domain of the user (set to "" to fall back to current machine (i.e. local user).</param>
        /// <returns>Output of the command.</returns>
        public static string RunAs(string command, string arguments, 
            string username, System.Security.SecureString password, string domain = "")
        {
            // Do we have a domain?
            if (domain == "")
            {
                // Fall back to current machine (i.e. local user)
                domain = System.Environment.MachineName;
            }

            // Set up the parameters of the process
            System.Diagnostics.ProcessStartInfo info = new System.Diagnostics.ProcessStartInfo(command)
            {
                UseShellExecute = false,
                RedirectStandardInput = true,
                RedirectStandardError = true,
                RedirectStandardOutput = true,
                LoadUserProfile = true,
                WorkingDirectory = @"C:\temp",
                Domain = domain,
                Arguments = arguments,
                UserName = username,
                Password = password
            };

            // Run the process as the user and wait for it to finish. Collect the standard output.
            string output = "";
            using (System.Diagnostics.Process proc = System.Diagnostics.Process.Start(info))
            {
                output = proc.StandardOutput.ReadToEnd();
                proc.WaitForExit();
            }

            // Return the output
            return output;
        }
    }
}
