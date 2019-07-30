using System.ServiceProcess;

namespace obit_manager_services
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main()
        {
            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[]
            {
                new DatamoverService()
            };
            ServiceBase.Run(ServicesToRun);
        }
    }
}
