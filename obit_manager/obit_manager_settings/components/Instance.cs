using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static obit_manager_settings.Constants;

namespace obit_manager_settings
{
    namespace components
    {
        public class Instance
        {
            // A Client
            private Client mClient = new Client();

            // A Server
            private Server mServer = new Server();

            // A Datamover
            private Datamover mDatamover = new Datamover();
        }
    }
}
