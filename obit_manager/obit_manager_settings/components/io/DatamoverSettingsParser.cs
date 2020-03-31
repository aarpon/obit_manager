using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace obit_manager_settings
{
    namespace io
    {
        class DatamoverSettingsParser
        {
            private string mSettingsFileName;
            private Dictionary<string, string> mMap;

            public DatamoverSettingsParser(string datamoverJSLdir)
            {
                this.mSettingsFileName = Path.Combine(datamoverJSLdir, @"datamover\etc\service.properties");

                this.mMap = new Dictionary<string, string>();

                Load();
            }

            public bool Load()
            {
                // Read all lines
                string[] lines;
                
                try
                {
                    lines = File.ReadAllLines(this.mSettingsFileName);
                }
                catch (Exception e)
                {
                    return false;
                }
                
                // Fill the map
                foreach(string line in lines)
                {
                    string[] parts = line.Split('=');
                    if (parts.Length != 2)
                    {
                        return false;
                    }

                    string key = parts[0].Trim();
                    string value = parts[1].Trim();

                    this.mMap.Add(key, value);
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
