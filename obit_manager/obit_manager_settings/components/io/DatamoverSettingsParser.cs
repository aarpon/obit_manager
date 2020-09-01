using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using NLog;
using obit_manager_api.core;

namespace obit_manager_settings.components.io
{
    public class DatamoverSettingsParser
    {
        // Logger
        private static Logger sLogger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Dictionary of configurations.
        /// </summary>
        private Dictionary<string, Dictionary<string, string>> mMap;

        /// <summary>
        /// Return configuration keys.
        /// </summary>
        public List<string> Configurations
        {
            get
            {
                List<string> names = new List<string>();
                if (this.mMap != null)
                {
                    foreach (string key in this.mMap.Keys)
                    {
                        names.Add(key);
                    }
                }
                return names;
            }
        }

        public DatamoverSettingsParser(string installationDir, string[] relativeDatamoverJSLdirs)
        {
            // Clear all configurations
            this.mMap = new Dictionary<string, Dictionary<string, string>>();

            for (int i = 0; i < relativeDatamoverJSLdirs.Length; i++)
            {
                // Store the datamoverJSL parent directory as key
                string path = Path.Combine(installationDir, relativeDatamoverJSLdirs[i]);

                // Store the key with no valid value yet
                this.mMap[path] = null;
            }

            this.Load();
        }

        public void Load()
        {
            // Keep track of which expected Datamover/DatamoverJSL does 
            // not have a valid settings file and needs default values
            Dictionary<string, bool> needDefaults = new Dictionary<string, bool>();

            // To speed things up keep track the number of configuration that
            // need default values.
            int numOfConfThatNeedDefaults = 0;

            // Since we will update the dictionary, we cannot iterate 
            // directly over it.
            List<string> keys = new List<string>(this.mMap.Keys);

            // Process all file names
            foreach (string key in keys)
            {
                // Build the file name
                string fileName = Path.Combine(key, @"datamover\etc\service.properties");

                // Does the settings file exist?
                if (File.Exists(fileName))
                {
                    Dictionary<string, string> map = new Dictionary<string, string>();

                    // Read all lines
                    string[] lines;

                    try
                    {
                        lines = File.ReadAllLines(fileName);
                    }
                    catch (Exception e)
                    {
                        // Log
                        sLogger.Error("Could not read Datamover settings file '" + fileName + "'.");

                        // Mark as incomplete: need for default values.
                        needDefaults[key] = true;

                        // Update the counter
                        numOfConfThatNeedDefaults++;

                        continue;
                    }

                    // Fill the map
                    foreach (string line in lines)
                    {
                        string[] parts = line.Split('=');
                        if (parts.Length != 2)
                        {
                            continue;
                        }

                        string l_key = parts[0].Trim();
                        string l_value = parts[1].Trim();

                        map.Add(l_key, l_value);
                    }

                    // Append to the list of maps
                    this.mMap[key] = map;

                    // Mark as fine
                    needDefaults[key] = false;

                    // Inform
                    sLogger.Info("Successfully parsed Datamover settings file '" + fileName + "'.");
                }
                else
                {
                    // Mark as incomplete: need for default values.
                    needDefaults[key] = true;

                    // Update the counter
                    numOfConfThatNeedDefaults++;

                    // Inform
                    sLogger.Warn("Expected Datamover settings file '" + fileName + "' was not found.");
                }
            }

            if (numOfConfThatNeedDefaults > 0)
            {
                // Create defaults (but do not save them yet)
                this.CreateDefaults(needDefaults);
            }
        }

        public bool Save()
        {
            throw new NotImplementedException("Implement me!");
        }

        /// <summary>
        /// Get the settings with given name.
        /// </summary>
        /// <param name="confName">Configuration name.</param>
        /// <param name="key">Settings name.</param>
        /// <returns>Settings with given name.</returns>
        public string Get(string confName, string key)
        {
            string value;
            try
            {
                value = this.mMap[confName][key];
            }
            catch (Exception)
            {
                // Set to empty string
                value = "";

                // Log
                sLogger.Error("Could not retrieve setting '" + key +
                              "' from configuration '" + confName + "'.");
            }

            return value;
        }

