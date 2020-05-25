using NLog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;


namespace obit_manager_settings
{
    namespace io
    {
        class DatamoverSettingsParser
        {
            // Logger
            private static Logger sLogger = LogManager.GetCurrentClassLogger();

            private Dictionary<string, Dictionary<string, string>> mMap;

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

                        // Log
                        sLogger.Info("Successfully parsed Datamover settings file '" + fileName + "'.");
                    }
                }

                if (this.mMap.Count == 0)
                {
                    // Log
                    sLogger.Info("No Datamover settings files found!");

                    return false;
                }

                return true;
            }

            public bool Save()
            {
                throw new NotImplementedException("Implement me!");
            }
        }
    }
}
