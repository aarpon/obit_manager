using obit_manager_settings.components;
using obit_manager_settings.io;
using System.Collections.Generic;
using System.IO;

namespace obit_manager_settings
{
    /// <summary>
    /// Manager of all oBIS settings.
    /// </summary>
    public class SettingsManager
    {
        // Settings parsers
        private ManagerSettingsParser mManagerParser;
        private AnnotationToolSettingsParser mAnnotationToolParser;
        private DatamoverJSLSettingsParser mDatamoverJSLParser;
        private DatamoverSettingsParser mDatamoverParser;

        // List of instances
        private List<Instance> mInstances;

        public SettingsManager()
        {
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

            // Populate the instances
            this.PopulateInstances();
        }

        public void Reload()
        {
            // @Todo Implement!
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

        private void PopulateInstances()
        {

        }

        #region properties

        // openBIS Importer Toolset installation dir
        public string InstallationDir
        {
            get
            {
                return this.mManagerParser.InstallationDir;
            }
            set
            {
                this.mManagerParser.InstallationDir = value;
            }
        }

        #endregion properties

    }
}
