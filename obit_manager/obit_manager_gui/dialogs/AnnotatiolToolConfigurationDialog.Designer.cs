namespace obit_manager_gui.dialogs
{
    partial class AnnotatiolToolConfigurationDialog
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.buttonCancel = new System.Windows.Forms.Button();
            this.buttonAccept = new System.Windows.Forms.Button();
            this.labelUserDataDir = new System.Windows.Forms.Label();
            this.buttonUserDataDir = new System.Windows.Forms.Button();
            this.labelHumanFriedlyMachineName = new System.Windows.Forms.Label();
            this.textBoxHumanFriedlyMachineName = new System.Windows.Forms.TextBox();
            this.checkBoxCreateMarkerFile = new System.Windows.Forms.CheckBox();
            this.buttonNetworkHostName = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // buttonCancel
            // 
            this.buttonCancel.Location = new System.Drawing.Point(232, 132);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(204, 23);
            this.buttonCancel.TabIndex = 14;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // buttonAccept
            // 
            this.buttonAccept.Location = new System.Drawing.Point(12, 132);
            this.buttonAccept.Name = "buttonAccept";
            this.buttonAccept.Size = new System.Drawing.Size(204, 23);
            this.buttonAccept.TabIndex = 13;
            this.buttonAccept.Text = "Ok";
            this.buttonAccept.UseVisualStyleBackColor = true;
            this.buttonAccept.Click += new System.EventHandler(this.buttonAccept_Click);
            // 
            // labelUserDataDir
            // 
            this.labelUserDataDir.AutoSize = true;
            this.labelUserDataDir.Location = new System.Drawing.Point(12, 9);
            this.labelUserDataDir.Name = "labelUserDataDir";
            this.labelUserDataDir.Size = new System.Drawing.Size(82, 13);
            this.labelUserDataDir.TabIndex = 15;
            this.labelUserDataDir.Text = "User data folder";
            // 
            // buttonUserDataDir
            // 
            this.buttonUserDataDir.Location = new System.Drawing.Point(12, 26);
            this.buttonUserDataDir.Name = "buttonUserDataDir";
            this.buttonUserDataDir.Size = new System.Drawing.Size(424, 23);
            this.buttonUserDataDir.TabIndex = 16;
            this.buttonUserDataDir.Text = "...";
            this.buttonUserDataDir.UseVisualStyleBackColor = true;
            this.buttonUserDataDir.Click += new System.EventHandler(this.buttonUserDataDir_Click);
            // 
            // labelHumanFriedlyMachineName
            // 
            this.labelHumanFriedlyMachineName.AutoSize = true;
            this.labelHumanFriedlyMachineName.Location = new System.Drawing.Point(12, 62);
            this.labelHumanFriedlyMachineName.Name = "labelHumanFriedlyMachineName";
            this.labelHumanFriedlyMachineName.Size = new System.Drawing.Size(115, 13);
            this.labelHumanFriedlyMachineName.TabIndex = 17;
            this.labelHumanFriedlyMachineName.Text = "Friendly machine name";
            // 
            // textBoxHumanFriedlyMachineName
            // 
            this.textBoxHumanFriedlyMachineName.Location = new System.Drawing.Point(133, 58);
            this.textBoxHumanFriedlyMachineName.Name = "textBoxHumanFriedlyMachineName";
            this.textBoxHumanFriedlyMachineName.Size = new System.Drawing.Size(247, 20);
            this.textBoxHumanFriedlyMachineName.TabIndex = 18;
            this.textBoxHumanFriedlyMachineName.TextChanged += new System.EventHandler(this.textBoxHumanFriedlyMachineName_TextChanged);
            // 
            // checkBoxCreateMarkerFile
            // 
            this.checkBoxCreateMarkerFile.AutoSize = true;
            this.checkBoxCreateMarkerFile.Location = new System.Drawing.Point(12, 97);
            this.checkBoxCreateMarkerFile.Name = "checkBoxCreateMarkerFile";
            this.checkBoxCreateMarkerFile.Size = new System.Drawing.Size(165, 17);
            this.checkBoxCreateMarkerFile.TabIndex = 19;
            this.checkBoxCreateMarkerFile.Text = "Create marker file (advanced)";
            this.checkBoxCreateMarkerFile.UseVisualStyleBackColor = true;
            this.checkBoxCreateMarkerFile.CheckedChanged += new System.EventHandler(this.checkBoxCreateMarkerFile_CheckedChanged);
            // 
            // buttonNetworkHostName
            // 
            this.buttonNetworkHostName.Location = new System.Drawing.Point(386, 57);
            this.buttonNetworkHostName.Name = "buttonNetworkHostName";
            this.buttonNetworkHostName.Size = new System.Drawing.Size(50, 23);
            this.buttonNetworkHostName.TabIndex = 20;
            this.buttonNetworkHostName.Text = "Reset";
            this.buttonNetworkHostName.UseVisualStyleBackColor = true;
            this.buttonNetworkHostName.Click += new System.EventHandler(this.buttonNetworkHostName_Click);
            // 
            // AnnotatiolToolConfigurationDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(445, 173);
            this.Controls.Add(this.buttonNetworkHostName);
            this.Controls.Add(this.labelHumanFriedlyMachineName);
            this.Controls.Add(this.textBoxHumanFriedlyMachineName);
            this.Controls.Add(this.checkBoxCreateMarkerFile);
            this.Controls.Add(this.buttonUserDataDir);
            this.Controls.Add(this.labelUserDataDir);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonAccept);
            this.Name = "AnnotatiolToolConfigurationDialog";
            this.Text = "Edit Annotation Tool configuration";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Button buttonAccept;
        private System.Windows.Forms.Label labelUserDataDir;
        private System.Windows.Forms.Button buttonUserDataDir;
        private System.Windows.Forms.Label labelHumanFriedlyMachineName;
        private System.Windows.Forms.TextBox textBoxHumanFriedlyMachineName;
        private System.Windows.Forms.CheckBox checkBoxCreateMarkerFile;
        private System.Windows.Forms.Button buttonNetworkHostName;
    }
}