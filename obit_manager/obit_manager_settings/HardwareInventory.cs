using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace obit_manager_settings
{
    public static class Hardware
    {

        public static Dictionary<string, HardwareMapEntry> Inventory = new Dictionary<string, HardwareMapEntry>()
        {
            {
                "microscopy", 
                new HardwareMapEntry()
                {
                    Key = "microscopy",
                    Category = "Microscopy",
                    Name = "All microscopes",
                    Description = "All bioformats-compatible light microscopes."
                }
            },
            {
                "lsrfortessa",
                new HardwareMapEntry()
                {
                    Key = "lsrfortessa",
                    Category = "Flow cytometry",
                    Name = "BD LSR Fortessa",
                    Description = "BD BioSciences LSR Fortessa cell analyzer."
                }
            },
            {
                "facsaria", 
                new HardwareMapEntry()
                {
                    Key = "facsaria",
                    Category = "Flow cytometry",
                    Name = "BD FACS Aria",
                    Description = "BD BioSciences FACS Aria III/Fusion cell sorter."
                }
            },
            {
                "influx",
                new HardwareMapEntry()
                {
                    Key = "influx",
                    Category = "Flow cytometry",
                    Name = "BD Influx",
                    Description = "BD BioSciences Influx cell sorter."
                }
            },
            {
                "s3e",
                new HardwareMapEntry()
                {
                    Key = "s3e",
                    Category = "Flow cytometry",
                    Name = "Bio-Rad S3e",
                    Description = "Bio-Rad S3e cell sorter."
                }
            },
            {
                "mofloxdp",
                new HardwareMapEntry()
                {
                    Key = "mofloxdp",
                    Category = "Flow cytometry",
                    Name = "BC MoFlo XDP",
                    Description = "Beckman Coulter MoFlow XDP cell sorter."
                }
            },
            {
                "sonysh800s",
                new HardwareMapEntry()
                {
                    Key = "sonysh800s",
                    Category = "Flow cytometry",
                    Name = "Sony SH800S",
                    Description = "Sony Biotechnology SH800S cell sorter."
                }
            },
            {
                "sonyma900",
                new HardwareMapEntry()
                {
                    Key = "sonyma900",
                    Category = "Flow cytometry",
                    Name = "Sony MA900",
                    Description = "Sony Biotechnology MA900 cell sorter."
                }
            },
            {
                "cytoflexS",
                new HardwareMapEntry()
                {
                    Key = "cytoflexS",
                    Category = "Flow cytometry",
                    Name = "BC CytoFlexS",
                    Description = "Beckman Coulter CytoFLEX S cell analyzer."
                }
            }
        };
    }

    public class HardwareMapEntry
    {
        public string Key { get; set; }
        public string Category { get; set; }
        public string Name { get; set; }
        public String Description { get; set; }
    };

}
