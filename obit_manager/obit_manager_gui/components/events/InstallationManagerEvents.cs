using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace obit_manager_gui.components
{
    internal partial class InstallationManager
    {
        #region events
        public static event EventHandler<string> OperationDescriptionChanged;
        public static event EventHandler<int> OperationProgressChanged;
        public static event EventHandler<string> TextToDisplayToOutputPaneReady;

        public static void OnOperationDescriptionChange(InstallationManager installationManager, string description)
        {
            EventHandler<string> handler = OperationDescriptionChanged;
            handler?.Invoke(installationManager, description);
        }

        public static void OnOperationProgressChanged(InstallationManager installationManager, int progress)
        {
            EventHandler<int> handler = OperationProgressChanged;
            handler?.Invoke(installationManager, progress);
        }

        public static void OnTextToDisplayToOutputPaneReady(InstallationManager installationManager, string text)
        {
            EventHandler<string> handler = TextToDisplayToOutputPaneReady;
            handler?.Invoke(installationManager, text);
        }

        #endregion events
    }
}
