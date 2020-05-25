using IniParser;
using IniParser.Model;
using NLog;
using System;
using System.Collections.Generic;
using System.IO;

namespace obit_manager_settings
{
    namespace io
    {
        class DatamoverJSLSettingsParser
        {
            // Logger
            private static Logger sLogger = LogManager.GetCurrentClassLogger();

            /// <summary>
            /// File parser.
            /// </summary>
            private FileIniDataParser mParser;

            private Dictionary<string, IniData> mMap;

            public DatamoverJSLSettingsParser(string installationDir, string[] relativeDatamoverJSLdirs)
            {
                // Initialize the mParser
                this.mParser = new FileIniDataParser();

                // Clear all configurations
                this.mMap = new Dictionary<string, IniData>();

                for (int i = 0; i < relativeDatamoverJSLdirs.Length; i++)
                {
                    // Store the datamoverJSL parent directory as key
                    string path = Path.Combine(installationDir, relativeDatamoverJSLdirs[i]);

                    // Store the key with no valid value yet
                    this.mMap[path] = null;
                }

                this.Load();
            }

            /// <summary>
            /// Loads settings from disk
            /// </summary>
            public void Load()
            {
                // Since we will update the dictionary, we cannot iterate 
                // directly over it.
                List<string> keys = new List<string>(this.mMap.Keys);

                // Process all file names
                foreach (string key in keys)
                {
                    // Build the file name
                    string fileName = Path.Combine(key, @"jsl_static.ini");

                    // Does the settings file exist?
                    if (File.Exists(fileName))
                    {
                        // Load the file and update the dictionary
                        IniData data = mParser.ReadFile(fileName);

                        // Update the dictionary
                        this.mMap[key] = data;

                        // Log
                        sLogger.Info("Processed DatamoverJSL settings file '" + fileName + "'.");
                    }
                }

                if (this.mMap.Count == 0)
                {
                    // Log
                    sLogger.Info("No Datamover JSL settings files found!");
                }
            }

            /// <summary>
            /// Get the settings with given name.
            /// </summary>
            /// <param name="key">Settings name.</param>
            /// <returns>Settings with given name.</returns>
            private string Get(IniData data, string section, string key)
            {
                string value;
                try
                {
                    value = data[section][key];
                }
                catch (Exception)
                {
                    // Set to empty string
                    value = "";

                    // Log
                    sLogger.Error("Could not retrieve configuration setting '" + key +
                        "' from section '" + section + "'.");
                }

                return value;
            }

            /// <summary>
            /// Sets the value for the property with given name.
            /// </summary>
            /// <param name="key">Property name.</param>
            /// <param name="value">Property value.</param>
            private void Set(IniData data, string section, string key, string value)
            {
                KeyData keyData = new KeyData(key);
                keyData.Value = value;
                data[section].SetKeyData(keyData);

                // Log
                sLogger.Info("Added configuration setting '" + key + "' with value '" +
                    value + "' to section '" + section + "'.");
            }


            /// <summary>
            /// Save all settings to disk.
            /// </summary>
            public void Save()
            {
                // Process all file names
                foreach (KeyValuePair<string, IniData> entry in this.mMap)
                {
                    // Build the file name
                    string fileName = Path.Combine(entry.Key, @"jsl_static.ini");

                    // Save the settings to file
                    this.Save(fileName, entry.Value);
                }
            }

            /// <summary>
            /// Save current settings to disk.
            /// </summary>
            public void Save(string settingsFileName, IniData data)
            {
                // Write settings to file
                mParser.WriteFile(settingsFileName, data);

                // Log
                sLogger.Info("Written DatamoverJSL configuration file to '" + settingsFileName + "'.");
            }
        }
    }
}
