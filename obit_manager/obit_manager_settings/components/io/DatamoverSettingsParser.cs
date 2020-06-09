using System;
using System.Collections.Generic;
using System.IO;
using NLog;

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

        public bool Load()
        {
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

                    // Inform
                    sLogger.Info("Successfully parsed Datamover settings file '" + fileName + "'.");
                }
                else
                {
                    // Inform
                    sLogger.Warn("Expected Datamover settings file '" + fileName + "' was not found.");
                }
            }

            if (this.mMap.Count == 0)
            {
                // Log
                sLogger.Info("No Datamover settings files found!");

                // Create default configuration
                this.CreateDefault();

                return false;
            }

            return true;
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
        private void CreateDefault()
        {
            // Clear all configurations
            this.mMap = new Dictionary<string, Dictionary<string, string>>();

            // Create default configuration
            Dictionary<string, string> map = new Dictionary<string, string>();
            this.mMap["obit_datamoder_jsl"] = map;

            // Set defaults
            this.Set("obit_datamover_jsl", "incoming-target", "D:/Datamover/incoming");
            this.Set("obit_datamover_jsl", "skip-accessibility-test-on-incoming", "false");
            this.Set("obit_datamover_jsl", "buffer-dir", "D:/Datamover/buffer");
            this.Set("obit_datamover_jsl", "buffer-dir-highwater-mark", "1048576");
            this.Set("obit_datamover_jsl", "outgoing-target", "openbis@bs-openbis05.ethz.ch:/links/groups/scu/openbis/data/incoming-microscopy");
            this.Set("obit_datamover_jsl", "outgoing-target-highwater-mark", "1048576");
            this.Set("obit_datamover_jsl", "skip-accessibility-test-on-outgoing", "true");
            this.Set("obit_datamover_jsl", "data-completed-script", "scripts/ata_completed_script.bat");
            this.Set("obit_datamover_jsl", "manual-intervention-dir", "D:/Datamover/ manual_intervention");
            this.Set("obit_datamover_jsl", "quiet-period", "60");
            this.Set("obit_datamover_jsl", "check-interval", "60");
            this.Set("obit_datamover_jsl", "outgoing-host-lastchanged-executable", "/local0/openbis/openbis/bin/lastchanged");
        }
    }
}
