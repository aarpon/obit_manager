using System;
using obit_manager_settings.components;
using System.Collections.Generic;
using NLog;
using NLog.Config;
using obit_manager_settings.components.io;
using static obit_manager_settings.Constants;
using System.Configuration;

namespace obit_manager_settings
{
    /// <summary>
    /// Manager of all oBIS settings.
    /// </summary>
    public class SettingsManager
    {
        // Logger
        private static Logger sLogger = LogManager.GetCurrentClassLogger();

        // Settings parsers
        private readonly ManagerSettingsParser mManagerParser;
        private readonly AnnotationToolSettingsParser mAnnotationToolParser;
        private readonly DatamoverJSLSettingsParser mDatamoverJSLParser;
        private readonly DatamoverSettingsParser mDatamoverParser;

        // List of instances
        private List<Instance> mInstances;

        public SettingsManager()
        {
            // Initialize application state
            this.ApplicationState = State.OBIT_NOT_INSTALLED;

            // Initialize list of instances
            this.mInstances = new List<Instance>();

            // Load (if possible, otherwise instantiate with default values)
            // the oBIT Manager Application Settings
            this.mManagerParser = new ManagerSettingsParser();

            // Load (if possible, otherwise instantiate with default values)
            // the AnnotationTool Settings
            this.mAnnotationToolParser = new AnnotationToolSettingsParser();

            // Load (if possible, otherwise instantiate with default values)
            // the Datamover JSL Settings
            this.mDatamoverJSLParser = new DatamoverJSLSettingsParser(
                this.mManagerParser.InstallationDir,
                this.mManagerParser.DatamoverRelativeDirList
            );

            // Load (if possible, otherwise instantiate with default values)
            // the Datamover Settings
            this.mDatamoverParser = new DatamoverSettingsParser(
                this.mManagerParser.InstallationDir,
                this.mManagerParser.DatamoverRelativeDirList
            );

            // If needed, update the relative DatamoverJSL directories
            this.mManagerParser.DatamoverRelativeDirList = this.mDatamoverJSLParser.GetRelativeDatamoverJSLDirs();

            // Populate the instances and update the state
            this.ApplicationState = this.PopulateInstances();
        }

        public State ApplicationState { get; private set; }

        public void Reload()
        {
            // @Todo Re-implement correctly!
            this.mManagerParser.Load();
            this.mAnnotationToolParser.Load();
            this.mDatamoverJSLParser.Load();
            this.mDatamoverParser.Load();

            // Populate the instances
            this.PopulateInstances();
        }

        public void Save()
        {
            // @Todo Implement!
            this.mManagerParser.Save();
            this.mAnnotationToolParser.Save();
            this.mDatamoverJSLParser.Save();
            this.mDatamoverParser.Save();
        }

        private State PopulateInstances()
        {
            // Get the list of instances from the Annotation Tool parser
            var configurations = this.mAnnotationToolParser.Configurations;

            // Inform
            foreach (KeyValuePair<string, Dictionary<string, string>> configuration in configurations)
            {
                // Inform
                sLogger.Info("Processing instance '" + configuration.Key + "'.");

                // Create a new Client from the AnnotationTool settings
                Client client = new Client(configuration.Value);

                // Create a Datamover object from the Datamover and DatamoverJSL configurations
                Datamover datamover;
                try
                {
                    datamover = new Datamover(client.DatamoverIncomingDir, this.mDatamoverJSLParser,
                        this.mDatamoverParser);
                }
                catch (ConfigurationException)
                {
                    // Inform
                    sLogger.Error("Configuration '" + configuration.Key + "' is invalid and will be skipped.");

                    // Skip this configuration.
                    continue;
                }

                // Create a Server object
                Server server;
                try
                {
                    server = new Server(client.DatamoverIncomingDir, this.mDatamoverParser);
                }
                catch (ConfigurationException e)
                {
                    // Inform
                    sLogger.Error("Configuration '" + configuration.Key + "' is invalid and will be skipped.");

                    // Skip this configuration.
                    continue;
                }

                // Create new instance
                Instance instance = new Instance(configuration.Key, client, server, datamover);

                // Add to the instance list
                this.mInstances.Add(instance);

                // Inform
                sLogger.Info("Created instance '" + configuration.Key + "'.");
            }

            // @TODO Set the proper State
            return State.OBIT_NOT_INSTALLED;
        }

        #region properties

        // openBIS Importer Toolset installation dir
        public string InstallationDir
        {
            get => this.mManagerParser.InstallationDir;
            set
            {
                if (this.mManagerParser != null)
                {
                    this.mManagerParser.InstallationDir = value;
                }
            }
        }

        #endregion properties
    }
}