        /// <summary>
        /// Sets the value for the property with given name.
        /// </summary>
        /// <param name="confName">Configuration name.</param>
        /// <param name="key">Property name.</param>
        /// <param name="value">Property value.</param>
        public void Set(string confName, string key, string value)
        {
            try
            {
                this.mMap[confName][key] = value;
            }
            catch (Exception)
            {
                // Set to empty string
                value = "";

                // Log
                sLogger.Error("Could not set setting '" + key +
                              "' with value '" + value + "' to configuration '" +
                              confName + "'.");
            }

            // Log
            sLogger.Info("Added setting '" + key + "' with value '" +
                         value + "' to configuration '" + confName + "'.");
        }

        /// <summary>
        /// Create default settings.
        /// </summary>
        private void CreateDefaults(Dictionary<string, bool> needDefaults)
        {
            int n = 0;

            foreach (KeyValuePair<string, bool> entry in needDefaults)
            {
                if (entry.Value == false)
                {
                    // This configuration does not require defaults
                    continue;
                }


                // Extract installation dir and Datamover JSL subfolder from key
                String normalizedPath = FileSystem.NormalizePath(entry.Key);
                int index = normalizedPath.LastIndexOf("\\");
                String relativeFolder;
                if (index == -1)
                {
                    // Fall back
                    relativeFolder = "Datamover";
                }
                else
                {
                    relativeFolder = normalizedPath.Substring(index + 1);

                    int index_datamover = relativeFolder.ToLower().LastIndexOf("datamover");
                    if (index_datamover == -1)
                    {
                        if (n > 0)
                        {
                            relativeFolder = "Datamover_" + n;
                        }
                        else
                        {
                            relativeFolder = "Datamover";
                        }
                    }
                    else
                    {
                        relativeFolder = relativeFolder.Substring(index_datamover);
                        relativeFolder = relativeFolder.First().ToString().ToUpper() + relativeFolder.Substring(1);
                    }
                }

                // Remove jsl from serviceName
                relativeFolder = Regex.Replace(relativeFolder, "_jsl", "", RegexOptions.IgnoreCase);

                // Update the counter
                n++;

                // Create default configuration
                Dictionary<string, string> map = new Dictionary<string, string>();
                this.mMap[entry.Key] = map;

                // Set defaults
                this.Set(entry.Key, "incoming-target", 
                    FileSystem.ChangeBackwardToForwardSlashesInPath(Path.Combine("D:\\", relativeFolder, "incoming"))
                );
                this.Set(entry.Key, "skip-accessibility-test-on-incoming", "false");
                this.Set(entry.Key, "buffer-dir",
                    FileSystem.ChangeBackwardToForwardSlashesInPath(Path.Combine("D:\\", relativeFolder, "buffer"))
                );
                this.Set(entry.Key, "buffer-dir-highwater-mark", "1048576");
                this.Set(entry.Key, "outgoing-target", "openbis@bs-openbis05.ethz.ch:/links/groups/scu/openbis/data/incoming-microscopy");
                this.Set(entry.Key, "outgoing-target-highwater-mark", "1048576");
                this.Set(entry.Key, "skip-accessibility-test-on-outgoing", "true");
                this.Set(entry.Key, "data-completed-script", "scripts/ata_completed_script.bat");
                this.Set(entry.Key, "manual-intervention-dir",
                    FileSystem.ChangeBackwardToForwardSlashesInPath(Path.Combine("D:\\", relativeFolder, "manual_intervention"))
                );
                this.Set(entry.Key, "quiet-period", "60");
                this.Set(entry.Key, "check-interval", "60");
                this.Set(entry.Key, "outgoing-host-lastchanged-executable", "/local0/openbis/openbis/bin/lastchanged");
            }
        }
    }
}
