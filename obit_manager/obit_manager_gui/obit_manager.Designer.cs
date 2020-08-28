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
            this.buttonApplyAllChanges = new System.Windows.Forms.Button();
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
            this.textBoxOutputPane = new System.Windows.Forms.TextBox();
            this.labelInstancesTitle = new System.Windows.Forms.Label();
            this.comboBoxInstances = new System.Windows.Forms.ComboBox();
            this.buttonInstanceAdd = new System.Windows.Forms.Button();
            this.buttonInstance = new System.Windows.Forms.Button();
            this.buttonInstanceUp = new System.Windows.Forms.Button();
            this.buttonInstanceDown = new System.Windows.Forms.Button();
            this.labelInstanceConfigurator = new System.Windows.Forms.Label();
            this.labeloBITInstallDir = new System.Windows.Forms.Label();
            this.buttonEditInstanceName = new System.Windows.Forms.Button();
            this.labelSelectedInstanceIsDefault = new System.Windows.Forms.Label();
            this.groupBoxInstance = new System.Windows.Forms.GroupBox();
            this.buttonServerEdit = new System.Windows.Forms.Button();
            this.buttonServerRemove = new System.Windows.Forms.Button();
            this.buttonServerAdd = new System.Windows.Forms.Button();
            this.labelServer = new System.Windows.Forms.Label();
            this.comboBoxServer = new System.Windows.Forms.ComboBox();
            this.buttonDatamoverIncomingFolderEdit = new System.Windows.Forms.Button();
            this.buttonDatamoverIncomingFolderRemove = new System.Windows.Forms.Button();
            this.buttonDatamoverIncomingFolderAdd = new System.Windows.Forms.Button();
            this.labelDatamoverIncomingFolder = new System.Windows.Forms.Label();
            this.comboBoxDatamoverIncomingFolder = new System.Windows.Forms.ComboBox();
            this.buttonUserFolderEdit = new System.Windows.Forms.Button();
            this.buttonUserFolderRemove = new System.Windows.Forms.Button();
            this.buttonUserFolderAdd = new System.Windows.Forms.Button();
            this.labelUserFolder = new System.Windows.Forms.Label();
            this.comboBoxUserFolder = new System.Windows.Forms.ComboBox();
            this.labelOperationsTitle = new System.Windows.Forms.Label();
            this.labelOperations = new System.Windows.Forms.Label();
            this.progressBarOperations = new System.Windows.Forms.ProgressBar();
            this.menuStrip1.SuspendLayout();
            this.groupBoxInstance.SuspendLayout();
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
            // buttonApplyAllChanges
            // 
            this.buttonApplyAllChanges.Location = new System.Drawing.Point(10, 296);
            this.buttonApplyAllChanges.Name = "buttonApplyAllChanges";
            this.buttonApplyAllChanges.Size = new System.Drawing.Size(582, 34);
            this.buttonApplyAllChanges.TabIndex = 1;
            this.buttonApplyAllChanges.Text = "Apply all";
            this.buttonApplyAllChanges.UseVisualStyleBackColor = true;
            this.buttonApplyAllChanges.Click += new System.EventHandler(this.buttonApplyAllChanges_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(4, 2, 0, 2);
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
            // textBoxOutputPane
            // 
            this.textBoxOutputPane.Location = new System.Drawing.Point(12, 405);
            this.textBoxOutputPane.Multiline = true;
            this.textBoxOutputPane.Name = "textBoxOutputPane";
            this.textBoxOutputPane.Size = new System.Drawing.Size(581, 80);
            this.textBoxOutputPane.TabIndex = 4;
            // 
            // labelInstancesTitle
            // 
            this.labelInstancesTitle.AutoSize = true;
            this.labelInstancesTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelInstancesTitle.Location = new System.Drawing.Point(12, 93);
            this.labelInstancesTitle.Name = "labelInstancesTitle";
            this.labelInstancesTitle.Size = new System.Drawing.Size(62, 13);
            this.labelInstancesTitle.TabIndex = 5;
            this.labelInstancesTitle.Text = "Instances";
            // 
            // comboBoxInstances
            // 
            this.comboBoxInstances.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxInstances.FormattingEnabled = true;
            this.comboBoxInstances.Location = new System.Drawing.Point(12, 115);
            this.comboBoxInstances.Name = "comboBoxInstances";
            this.comboBoxInstances.Size = new System.Drawing.Size(408, 21);
            this.comboBoxInstances.TabIndex = 6;
            this.comboBoxInstances.SelectedIndexChanged += new System.EventHandler(this.comboBoxInstances_SelectedIndexChanged);
            // 
            // buttonInstanceAdd
            // 
            this.buttonInstanceAdd.Location = new System.Drawing.Point(461, 115);
            this.buttonInstanceAdd.Name = "buttonInstanceAdd";
            this.buttonInstanceAdd.Size = new System.Drawing.Size(28, 23);
            this.buttonInstanceAdd.TabIndex = 8;
            this.buttonInstanceAdd.Text = "+";
            this.buttonInstanceAdd.UseVisualStyleBackColor = true;
            // 
            // buttonInstance
            // 
            this.buttonInstance.Location = new System.Drawing.Point(495, 115);
            this.buttonInstance.Name = "buttonInstance";
            this.buttonInstance.Size = new System.Drawing.Size(28, 23);
            this.buttonInstance.TabIndex = 9;
            this.buttonInstance.Text = "‒";
            this.buttonInstance.UseVisualStyleBackColor = true;
            // 
            // buttonInstanceUp
            // 
            this.buttonInstanceUp.Location = new System.Drawing.Point(530, 115);
            this.buttonInstanceUp.Name = "buttonInstanceUp";
            this.buttonInstanceUp.Size = new System.Drawing.Size(28, 23);
            this.buttonInstanceUp.TabIndex = 10;
            this.buttonInstanceUp.Text = "↑";
            this.buttonInstanceUp.UseVisualStyleBackColor = true;
            this.buttonInstanceUp.Click += new System.EventHandler(this.buttonInstanceUp_Click);
            // 
            // buttonInstanceDown
            // 
            this.buttonInstanceDown.Location = new System.Drawing.Point(563, 115);
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
            this.labelInstanceConfigurator.Location = new System.Drawing.Point(12, 154);
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
            this.buttonEditInstanceName.Location = new System.Drawing.Point(428, 115);
            this.buttonEditInstanceName.Name = "buttonEditInstanceName";
            this.buttonEditInstanceName.Size = new System.Drawing.Size(28, 23);
            this.buttonEditInstanceName.TabIndex = 15;
            this.buttonEditInstanceName.Text = "✎";
            this.buttonEditInstanceName.UseVisualStyleBackColor = true;
            this.buttonEditInstanceName.Click += new System.EventHandler(this.buttonEditInstanceName_Click);
            // 
            // labelSelectedInstanceIsDefault
            // 
            this.labelSelectedInstanceIsDefault.AutoSize = true;
            this.labelSelectedInstanceIsDefault.Location = new System.Drawing.Point(15, 154);
            this.labelSelectedInstanceIsDefault.Name = "labelSelectedInstanceIsDefault";
            this.labelSelectedInstanceIsDefault.Size = new System.Drawing.Size(0, 13);
            this.labelSelectedInstanceIsDefault.TabIndex = 16;
            // 
            // groupBoxInstance
            // 
            this.groupBoxInstance.Controls.Add(this.buttonServerEdit);
            this.groupBoxInstance.Controls.Add(this.buttonServerRemove);
            this.groupBoxInstance.Controls.Add(this.buttonServerAdd);
            this.groupBoxInstance.Controls.Add(this.labelServer);
            this.groupBoxInstance.Controls.Add(this.comboBoxServer);
            this.groupBoxInstance.Controls.Add(this.buttonDatamoverIncomingFolderEdit);
            this.groupBoxInstance.Controls.Add(this.buttonDatamoverIncomingFolderRemove);
            this.groupBoxInstance.Controls.Add(this.buttonDatamoverIncomingFolderAdd);
            this.groupBoxInstance.Controls.Add(this.labelDatamoverIncomingFolder);
            this.groupBoxInstance.Controls.Add(this.comboBoxDatamoverIncomingFolder);
            this.groupBoxInstance.Controls.Add(this.buttonUserFolderEdit);
            this.groupBoxInstance.Controls.Add(this.buttonUserFolderRemove);
            this.groupBoxInstance.Controls.Add(this.buttonUserFolderAdd);
            this.groupBoxInstance.Controls.Add(this.labelUserFolder);
            this.groupBoxInstance.Controls.Add(this.comboBoxUserFolder);
            this.groupBoxInstance.Location = new System.Drawing.Point(12, 180);
            this.groupBoxInstance.Margin = new System.Windows.Forms.Padding(2);
            this.groupBoxInstance.Name = "groupBoxInstance";
            this.groupBoxInstance.Padding = new System.Windows.Forms.Padding(2);
            this.groupBoxInstance.Size = new System.Drawing.Size(580, 111);
            this.groupBoxInstance.TabIndex = 17;
            this.groupBoxInstance.TabStop = false;
            this.groupBoxInstance.Text = "Default";
            // 
            // buttonServerEdit
            // 
            this.buttonServerEdit.Location = new System.Drawing.Point(480, 78);
            this.buttonServerEdit.Margin = new System.Windows.Forms.Padding(2);
            this.buttonServerEdit.Name = "buttonServerEdit";
            this.buttonServerEdit.Size = new System.Drawing.Size(26, 21);
            this.buttonServerEdit.TabIndex = 9;
            this.buttonServerEdit.Text = "✎";
            this.buttonServerEdit.UseVisualStyleBackColor = true;
            this.buttonServerEdit.Click += new System.EventHandler(this.buttonServerEdit_Click);
            // 
            // buttonServerRemove
            // 
            this.buttonServerRemove.Location = new System.Drawing.Point(538, 78);
            this.buttonServerRemove.Margin = new System.Windows.Forms.Padding(2);
            this.buttonServerRemove.Name = "buttonServerRemove";
            this.buttonServerRemove.Size = new System.Drawing.Size(26, 21);
            this.buttonServerRemove.TabIndex = 11;
            this.buttonServerRemove.Text = "—";
            this.buttonServerRemove.UseVisualStyleBackColor = true;
            // 
            // buttonServerAdd
            // 
            this.buttonServerAdd.Location = new System.Drawing.Point(510, 78);
            this.buttonServerAdd.Margin = new System.Windows.Forms.Padding(2);
            this.buttonServerAdd.Name = "buttonServerAdd";
            this.buttonServerAdd.Size = new System.Drawing.Size(25, 21);
            this.buttonServerAdd.TabIndex = 10;
            this.buttonServerAdd.Text = "+";
            this.buttonServerAdd.UseVisualStyleBackColor = true;
            // 
            // labelServer
            // 
            this.labelServer.AutoSize = true;
            this.labelServer.Location = new System.Drawing.Point(5, 81);
            this.labelServer.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelServer.Name = "labelServer";
            this.labelServer.Size = new System.Drawing.Size(80, 13);
            this.labelServer.TabIndex = 11;
            this.labelServer.Text = "openBIS server";
            // 
            // comboBoxServer
            // 
            this.comboBoxServer.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxServer.FormattingEnabled = true;
            this.comboBoxServer.Location = new System.Drawing.Point(110, 78);
            this.comboBoxServer.Margin = new System.Windows.Forms.Padding(2);
            this.comboBoxServer.Name = "comboBoxServer";
            this.comboBoxServer.Size = new System.Drawing.Size(367, 21);
            this.comboBoxServer.TabIndex = 8;
            this.comboBoxServer.SelectedIndexChanged += new System.EventHandler(this.comboBoxServer_SelectedIndexChanged);
            // 
            // buttonDatamoverIncomingFolderEdit
            // 
            this.buttonDatamoverIncomingFolderEdit.Location = new System.Drawing.Point(480, 48);
            this.buttonDatamoverIncomingFolderEdit.Margin = new System.Windows.Forms.Padding(2);
            this.buttonDatamoverIncomingFolderEdit.Name = "buttonDatamoverIncomingFolderEdit";
            this.buttonDatamoverIncomingFolderEdit.Size = new System.Drawing.Size(26, 21);
            this.buttonDatamoverIncomingFolderEdit.TabIndex = 5;
            this.buttonDatamoverIncomingFolderEdit.Text = "✎";
            this.buttonDatamoverIncomingFolderEdit.UseVisualStyleBackColor = true;
            this.buttonDatamoverIncomingFolderEdit.Click += new System.EventHandler(this.buttonDatamoverIncomingFolderEdit_Click);
            // 
            // buttonDatamoverIncomingFolderRemove
            // 
            this.buttonDatamoverIncomingFolderRemove.Location = new System.Drawing.Point(538, 48);
            this.buttonDatamoverIncomingFolderRemove.Margin = new System.Windows.Forms.Padding(2);
            this.buttonDatamoverIncomingFolderRemove.Name = "buttonDatamoverIncomingFolderRemove";
            this.buttonDatamoverIncomingFolderRemove.Size = new System.Drawing.Size(26, 21);
            this.buttonDatamoverIncomingFolderRemove.TabIndex = 7;
            this.buttonDatamoverIncomingFolderRemove.Text = "—";
            this.buttonDatamoverIncomingFolderRemove.UseVisualStyleBackColor = true;
            // 
            // buttonDatamoverIncomingFolderAdd
            // 
            this.buttonDatamoverIncomingFolderAdd.Location = new System.Drawing.Point(510, 48);
            this.buttonDatamoverIncomingFolderAdd.Margin = new System.Windows.Forms.Padding(2);
            this.buttonDatamoverIncomingFolderAdd.Name = "buttonDatamoverIncomingFolderAdd";
            this.buttonDatamoverIncomingFolderAdd.Size = new System.Drawing.Size(25, 21);
            this.buttonDatamoverIncomingFolderAdd.TabIndex = 6;
            this.buttonDatamoverIncomingFolderAdd.Text = "+";
            this.buttonDatamoverIncomingFolderAdd.UseVisualStyleBackColor = true;
            // 
            // labelDatamoverIncomingFolder
            // 
            this.labelDatamoverIncomingFolder.AutoSize = true;
            this.labelDatamoverIncomingFolder.Location = new System.Drawing.Point(5, 51);
            this.labelDatamoverIncomingFolder.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelDatamoverIncomingFolder.Name = "labelDatamoverIncomingFolder";
            this.labelDatamoverIncomingFolder.Size = new System.Drawing.Size(59, 13);
            this.labelDatamoverIncomingFolder.TabIndex = 6;
            this.labelDatamoverIncomingFolder.Text = "Datamover";
            // 
            // comboBoxDatamoverIncomingFolder
            // 
            this.comboBoxDatamoverIncomingFolder.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxDatamoverIncomingFolder.FormattingEnabled = true;
            this.comboBoxDatamoverIncomingFolder.Location = new System.Drawing.Point(110, 48);
            this.comboBoxDatamoverIncomingFolder.Margin = new System.Windows.Forms.Padding(2);
            this.comboBoxDatamoverIncomingFolder.Name = "comboBoxDatamoverIncomingFolder";
            this.comboBoxDatamoverIncomingFolder.Size = new System.Drawing.Size(367, 21);
            this.comboBoxDatamoverIncomingFolder.TabIndex = 4;
            this.comboBoxDatamoverIncomingFolder.SelectedIndexChanged += new System.EventHandler(this.comboBoxDatamoverIncomingFolder_SelectedIndexChanged);
            // 
            // buttonUserFolderEdit
            // 
            this.buttonUserFolderEdit.Location = new System.Drawing.Point(480, 18);
            this.buttonUserFolderEdit.Margin = new System.Windows.Forms.Padding(2);
            this.buttonUserFolderEdit.Name = "buttonUserFolderEdit";
            this.buttonUserFolderEdit.Size = new System.Drawing.Size(26, 21);
            this.buttonUserFolderEdit.TabIndex = 1;
            this.buttonUserFolderEdit.Text = "✎";
            this.buttonUserFolderEdit.UseVisualStyleBackColor = true;
            this.buttonUserFolderEdit.Click += new System.EventHandler(this.buttonUserFolderEdit_Click);
            // 
            // buttonUserFolderRemove
            // 
            this.buttonUserFolderRemove.Location = new System.Drawing.Point(538, 18);
            this.buttonUserFolderRemove.Margin = new System.Windows.Forms.Padding(2);
            this.buttonUserFolderRemove.Name = "buttonUserFolderRemove";
            this.buttonUserFolderRemove.Size = new System.Drawing.Size(26, 21);
            this.buttonUserFolderRemove.TabIndex = 3;
            this.buttonUserFolderRemove.Text = "—";
            this.buttonUserFolderRemove.UseVisualStyleBackColor = true;
            // 
            // buttonUserFolderAdd
            // 
            this.buttonUserFolderAdd.Location = new System.Drawing.Point(509, 18);
            this.buttonUserFolderAdd.Margin = new System.Windows.Forms.Padding(2);
            this.buttonUserFolderAdd.Name = "buttonUserFolderAdd";
            this.buttonUserFolderAdd.Size = new System.Drawing.Size(26, 21);
            this.buttonUserFolderAdd.TabIndex = 2;
            this.buttonUserFolderAdd.Text = "+";
            this.buttonUserFolderAdd.UseVisualStyleBackColor = true;
            // 
            // labelUserFolder
            // 
            this.labelUserFolder.AutoSize = true;
            this.labelUserFolder.Location = new System.Drawing.Point(5, 21);
            this.labelUserFolder.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelUserFolder.Name = "labelUserFolder";
            this.labelUserFolder.Size = new System.Drawing.Size(82, 13);
            this.labelUserFolder.TabIndex = 1;
            this.labelUserFolder.Text = "Annotation Tool";
            // 
            // comboBoxUserFolder
            // 
            this.comboBoxUserFolder.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxUserFolder.FormattingEnabled = true;
            this.comboBoxUserFolder.Location = new System.Drawing.Point(110, 18);
            this.comboBoxUserFolder.Margin = new System.Windows.Forms.Padding(2);
            this.comboBoxUserFolder.Name = "comboBoxUserFolder";
            this.comboBoxUserFolder.Size = new System.Drawing.Size(367, 21);
            this.comboBoxUserFolder.TabIndex = 0;
            this.comboBoxUserFolder.SelectedIndexChanged += new System.EventHandler(this.comboBoxUserFolder_SelectedIndexChanged);
            // 
            // labelOperationsTitle
            // 
            this.labelOperationsTitle.AutoSize = true;
            this.labelOperationsTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelOperationsTitle.Location = new System.Drawing.Point(12, 333);
            this.labelOperationsTitle.Name = "labelOperationsTitle";
            this.labelOperationsTitle.Size = new System.Drawing.Size(68, 13);
            this.labelOperationsTitle.TabIndex = 18;
            this.labelOperationsTitle.Text = "Operations";
            // 
            // labelOperations
            // 
            this.labelOperations.BackColor = System.Drawing.SystemColors.ControlLight;
            this.labelOperations.Location = new System.Drawing.Point(14, 350);
            this.labelOperations.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelOperations.Name = "labelOperations";
            this.labelOperations.Size = new System.Drawing.Size(578, 19);
            this.labelOperations.TabIndex = 19;
            this.labelOperations.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // progressBarOperations
            // 
            this.progressBarOperations.Location = new System.Drawing.Point(12, 381);
            this.progressBarOperations.Margin = new System.Windows.Forms.Padding(2);
            this.progressBarOperations.Name = "progressBarOperations";
            this.progressBarOperations.Size = new System.Drawing.Size(579, 19);
            this.progressBarOperations.TabIndex = 20;
            // 
            // obit_manager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(604, 497);
            this.Controls.Add(this.progressBarOperations);
            this.Controls.Add(this.labelOperations);
            this.Controls.Add(this.labelOperationsTitle);
            this.Controls.Add(this.groupBoxInstance);
            this.Controls.Add(this.labelSelectedInstanceIsDefault);
            this.Controls.Add(this.buttonEditInstanceName);
            this.Controls.Add(this.labeloBITInstallDir);
            this.Controls.Add(this.labelInstanceConfigurator);
            this.Controls.Add(this.buttonInstanceDown);
            this.Controls.Add(this.buttonInstanceUp);
            this.Controls.Add(this.buttonInstance);
            this.Controls.Add(this.buttonInstanceAdd);
            this.Controls.Add(this.comboBoxInstances);
            this.Controls.Add(this.labelInstancesTitle);
            this.Controls.Add(this.textBoxOutputPane);
            this.Controls.Add(this.buttonApplyAllChanges);
            this.Controls.Add(this.buttonOBITInstallationDirectory);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "obit_manager";
            this.Text = " ";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.groupBoxInstance.ResumeLayout(false);
            this.groupBoxInstance.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.NotifyIcon obitManagerNotifyIcon;
        private System.Windows.Forms.Button buttonOBITInstallationDirectory;
        private System.Windows.Forms.Button buttonApplyAllChanges;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem downloadOnlyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem download32bitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem download64bitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem settingsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loadToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.TextBox textBoxOutputPane;
        private System.Windows.Forms.ToolStripMenuItem checkForUpdatesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem viewLogToolStripMenuItem;
        private System.Windows.Forms.Label labelInstancesTitle;
        private System.Windows.Forms.ComboBox comboBoxInstances;
        private System.Windows.Forms.Button buttonInstanceAdd;
        private System.Windows.Forms.Button buttonInstance;
        private System.Windows.Forms.Button buttonInstanceUp;
        private System.Windows.Forms.Button buttonInstanceDown;
        private System.Windows.Forms.Label labelInstanceConfigurator;
        private System.Windows.Forms.Label labeloBITInstallDir;
        private System.Windows.Forms.Button buttonEditInstanceName;
        private System.Windows.Forms.Label labelSelectedInstanceIsDefault;
        private System.Windows.Forms.GroupBox groupBoxInstance;
        private System.Windows.Forms.Button buttonServerEdit;
        private System.Windows.Forms.Button buttonServerRemove;
        private System.Windows.Forms.Button buttonServerAdd;
        private System.Windows.Forms.Label labelServer;
        private System.Windows.Forms.ComboBox comboBoxServer;
        private System.Windows.Forms.Button buttonDatamoverIncomingFolderEdit;
        private System.Windows.Forms.Button buttonDatamoverIncomingFolderRemove;
        private System.Windows.Forms.Button buttonDatamoverIncomingFolderAdd;
        private System.Windows.Forms.Label labelDatamoverIncomingFolder;
        private System.Windows.Forms.ComboBox comboBoxDatamoverIncomingFolder;
        private System.Windows.Forms.Button buttonUserFolderEdit;
        private System.Windows.Forms.Button buttonUserFolderRemove;
        private System.Windows.Forms.Button buttonUserFolderAdd;
        private System.Windows.Forms.Label labelUserFolder;
        private System.Windows.Forms.ComboBox comboBoxUserFolder;
        private System.Windows.Forms.Label labelOperationsTitle;
        private System.Windows.Forms.Label labelOperations;
        private System.Windows.Forms.ProgressBar progressBarOperations;
    }
}

