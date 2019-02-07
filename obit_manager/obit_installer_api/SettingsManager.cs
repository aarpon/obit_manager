using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace obit_manager_api
{
    public class SettingsManager
    {
        /// <summary>
        /// Settings file name.
        /// </summary>
        private static string settingsFileName = @"C:\ProgramData\obit\AnnotationTool\settings.xml";

        /// <summary>
        /// LINQ XElement object (points to the root of the settings XML tree.
        /// </summary>
        private System.Xml.Linq.XElement settings;

        /// <summary>
        /// Constructor.
        /// </summary>
        public SettingsManager()
        {
            // Initialize needed defaults.
        }

        /// <summary>
        /// Reads the settings XML file.
        /// </summary>
        /// <returns>True if the settings XML file could be read successfully, false otherwise.</returns>
        public bool Read()
        {
            try
            {
                // Load the settings XML file.
                settings = System.Xml.Linq.XElement.Load(settingsFileName);
            }
            catch (System.IO.DirectoryNotFoundException)
            {
                Console.WriteLine("The path to the settings file was not correct!");
                return false;
            }
            catch (System.IO.FileNotFoundException)
            {
                Console.WriteLine("SettingsManager file not found!");
                return false;
            }
            catch (Exception e)
            {
                Console.WriteLine("Could not open the file (" +
                    e.GetType() + "): " + e.Message);
            }

            // DO something with the file!
            return true;
        }
    }
}
