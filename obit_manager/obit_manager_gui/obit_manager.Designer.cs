namespace obit_manager_gui
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.downloadOnlyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.download32bitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.download64bitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.checkForUpdatesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.settingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.textBoxLogWindow = new System.Windows.Forms.TextBox();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewLogToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // obitManagerNotifyIcon
            // 
            this.obitManagerNotifyIcon.Text = "oBIT manager";
            this.obitManagerNotifyIcon.Visible = true;
            // 
            // buttonOBITInstallationDirectory
            // 
            this.buttonOBITInstallationDirectory.Location = new System.Drawing.Point(12, 27);
            this.buttonOBITInstallationDirectory.Name = "buttonOBITInstallationDirectory";
            this.buttonOBITInstallationDirectory.Size = new System.Drawing.Size(396, 34);
            this.buttonOBITInstallationDirectory.TabIndex = 0;
            this.buttonOBITInstallationDirectory.Text = "Pick oBIT installation dir...";
            this.buttonOBITInstallationDirectory.UseVisualStyleBackColor = true;
            this.buttonOBITInstallationDirectory.Click += new System.EventHandler(this.buttonOBITInstallationDirectory_Click);
            // 
            // buttonFreshInstall
            // 
            this.buttonFreshInstall.Location = new System.Drawing.Point(12, 347);
            this.buttonFreshInstall.Name = "buttonFreshInstall";
            this.buttonFreshInstall.Size = new System.Drawing.Size(396, 34);
            this.buttonFreshInstall.TabIndex = 1;
            this.buttonFreshInstall.Text = "Fresh install";
            this.buttonFreshInstall.UseVisualStyleBackColor = true;
            this.buttonFreshInstall.Click += new System.EventHandler(this.buttonFreshInstall_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(420, 24);
            this.menuStrip1.TabIndex = 3;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.downloadOnlyToolStripMenuItem,
            this.checkForUpdatesToolStripMenuItem,
            this.settingsToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // downloadOnlyToolStripMenuItem
            // 
            this.downloadOnlyToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.download32bitToolStripMenuItem,
            this.download64bitToolStripMenuItem});
            this.downloadOnlyToolStripMenuItem.Name = "downloadOnlyToolStripMenuItem";
            this.downloadOnlyToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.downloadOnlyToolStripMenuItem.Text = "Download only";
            // 
            // download32bitToolStripMenuItem
            // 
            this.download32bitToolStripMenuItem.Name = "download32bitToolStripMenuItem";
            this.download32bitToolStripMenuItem.Size = new System.Drawing.Size(103, 22);
            this.download32bitToolStripMenuItem.Text = "32 bit";
            this.download32bitToolStripMenuItem.Click += new System.EventHandler(this.download32bitToolStripMenuItem_Click);
            // 
            // download64bitToolStripMenuItem
            // 
            this.download64bitToolStripMenuItem.Name = "download64bitToolStripMenuItem";
            this.download64bitToolStripMenuItem.Size = new System.Drawing.Size(103, 22);
            this.download64bitToolStripMenuItem.Text = "64 bit";
            this.download64bitToolStripMenuItem.Click += new System.EventHandler(this.download64bitToolStripMenuItem_Click);
            // 
            // checkForUpdatesToolStripMenuItem
            // 
            this.checkForUpdatesToolStripMenuItem.Name = "checkForUpdatesToolStripMenuItem";
            this.checkForUpdatesToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.checkForUpdatesToolStripMenuItem.Text = "Check for updates";
            // 
            // settingsToolStripMenuItem
            // 
            this.settingsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.loadToolStripMenuItem,
            this.saveToolStripMenuItem});
            this.settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            this.settingsToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.settingsToolStripMenuItem.Text = "Settings";
            // 
            // loadToolStripMenuItem
            // 
            this.loadToolStripMenuItem.Name = "loadToolStripMenuItem";
            this.loadToolStripMenuItem.Size = new System.Drawing.Size(100, 22);
            this.loadToolStripMenuItem.Text = "Load";
            this.loadToolStripMenuItem.Click += new System.EventHandler(this.loadToolStripMenuItem_Click);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(100, 22);
            this.saveToolStripMenuItem.Text = "Save";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
            // 
            // textBoxLogWindow
            // 
            this.textBoxLogWindow.Location = new System.Drawing.Point(12, 387);
            this.textBoxLogWindow.Multiline = true;
            this.textBoxLogWindow.Name = "textBoxLogWindow";
            this.textBoxLogWindow.Size = new System.Drawing.Size(396, 51);
            this.textBoxLogWindow.TabIndex = 4;
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.viewLogToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // viewLogToolStripMenuItem
            // 
            this.viewLogToolStripMenuItem.Name = "viewLogToolStripMenuItem";
            this.viewLogToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.viewLogToolStripMenuItem.Text = "View Log";
            this.viewLogToolStripMenuItem.Click += new System.EventHandler(this.viewLogToolStripMenuItem_Click);
            // 
            // obit_manager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(420, 450);
            this.Controls.Add(this.textBoxLogWindow);
            this.Controls.Add(this.buttonFreshInstall);
            this.Controls.Add(this.buttonOBITInstallationDirectory);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "obit_manager";
            this.Text = "oBIT manager";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.NotifyIcon obitManagerNotifyIcon;
        private System.Windows.Forms.Button buttonOBITInstallationDirectory;
        private System.Windows.Forms.Button buttonFreshInstall;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem downloadOnlyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem download32bitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem download64bitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem settingsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loadToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.TextBox textBoxLogWindow;
        private System.Windows.Forms.ToolStripMenuItem checkForUpdatesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem viewLogToolStripMenuItem;
    }
}

