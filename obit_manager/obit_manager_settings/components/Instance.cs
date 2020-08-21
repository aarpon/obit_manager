using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32.SafeHandles;
using static obit_manager_settings.Constants;

namespace obit_manager_settings.components
{
    public partial class Instance
    {
        // Name of the Instance
        private string mName;

        // Index of the Client
        private int mClientIndex;

        // A Server
        private int mServerIndex;

        // A Datamover
        private int mDatamoverIndex;

        /// <summary>
        /// Default constructor.
        /// </summary>
        public Instance()
        {
            // Initialize with default name
            this.mName = "Default";

            // Initialize with invalid Client index
            this.mClientIndex = -1;

            // Initialize with invalid Server index
            this.mServerIndex = -1;

            // Initialize with invalid Datamover index
            this.mDatamoverIndex = -1;
        }

        /// <summary>
        /// Alternative constructor.
        /// </summary>
        /// <param name="clientIndex">Index of the Client in the list of Clients.</param>
        /// <param name="serverIndex">Index of the Server in the list of Servers.</param>
        /// <param name="datamoverIndex">Index of the Datamover in the list of Datamovers.</param>
        public Instance(string name, int clientIndex, int serverIndex, int datamoverIndex)
        {
            // Addign the given name
            this.mName = name;

            // Assign given Client index
            this.mClientIndex = clientIndex;

            // Assign given Server index
            this.mServerIndex = serverIndex;

            // Assign given Datamover index
            this.mDatamoverIndex = datamoverIndex;
        }

        #region properties

        public string Name
        {
            get => this.mName;
            set
            {
                if (value.Length == 0)
                {
                    return;
                }

                this.mName = value;

                // Raise the event
                Instance.OnConfigurationNameChanged(this, value);
            }
        }

        public int ClientIndex
        {
            get => this.mClientIndex;
            set
            {
                this.mClientIndex = value;

                // @TODO Update client - datamover - server logical links
            }
        }

        public int ServerIndex
        {
            get => this.mServerIndex;
            set
            {
                this.mServerIndex = value;

                // @TODO Update client - datamover - server logical links
            }
        }

        public int DatamoverIndex
        {
            get => this.mDatamoverIndex;
            set
            {
                this.mDatamoverIndex = value;

                // @TODO Update client - datamover - server logical links
            }
        }

        #endregion properties

    }
}
