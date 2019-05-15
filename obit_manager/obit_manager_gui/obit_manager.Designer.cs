namespace obit_manager
{
    partial class obit_manager
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(obit_manager));
            this.obitManagerNotifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.buttonOBITInstallationDirectory = new System.Windows.Forms.Button();
            this.buttonFreshInstall = new System.Windows.Forms.Button();
            this.groupBoxTargetPlatform = new System.Windows.Forms.GroupBox();
            this.checkBoxPlatformThisMachine = new System.Windows.Forms.CheckBox();
            this.radioButtonPlatform64bit = new System.Windows.Forms.RadioButton();
            this.radioButtonPlatform32bit = new System.Windows.Forms.RadioButton();
            this.groupBoxTargetPlatform.SuspendLayout();
            this.SuspendLayout();
            // 
            // obitManagerNotifyIcon
            // 
            this.obitManagerNotifyIcon.Text = "oBIT manager";
            this.obitManagerNotifyIcon.Visible = true;
            // 
            // buttonOBITInstallationDirectory
            // 
            this.buttonOBITInstallationDirectory.Location = new System.Drawing.Point(12, 12);
            this.buttonOBITInstallationDirectory.Name = "buttonOBITInstallationDirectory";
            this.buttonOBITInstallationDirectory.Size = new System.Drawing.Size(396, 34);
            this.buttonOBITInstallationDirectory.TabIndex = 0;
            this.buttonOBITInstallationDirectory.Text = "Pick oBIT installation dir...";
            this.buttonOBITInstallationDirectory.UseVisualStyleBackColor = true;
            this.buttonOBITInstallationDirectory.Click += new System.EventHandler(this.buttonOBITInstallationDirectory_Click);
            // 
            // buttonFreshInstall
            // 
            this.buttonFreshInstall.Location = new System.Drawing.Point(12, 404);
            this.buttonFreshInstall.Name = "buttonFreshInstall";
            this.buttonFreshInstall.Size = new System.Drawing.Size(396, 34);
            this.buttonFreshInstall.TabIndex = 1;
            this.buttonFreshInstall.Text = "Fresh install";
            this.buttonFreshInstall.UseVisualStyleBackColor = true;
            this.buttonFreshInstall.Click += new System.EventHandler(this.buttonFreshInstall_Click);
            // 
            // groupBoxTargetPlatform
            // 
            this.groupBoxTargetPlatform.Controls.Add(this.checkBoxPlatformThisMachine);
            this.groupBoxTargetPlatform.Controls.Add(this.radioButtonPlatform64bit);
            this.groupBoxTargetPlatform.Controls.Add(this.radioButtonPlatform32bit);
            this.groupBoxTargetPlatform.Location = new System.Drawing.Point(12, 52);
            this.groupBoxTargetPlatform.Name = "groupBoxTargetPlatform";
            this.groupBoxTargetPlatform.Size = new System.Drawing.Size(396, 48);
            this.groupBoxTargetPlatform.TabIndex = 2;
            this.groupBoxTargetPlatform.TabStop = false;
            this.groupBoxTargetPlatform.Text = "Target platform";
            // 
            // checkBoxPlatformThisMachine
            // 
            this.checkBoxPlatformThisMachine.AutoSize = true;
            this.checkBoxPlatformThisMachine.Checked = true;
            this.checkBoxPlatformThisMachine.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxPlatformThisMachine.Location = new System.Drawing.Point(25, 19);
            this.checkBoxPlatformThisMachine.Name = "checkBoxPlatformThisMachine";
            this.checkBoxPlatformThisMachine.Size = new System.Drawing.Size(130, 17);
            this.checkBoxPlatformThisMachine.TabIndex = 4;
            this.checkBoxPlatformThisMachine.Text = "Install on this machine";
            this.checkBoxPlatformThisMachine.UseVisualStyleBackColor = true;
            // 
            // radioButtonPlatform64bit
            // 
            this.radioButtonPlatform64bit.AutoSize = true;
            this.radioButtonPlatform64bit.Location = new System.Drawing.Point(312, 19);
            this.radioButtonPlatform64bit.Name = "radioButtonPlatform64bit";
            this.radioButtonPlatform64bit.Size = new System.Drawing.Size(51, 17);
            this.radioButtonPlatform64bit.TabIndex = 3;
            this.radioButtonPlatform64bit.TabStop = true;
            this.radioButtonPlatform64bit.Text = "64 bit";
            this.radioButtonPlatform64bit.UseVisualStyleBackColor = true;
            // 
            // radioButtonPlatform32bit
            // 
            this.radioButtonPlatform32bit.AutoSize = true;
            this.radioButtonPlatform32bit.Location = new System.Drawing.Point(208, 19);
            this.radioButtonPlatform32bit.Name = "radioButtonPlatform32bit";
            this.radioButtonPlatform32bit.Size = new System.Drawing.Size(51, 17);
            this.radioButtonPlatform32bit.TabIndex = 0;
            this.radioButtonPlatform32bit.TabStop = true;
            this.radioButtonPlatform32bit.Text = "32 bit";
            this.radioButtonPlatform32bit.UseVisualStyleBackColor = true;
            // 
            // obit_manager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(420, 450);
            this.Controls.Add(this.groupBoxTargetPlatform);
            this.Controls.Add(this.buttonFreshInstall);
            this.Controls.Add(this.buttonOBITInstallationDirectory);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "obit_manager";
            this.Text = "oBIT manager";
            this.groupBoxTargetPlatform.ResumeLayout(false);
            this.groupBoxTargetPlatform.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.NotifyIcon obitManagerNotifyIcon;
        private System.Windows.Forms.Button buttonOBITInstallationDirectory;
        private System.Windows.Forms.Button buttonFreshInstall;
        private System.Windows.Forms.GroupBox groupBoxTargetPlatform;
        private System.Windows.Forms.RadioButton radioButtonPlatform32bit;
        private System.Windows.Forms.RadioButton radioButtonPlatform64bit;
        private System.Windows.Forms.CheckBox checkBoxPlatformThisMachine;
    }
}

