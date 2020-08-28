using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using obit_manager_gui.components;
using obit_manager_settings.components;

namespace obit_manager_gui
{
    public partial class obit_manager
    {
        #region event_handlers

        private void RegisterEventHandlers()
        {
            InstallationManager.OperationDescriptionChanged += OperationDescriptionChangedHandler;
            InstallationManager.OperationProgressChanged += OperationProgressChangedHandler;
            InstallationManager.TextToDisplayToOutputPaneReady += TextToDisplayToOutputPaneReadyHandler;
        }

        // Event handlers
        void OperationDescriptionChangedHandler(object sender, string description)
        {
            // Display the operation description
            this.labelOperations.Text = description;
        }

        void OperationProgressChangedHandler(object sender, int value)
        {
            // Display the operation description
            this.progressBarOperations.Value = value;
        }

        void TextToDisplayToOutputPaneReadyHandler(object sender, string text)
        {
            // Display the operation description
            this.textBoxOutputPane.AppendText(text + "\r\n");
        }

        #endregion event_handlers
    }
}
