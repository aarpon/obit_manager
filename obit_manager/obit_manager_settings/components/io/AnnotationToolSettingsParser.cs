using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using NLog;

namespace obit_manager_settings.components.io
{
    public class AnnotationToolSettingsParser
    {
        // Logger
        private static Logger sLogger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// List of configurations
        /// </summary>
        private Dictionary<string, Dictionary<string, string>> mConfigurations;

        /// <summary>
        /// Annotation Tool settings version.
        /// </summary>
        private static readonly int sAnnotationToolSettingsVersion = 9;

        /// <summary>
        /// Settings directory.
        /// </summary>
        private static readonly string sSettingsDirectory =
            Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) +
            @"\obit\AnnotationTool";

        /// <summary>
        /// Settings file name.
        /// </summary>
        private static readonly string sSettingsFileName = sSettingsDirectory + @"\settings.xml";

        /// <summary>
        /// XML document.
        /// </summary>
        XmlDocument doc;

        /// <summary>
        /// Property Configurations
        /// </summary>
        public Dictionary<string, Dictionary<string, string>> Configurations
        {
            get => this.mConfigurations;
        }

        public AnnotationToolSettingsParser()
        {
            // Instantiate XmlDocument parser
            this.doc = new XmlDocument();

            // Load if possible
            this.Load();
        }

        public bool Load()
        {
            if (!File.Exists(AnnotationToolSettingsParser.sSettingsFileName))
            {
                // Log
                sLogger.Info("Annotation Tool configuration file " +
                    AnnotationToolSettingsParser.sSettingsFileName +
                    " does not exist.");

                return false;
            }

            // Load the file
            this.doc.Load(AnnotationToolSettingsParser.sSettingsFileName);

            // Get the root note
            XmlElement rootNode = this.doc.DocumentElement;

            if (rootNode == null)
            {
                // Log
                sLogger.Error("Annotation Tool configuration file " +
                              AnnotationToolSettingsParser.sSettingsFileName +
                              " does not seem to be valid.");

                return false;
            }

            // Check the version
            if (rootNode.Name != "AnnotationTool_App_Settings")
            {
                // Log
                sLogger.Error("Annotation Tool configuration file " +
                    AnnotationToolSettingsParser.sSettingsFileName +
                    " does not seem to be valid.");

                return false;
            }

            if (!Int32.TryParse(rootNode.GetAttribute("version"), out int version))
            {

                // Log
                sLogger.Error("Annotation Tool configuration file " +
                    AnnotationToolSettingsParser.sSettingsFileName +
                    " does not seem to be valid.");

                return false;
            }

            if (version < AnnotationToolSettingsParser.sAnnotationToolSettingsVersion)
            {
                // Log
                sLogger.Warn("Implement upgrade of Annotation Tool configuration files!");

                // @TODO Implement update!
                return false;
            }

            // Reset (or instantiate) list of configurations
            this.mConfigurations = new Dictionary<string, Dictionary<string, string>>();

            // Read all configurations
            foreach (XmlNode node in rootNode.ChildNodes)
            {
                Dictionary<string, string> conf = new Dictionary<string, string>();
                conf["CreateMarkerFile"] = node.Attributes["CreateMarkerFile"].Value;
                conf["OpenBISURL"] = node.Attributes["OpenBISURL"].Value;
                conf["AcceptSelfSignedCertificates"] = node.Attributes["AcceptSelfSignedCertificates"].Value;
                conf["HumanFriendlyHostName"] = node.Attributes["HumanFriendlyHostName"].Value;
                conf["AcquisitionStation"] = node.Attributes["AcquisitionStation"].Value;
                conf["DatamoverIncomingDir"] = node.Attributes["DatamoverIncomingDir"].Value;
                conf["UserDataDir"] = node.Attributes["UserDataDir"].Value;
                conf["ConfigurationName"] = node.Attributes["ConfigurationName"].Value;
                this.mConfigurations[conf["ConfigurationName"]] = conf;

                // Log
                sLogger.Info("Processed Annotation Tool configuration '" + conf["ConfigurationName"] + "'.");
            }

            // Log
            sLogger.Info("Successfully parsed Annotation Tool settings file '" + AnnotationToolSettingsParser.sSettingsFileName + "'.");

            return true;
        }

        public bool Save()
        {
            throw new NotImplementedException("Implement me!");
        }

        /// <summary>
        /// Get the settings with given name.
        /// </summary>
        /// <param name="confName">Name of the configuration to query.</param>
        /// <param name="key">Settings name.</param>
        /// <returns>Settings with given name.</returns>
        private string Get(String confName, String settingsName)
        {
            // Does the requested configuration exist?
            if (!this.Configurations.ContainsKey(confName))
            {
                // Inform
                sLogger.Error("No configuration found with name " + confName);
                return "";
            }

            // Get the requested configuration
            var conf = this.Configurations[confName];

            string value;
            try
            {
                value = conf[settingsName];
            }
            catch (Exception)
            {
                // Set to empty string
                value = "";

                // Log
                sLogger.Error("Could not retrieve configuration setting '" + settingsName + "' from configuration '" + confName + "'.");

            }
            return value;
        }

        /// <summary>
        /// Sets the value for the property with given name.
        /// </summary>
        /// <param name="confName">Name of the configuration to query.</param>
        /// <param name="settingsName">Property name.</param>
        /// <param name="value">Property value.</param>
        private void Set(String confName, String settingsName, string value)
        {
            // Does the requested configuration exist?
            if (!this.Configurations.ContainsKey(confName))
            {
                // Inform
                sLogger.Error("No configuration found with name " + confName);
            }

            // Get the requested configuration
            var conf = this.Configurations[confName];

            // Does the requested setting exist?
            if (!conf.ContainsKey(settingsName))
            {
                // Inform
                sLogger.Error("No setting found with name " + settingsName);
            }

            // Store it
            conf[settingsName] = value;

            // Log
            sLogger.Info("Added/updated setting '" + settingsName + "' with value '" +
                         value + "' to configuration '" + confName + "'.");
        }

    }
}
