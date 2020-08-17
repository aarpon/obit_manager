using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static obit_manager_settings.Constants;

namespace obit_manager_settings.components
{
    public partial class Instance
    {
        // Configuration name
        private string mName = "Default";

        // A Client
        private Client mClient = new Client();

        // A Server
        private Server mServer = new Server();

        // A Datamover
        private Datamover mDatamover = new Datamover();

        /// <summary>
        /// Default constructor.
        /// </summary>
        /// <param name="name"></param>
        public Instance(string name = "Default")
        {
            // Configuration name
            this.mName = name;

            // A Client
            this.mClient = new Client();

            // A Server
            this.mServer = new Server();

            // A Datamover
            this.mDatamover = new Datamover();
        }

        /// <summary>
        /// Alternative constructor.
        /// </summary>
        /// <param name="name"></param>
        public Instance(string name, Client client, Server server, Datamover datamover)
        {
            // Configuration name
            this.mName = name;

            // A Client
            this.mClient = client;

            // A Server
            this.mServer = server;

            // A Datamover
            this.mDatamover = datamover;
        }

        /// <summary>
        /// Validate that the various parts of the Instance settings are consistent and correct.
        /// </summary>
        /// <returns>True if the Instance is valid and consistent, false otherwise.</returns>
        public bool Validate()
        {
            // @ToDo: Implement me!
            return true;
        }

        #region properties

        public Client ClientRef
        {
            get => this.mClient;
            set
            {
                this.mClient = value;

                // @TODO Update client - datamover - server logical links
            }
        }

        public Server ServerRef
        {
            get => this.mServer;
            set
            {
                this.mServer = value;

                // @TODO Update client - datamover - server logical links
            }
        }

        public Datamover DatamoverRef
        {
            get => this.mDatamover;
            set
            {
                this.mDatamover = value;

                // @TODO Update client - datamover - server logical links
            }
        }

        #endregion properties

    }
}
