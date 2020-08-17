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
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewLogToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.textBoxLogWindow = new System.Windows.Forms.TextBox();
            this.labelInstancesTitle = new System.Windows.Forms.Label();
            this.comboBoxInstances = new System.Windows.Forms.ComboBox();
            this.buttonInstanceAdd = new System.Windows.Forms.Button();
            this.buttonInstance = new System.Windows.Forms.Button();
            this.buttonInstanceUp = new System.Windows.Forms.Button();
            this.buttonInstanceDown = new System.Windows.Forms.Button();
            this.labelInstanceConfigurator = new System.Windows.Forms.Label();
            this.labeloBITInstallDir = new System.Windows.Forms.Label();
            this.buttonEditInstanceName = new System.Windows.Forms.Button();
            this.mInstanceConfigurator = new obit_manager_gui.components.InstanceConfigurator();
            this.labelSelectedInstanceIsDefault = new System.Windows.Forms.Label();
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
            this.buttonOBITInstallationDirectory.Location = new System.Drawing.Point(12, 51);
            this.buttonOBITInstallationDirectory.Name = "buttonOBITInstallationDirectory";
            this.buttonOBITInstallationDirectory.Size = new System.Drawing.Size(580, 34);
            this.buttonOBITInstallationDirectory.TabIndex = 0;
            this.buttonOBITInstallationDirectory.Text = "Pick oBIT installation dir...";
            this.buttonOBITInstallationDirectory.UseVisualStyleBackColor = true;
            this.buttonOBITInstallationDirectory.Click += new System.EventHandler(this.buttonOBITInstallationDirectory_Click);
            // 
            // buttonFreshInstall
            // 
            this.buttonFreshInstall.Location = new System.Drawing.Point(12, 347);
            this.buttonFreshInstall.Name = "buttonFreshInstall";
            this.buttonFreshInstall.Size = new System.Drawing.Size(580, 34);
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
            this.menuStrip1.Size = new System.Drawing.Size(604, 24);
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
            this.downloadOnlyToolStripMenuItem.Size = new System.Drawing.Size(170, 22);
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
            this.checkForUpdatesToolStripMenuItem.Size = new System.Drawing.Size(170, 22);
            this.checkForUpdatesToolStripMenuItem.Text = "Check for updates";
            // 
            // settingsToolStripMenuItem
            // 
            this.settingsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.loadToolStripMenuItem,
            this.saveToolStripMenuItem});
            this.settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            this.settingsToolStripMenuItem.Size = new System.Drawing.Size(170, 22);
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
            this.viewLogToolStripMenuItem.Size = new System.Drawing.Size(122, 22);
            this.viewLogToolStripMenuItem.Text = "View Log";
            this.viewLogToolStripMenuItem.Click += new System.EventHandler(this.viewLogToolStripMenuItem_Click);
            // 
            // textBoxLogWindow
            // 
            this.textBoxLogWindow.Location = new System.Drawing.Point(12, 387);
            this.textBoxLogWindow.Multiline = true;
            this.textBoxLogWindow.Name = "textBoxLogWindow";
            this.textBoxLogWindow.Size = new System.Drawing.Size(580, 51);
            this.textBoxLogWindow.TabIndex = 4;
            // 
            // labelInstancesTitle
            // 
            this.labelInstancesTitle.AutoSize = true;
            this.labelInstancesTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelInstancesTitle.Location = new System.Drawing.Point(12, 104);
            this.labelInstancesTitle.Name = "labelInstancesTitle";
            this.labelInstancesTitle.Size = new System.Drawing.Size(62, 13);
            this.labelInstancesTitle.TabIndex = 5;
            this.labelInstancesTitle.Text = "Instances";
            // 
            // comboBoxInstances
            // 
            this.comboBoxInstances.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxInstances.FormattingEnabled = true;
            this.comboBoxInstances.Location = new System.Drawing.Point(15, 126);
            this.comboBoxInstances.Name = "comboBoxInstances";
            this.comboBoxInstances.Size = new System.Drawing.Size(408, 21);
            this.comboBoxInstances.TabIndex = 6;
            this.comboBoxInstances.SelectedIndexChanged += new System.EventHandler(this.comboBoxInstances_SelectedIndexChanged);
            // 
            // buttonInstanceAdd
            // 
            this.buttonInstanceAdd.Location = new System.Drawing.Point(462, 125);
            this.buttonInstanceAdd.Name = "buttonInstanceAdd";
            this.buttonInstanceAdd.Size = new System.Drawing.Size(28, 23);
            this.buttonInstanceAdd.TabIndex = 8;
            this.buttonInstanceAdd.Text = "+";
            this.buttonInstanceAdd.UseVisualStyleBackColor = true;
            // 
            // buttonInstance
            // 
            this.buttonInstance.Location = new System.Drawing.Point(496, 125);
            this.buttonInstance.Name = "buttonInstance";
            this.buttonInstance.Size = new System.Drawing.Size(28, 23);
            this.buttonInstance.TabIndex = 9;
            this.buttonInstance.Text = "‒";
            this.buttonInstance.UseVisualStyleBackColor = true;
            // 
            // buttonInstanceUp
            // 
            this.buttonInstanceUp.Location = new System.Drawing.Point(530, 125);
            this.buttonInstanceUp.Name = "buttonInstanceUp";
            this.buttonInstanceUp.Size = new System.Drawing.Size(28, 23);
            this.buttonInstanceUp.TabIndex = 10;
            this.buttonInstanceUp.Text = "↑";
            this.buttonInstanceUp.UseVisualStyleBackColor = true;
            this.buttonInstanceUp.Click += new System.EventHandler(this.buttonInstanceUp_Click);
            // 
            // buttonInstanceDown
            // 
            this.buttonInstanceDown.Location = new System.Drawing.Point(564, 125);
            this.buttonInstanceDown.Name = "buttonInstanceDown";
            this.buttonInstanceDown.Size = new System.Drawing.Size(28, 23);
            this.buttonInstanceDown.TabIndex = 11;
            this.buttonInstanceDown.Text = "↓";
            this.buttonInstanceDown.UseVisualStyleBackColor = true;
            this.buttonInstanceDown.Click += new System.EventHandler(this.buttonInstanceDown_Click);
            // 
            // labelInstanceConfigurator
            // 
            this.labelInstanceConfigurator.AutoSize = true;
            this.labelInstanceConfigurator.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelInstanceConfigurator.Location = new System.Drawing.Point(12, 190);
            this.labelInstanceConfigurator.Name = "labelInstanceConfigurator";
            this.labelInstanceConfigurator.Size = new System.Drawing.Size(129, 13);
            this.labelInstanceConfigurator.TabIndex = 13;
            this.labelInstanceConfigurator.Text = "Instance Configurator";
            // 
            // labeloBITInstallDir
            // 
            this.labeloBITInstallDir.AutoSize = true;
            this.labeloBITInstallDir.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labeloBITInstallDir.Location = new System.Drawing.Point(12, 29);
            this.labeloBITInstallDir.Name = "labeloBITInstallDir";
            this.labeloBITInstallDir.Size = new System.Drawing.Size(120, 13);
            this.labeloBITInstallDir.TabIndex = 14;
            this.labeloBITInstallDir.Text = "oBIT Installation Dir";
            // 
            // buttonEditInstanceName
            // 
            this.buttonEditInstanceName.Location = new System.Drawing.Point(429, 125);
            this.buttonEditInstanceName.Name = "buttonEditInstanceName";
            this.buttonEditInstanceName.Size = new System.Drawing.Size(28, 23);
            this.buttonEditInstanceName.TabIndex = 15;
            this.buttonEditInstanceName.Text = "✎";
            this.buttonEditInstanceName.UseVisualStyleBackColor = true;
            this.buttonEditInstanceName.Click += new System.EventHandler(this.buttonEditInstanceName_Click);
            // 
            // mInstanceConfigurator
            // 
            this.mInstanceConfigurator.Location = new System.Drawing.Point(12, 212);
            this.mInstanceConfigurator.Name = "mInstanceConfigurator";
            this.mInstanceConfigurator.Size = new System.Drawing.Size(592, 129);
            this.mInstanceConfigurator.TabIndex = 12;
            // 
            // labelSelectedInstanceIsDefault
            // 
            this.labelSelectedInstanceIsDefault.AutoSize = true;
            this.labelSelectedInstanceIsDefault.Location = new System.Drawing.Point(15, 154);
            this.labelSelectedInstanceIsDefault.Name = "labelSelectedInstanceIsDefault";
            this.labelSelectedInstanceIsDefault.Size = new System.Drawing.Size(0, 13);
            this.labelSelectedInstanceIsDefault.TabIndex = 16;
            // 
            // obit_manager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(604, 450);
            this.Controls.Add(this.labelSelectedInstanceIsDefault);
            this.Controls.Add(this.buttonEditInstanceName);
            this.Controls.Add(this.labeloBITInstallDir);
            this.Controls.Add(this.labelInstanceConfigurator);
            this.Controls.Add(this.mInstanceConfigurator);
            this.Controls.Add(this.buttonInstanceDown);
            this.Controls.Add(this.buttonInstanceUp);
            this.Controls.Add(this.buttonInstance);
            this.Controls.Add(this.buttonInstanceAdd);
            this.Controls.Add(this.comboBoxInstances);
            this.Controls.Add(this.labelInstancesTitle);
            this.Controls.Add(this.textBoxLogWindow);
            this.Controls.Add(this.buttonFreshInstall);
            this.Controls.Add(this.buttonOBITInstallationDirectory);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "obit_manager";
            this.Text = " ";
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
        private System.Windows.Forms.Label labelInstancesTitle;
        private System.Windows.Forms.ComboBox comboBoxInstances;
        private System.Windows.Forms.Button buttonInstanceAdd;
        private System.Windows.Forms.Button buttonInstance;
        private System.Windows.Forms.Button buttonInstanceUp;
        private System.Windows.Forms.Button buttonInstanceDown;
        private components.InstanceConfigurator mInstanceConfigurator;
        private System.Windows.Forms.Label labelInstanceConfigurator;
        private System.Windows.Forms.Label labeloBITInstallDir;
        private System.Windows.Forms.Button buttonEditInstanceName;
        private System.Windows.Forms.Label labelSelectedInstanceIsDefault;
    }
}

