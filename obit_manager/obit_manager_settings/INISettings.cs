using IniParser;
using IniParser.Model;
using System;

namespace obit_manager_settings
{
    public class INISettings
    {
        ///
        /// 
        /// 
        private readonly static string settingsDirectory = 
            Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) +
            @"\obit\obit_manager";

        /// <summary>
        /// Settings file name.
        /// </summary>
        private readonly static string settingsFileName = settingsDirectory + @"\obit_manager_settings.ini";

        /// <summary>
        /// File parser.
        /// </summary>
        private FileIniDataParser parser;

        /// <summary>
        /// Settings.
        /// </summary>
        IniData data;

        /// <summary>
        /// Settings version number.
        /// </summary>
        public readonly int version = 1;

        /// <summary>
        /// Constructor.
        /// </summary>
        public INISettings()
        {
            // Initialize the parser
            parser = new FileIniDataParser();

            // Load or create the settings file
            Load();
        }

        /// <summary>
        /// Save current settings to disk.
        /// </summary>
        public void Save()
        {
            // Make sure the path is valid
            System.IO.Directory.CreateDirectory(settingsDirectory);
            parser.WriteFile(settingsFileName, data);

        }
        /// <summary>
        /// Get the settings with given name.
        /// </summary>
        /// <param name="key">Settings name.</param>
        /// <returns>Settings with given name.</returns>
        public string Get(String section, String key)
        {
            string value;
            try
            {
                value = data[section][key];
            }
            catch (Exception)
            {
                value = "";
            }
            return value;
        }

        /// <summary>
        /// Sets the value for tje property with given name.
        /// </summary>
        /// <param name="key">Property name.</param>
        /// <param name="value">Property value.</param>
        public void Set(String section, String key, string value)
        {
            
        }

        /// <summary>
        /// Loads settings from disk
        /// </summary>
        private void Load()
        {
            // Does the settings file exist?
            if (System.IO.File.Exists(settingsFileName))
            {
                // Load the file
                data = parser.ReadFile(settingsFileName);

                // Update if needed
                UpdateIfNeeded();
            }
            else
            {
                // Create a default file
                CreateDefault();

                // Save it
                Save();
            }
            
        }

        /// <summary>
        /// Create default settings.
        /// </summary>
        private void CreateDefault()
        {
            // Create a new data object
            data = new IniData();

            // Versions
            data.Sections.AddSection("Versions");
            data["Versions"].AddKey("Settings", version.ToString());

            // Path
            data.Sections.AddSection("Paths");
            data["Paths"].AddKey("InstallationDir", @"C:\oBIT");
        }

        /// <summary>
        /// Check if the loaded settings file is up to date; otherwise
        /// update it.
        /// </summary>
        /// <returns>True if the file needed an update; false otherwise.</returns>
        private bool UpdateIfNeeded()
        {
            return false;
        }
    }
}
