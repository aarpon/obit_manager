using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace obit_manager_gui.dialogs
{
    public partial class SingleStringEditor : Form
    {
        private string mResult;

        public string Result => this.mResult;

        public SingleStringEditor(string dialogTitle, string stringLabel, string stringStartValue = "")
        {
            InitializeComponent();

            this.Name = dialogTitle;
            this.labelString.Text = stringLabel;
            this.textBoxString.Text = stringStartValue;

            this.mResult = stringStartValue;

        }

        private void textBoxString_TextChanged(object sender, EventArgs e)
        {
            this.Validate();
        }

        private void textBoxString_Validating(object sender, CancelEventArgs e)
        {
            if (this.textBoxString.Text.Equals(""))
            {
                this.textBoxString.BackColor = Color.Red;
                e.Cancel = true;
            }
            else
            {
                this.textBoxString.BackColor = Color.White;
                e.Cancel = false;
            }
        }

        private void textBoxString_Validated(object sender, EventArgs e)
        {
            if (this.textBoxString.Text.Equals(""))
            {
                return;
            }

            // Update the result
            this.mResult = this.textBoxString.Text;
        }

        private void buttonOk_Click(object sender, EventArgs e)
        {
            // Accept the changes
            this.DialogResult = DialogResult.OK;

            // Close the dialog
            this.Close();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            // Reject the changes
            this.DialogResult = DialogResult.Cancel;

            // Close the dialog
            this.Close();
        }
    }
}
