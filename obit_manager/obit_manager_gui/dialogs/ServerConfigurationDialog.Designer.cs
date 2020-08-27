namespace obit_manager_gui.dialogs
{
    partial class ServerConfigurationDialog
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
            this.labelOpenBISServerHostName = new System.Windows.Forms.Label();
            this.textBoxOpenBISServerHostName = new System.Windows.Forms.TextBox();
            this.labelOpenBISServerPort = new System.Windows.Forms.Label();
            this.textBoxOpenBISServerPort = new System.Windows.Forms.TextBox();
            this.checkBoxOpenBISSErverAcceptSelfSignedCert = new System.Windows.Forms.CheckBox();
            this.textBoxDSSHostName = new System.Windows.Forms.TextBox();
            this.labelDSSHostName = new System.Windows.Forms.Label();
            this.labelDSSUnixUser = new System.Windows.Forms.Label();
            this.textBoxDSSUnixUser = new System.Windows.Forms.TextBox();
            this.buttonUseCryptoKey = new System.Windows.Forms.Button();
            this.buttonGenerateCryptoKey = new System.Windows.Forms.Button();
            this.groupBoxOpenBISServer = new System.Windows.Forms.GroupBox();
            this.comboBoxOpenBISServerProtocol = new System.Windows.Forms.ComboBox();
            this.labelProtocol = new System.Windows.Forms.Label();
            this.groupBoxDSS = new System.Windows.Forms.GroupBox();
            this.labelHardwareDescription = new System.Windows.Forms.Label();
            this.comboBoxHardwareSubCategory = new System.Windows.Forms.ComboBox();
            this.labelHardwareSubCategory = new System.Windows.Forms.Label();
            this.comboBoxHardwareCategory = new System.Windows.Forms.ComboBox();
            this.labelHardwareCategory = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.labelDSSCryptoKey = new System.Windows.Forms.Label();
            this.labelLPathLastChanged = new System.Windows.Forms.Label();
            this.labelDropboxPath = new System.Windows.Forms.Label();
            this.textBoxDSSRootDropboxFolder = new System.Windows.Forms.TextBox();
            this.textBoxDSSPathToLastChangedExecutable = new System.Windows.Forms.TextBox();
            this.buttonAccept = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.groupBoxOpenBISServer.SuspendLayout();
            this.groupBoxDSS.SuspendLayout();
            this.SuspendLayout();
            // 
            // labelOpenBISServerHostName
            // 
            this.labelOpenBISServerHostName.AutoSize = true;
            this.labelOpenBISServerHostName.Location = new System.Drawing.Point(102, 24);
            this.labelOpenBISServerHostName.Name = "labelOpenBISServerHostName";
            this.labelOpenBISServerHostName.Size = new System.Drawing.Size(59, 13);
            this.labelOpenBISServerHostName.TabIndex = 0;
            this.labelOpenBISServerHostName.Text = "Hostname";
            // 
            // textBoxOpenBISServerHostName
            // 
            this.textBoxOpenBISServerHostName.Location = new System.Drawing.Point(102, 44);
            this.textBoxOpenBISServerHostName.Name = "textBoxOpenBISServerHostName";
            this.textBoxOpenBISServerHostName.Size = new System.Drawing.Size(311, 22);
            this.textBoxOpenBISServerHostName.TabIndex = 1;
            this.textBoxOpenBISServerHostName.TextChanged += new System.EventHandler(this.textBoxOpenBISServerHostName_TextChanged);
            // 
            // labelOpenBISServerPort
            // 
            this.labelOpenBISServerPort.AutoSize = true;
            this.labelOpenBISServerPort.Location = new System.Drawing.Point(6, 77);
            this.labelOpenBISServerPort.Name = "labelOpenBISServerPort";
            this.labelOpenBISServerPort.Size = new System.Drawing.Size(81, 13);
            this.labelOpenBISServerPort.TabIndex = 2;
            this.labelOpenBISServerPort.Text = "Port (optional)";
            // 
            // textBoxOpenBISServerPort
            // 
            this.textBoxOpenBISServerPort.Location = new System.Drawing.Point(102, 72);
            this.textBoxOpenBISServerPort.Name = "textBoxOpenBISServerPort";
            this.textBoxOpenBISServerPort.Size = new System.Drawing.Size(63, 22);
            this.textBoxOpenBISServerPort.TabIndex = 2;
            this.textBoxOpenBISServerPort.TextChanged += new System.EventHandler(this.textBoxOpenBISServerPort_TextChanged);
            // 
            // checkBoxOpenBISSErverAcceptSelfSignedCert
            // 
            this.checkBoxOpenBISSErverAcceptSelfSignedCert.AutoSize = true;
            this.checkBoxOpenBISSErverAcceptSelfSignedCert.Location = new System.Drawing.Point(235, 75);
            this.checkBoxOpenBISSErverAcceptSelfSignedCert.Name = "checkBoxOpenBISSErverAcceptSelfSignedCert";
            this.checkBoxOpenBISSErverAcceptSelfSignedCert.Size = new System.Drawing.Size(178, 17);
            this.checkBoxOpenBISSErverAcceptSelfSignedCert.TabIndex = 3;
            this.checkBoxOpenBISSErverAcceptSelfSignedCert.Text = "Accept self-signed certificates";
            this.checkBoxOpenBISSErverAcceptSelfSignedCert.UseVisualStyleBackColor = true;
            this.checkBoxOpenBISSErverAcceptSelfSignedCert.CheckedChanged += new System.EventHandler(this.checkBoxOpenBISSErverAcceptSelfSignedCert_CheckedChanged);
            // 
            // textBoxDSSHostName
            // 
            this.textBoxDSSHostName.Location = new System.Drawing.Point(102, 43);
            this.textBoxDSSHostName.Name = "textBoxDSSHostName";
            this.textBoxDSSHostName.Size = new System.Drawing.Size(311, 22);
            this.textBoxDSSHostName.TabIndex = 5;
            this.textBoxDSSHostName.TextChanged += new System.EventHandler(this.textBoxDSSHostName_TextChanged);
            // 
            // labelDSSHostName
            // 
            this.labelDSSHostName.AutoSize = true;
            this.labelDSSHostName.Location = new System.Drawing.Point(102, 27);
            this.labelDSSHostName.Name = "labelDSSHostName";
            this.labelDSSHostName.Size = new System.Drawing.Size(62, 13);
            this.labelDSSHostName.TabIndex = 5;
            this.labelDSSHostName.Text = "Host name";
            // 
            // labelDSSUnixUser
            // 
            this.labelDSSUnixUser.AutoSize = true;
            this.labelDSSUnixUser.Location = new System.Drawing.Point(6, 27);
            this.labelDSSUnixUser.Name = "labelDSSUnixUser";
            this.labelDSSUnixUser.Size = new System.Drawing.Size(55, 13);
            this.labelDSSUnixUser.TabIndex = 7;
            this.labelDSSUnixUser.Text = "Unix user";
            // 
            // textBoxDSSUnixUser
            // 
            this.textBoxDSSUnixUser.Location = new System.Drawing.Point(6, 43);
            this.textBoxDSSUnixUser.Name = "textBoxDSSUnixUser";
            this.textBoxDSSUnixUser.Size = new System.Drawing.Size(88, 22);
            this.textBoxDSSUnixUser.TabIndex = 4;
            this.textBoxDSSUnixUser.TextChanged += new System.EventHandler(this.textBoxDSSUnixUser_TextChanged);
            // 
            // buttonUseCryptoKey
            // 
            this.buttonUseCryptoKey.Font = new System.Drawing.Font("Segoe UI", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonUseCryptoKey.Location = new System.Drawing.Point(303, 90);
            this.buttonUseCryptoKey.Name = "buttonUseCryptoKey";
            this.buttonUseCryptoKey.Size = new System.Drawing.Size(52, 25);
            this.buttonUseCryptoKey.TabIndex = 7;
            this.buttonUseCryptoKey.Text = "Select";
            this.buttonUseCryptoKey.UseVisualStyleBackColor = true;
            this.buttonUseCryptoKey.Click += new System.EventHandler(this.buttonUseCryptoKey_Click);
            // 
            // buttonGenerateCryptoKey
            // 
            this.buttonGenerateCryptoKey.Font = new System.Drawing.Font("Segoe UI", 7F);
            this.buttonGenerateCryptoKey.Location = new System.Drawing.Point(361, 90);
            this.buttonGenerateCryptoKey.Name = "buttonGenerateCryptoKey";
            this.buttonGenerateCryptoKey.Size = new System.Drawing.Size(52, 25);
            this.buttonGenerateCryptoKey.TabIndex = 8;
            this.buttonGenerateCryptoKey.Text = "Generate";
            this.buttonGenerateCryptoKey.UseVisualStyleBackColor = true;
            this.buttonGenerateCryptoKey.Click += new System.EventHandler(this.buttonGenerateCryptoKey_Click);
            // 
            // groupBoxOpenBISServer
            // 
            this.groupBoxOpenBISServer.Controls.Add(this.comboBoxOpenBISServerProtocol);
            this.groupBoxOpenBISServer.Controls.Add(this.labelProtocol);
            this.groupBoxOpenBISServer.Controls.Add(this.labelOpenBISServerHostName);
            this.groupBoxOpenBISServer.Controls.Add(this.textBoxOpenBISServerHostName);
            this.groupBoxOpenBISServer.Controls.Add(this.labelOpenBISServerPort);
            this.groupBoxOpenBISServer.Controls.Add(this.textBoxOpenBISServerPort);
            this.groupBoxOpenBISServer.Controls.Add(this.checkBoxOpenBISSErverAcceptSelfSignedCert);
            this.groupBoxOpenBISServer.Location = new System.Drawing.Point(12, 12);
            this.groupBoxOpenBISServer.Name = "groupBoxOpenBISServer";
            this.groupBoxOpenBISServer.Size = new System.Drawing.Size(424, 108);
            this.groupBoxOpenBISServer.TabIndex = 11;
            this.groupBoxOpenBISServer.TabStop = false;
            this.groupBoxOpenBISServer.Text = "openBIS Server";
            // 
            // comboBoxOpenBISServerProtocol
            // 
            this.comboBoxOpenBISServerProtocol.FormattingEnabled = true;
            this.comboBoxOpenBISServerProtocol.Items.AddRange(new object[] {
            "https",
            "http"});
            this.comboBoxOpenBISServerProtocol.Location = new System.Drawing.Point(6, 45);
            this.comboBoxOpenBISServerProtocol.Name = "comboBoxOpenBISServerProtocol";
            this.comboBoxOpenBISServerProtocol.Size = new System.Drawing.Size(88, 21);
            this.comboBoxOpenBISServerProtocol.TabIndex = 0;
            this.comboBoxOpenBISServerProtocol.SelectedIndexChanged += new System.EventHandler(this.comboBoxOpenBISServerProtocol_SelectedIndexChanged);
            // 
            // labelProtocol
            // 
            this.labelProtocol.AutoSize = true;
            this.labelProtocol.Location = new System.Drawing.Point(6, 24);
            this.labelProtocol.Name = "labelProtocol";
            this.labelProtocol.Size = new System.Drawing.Size(50, 13);
            this.labelProtocol.TabIndex = 5;
            this.labelProtocol.Text = "Protocol";
            // 
            // groupBoxDSS
            // 
            this.groupBoxDSS.Controls.Add(this.labelHardwareDescription);
            this.groupBoxDSS.Controls.Add(this.comboBoxHardwareSubCategory);
            this.groupBoxDSS.Controls.Add(this.labelHardwareSubCategory);
            this.groupBoxDSS.Controls.Add(this.comboBoxHardwareCategory);
            this.groupBoxDSS.Controls.Add(this.labelHardwareCategory);
            this.groupBoxDSS.Controls.Add(this.textBox1);
            this.groupBoxDSS.Controls.Add(this.labelDSSCryptoKey);
            this.groupBoxDSS.Controls.Add(this.labelLPathLastChanged);
            this.groupBoxDSS.Controls.Add(this.labelDropboxPath);
            this.groupBoxDSS.Controls.Add(this.textBoxDSSRootDropboxFolder);
            this.groupBoxDSS.Controls.Add(this.textBoxDSSPathToLastChangedExecutable);
            this.groupBoxDSS.Controls.Add(this.textBoxDSSHostName);
            this.groupBoxDSS.Controls.Add(this.labelDSSHostName);
            this.groupBoxDSS.Controls.Add(this.buttonGenerateCryptoKey);
            this.groupBoxDSS.Controls.Add(this.textBoxDSSUnixUser);
            this.groupBoxDSS.Controls.Add(this.buttonUseCryptoKey);
            this.groupBoxDSS.Controls.Add(this.labelDSSUnixUser);
            this.groupBoxDSS.Location = new System.Drawing.Point(12, 126);
            this.groupBoxDSS.Name = "groupBoxDSS";
            this.groupBoxDSS.Size = new System.Drawing.Size(424, 309);
            this.groupBoxDSS.TabIndex = 12;
            this.groupBoxDSS.TabStop = false;
            this.groupBoxDSS.Text = "Data Store Server";
            // 
            // labelHardwareDescription
            // 
            this.labelHardwareDescription.AutoSize = true;
            this.labelHardwareDescription.Location = new System.Drawing.Point(6, 216);
            this.labelHardwareDescription.Name = "labelHardwareDescription";
            this.labelHardwareDescription.Size = new System.Drawing.Size(16, 13);
            this.labelHardwareDescription.TabIndex = 20;
            this.labelHardwareDescription.Text = "...";
            // 
            // comboBoxHardwareSubCategory
            // 
            this.comboBoxHardwareSubCategory.FormattingEnabled = true;
            this.comboBoxHardwareSubCategory.Items.AddRange(new object[] {
            "BC CytoFlexS",
            "BC MoFlo XDP",
            "BD FACS Aria",
            "BD Influx",
            "BD LSR Fortessa",
            "Bio-Rad S3e",
            "Sony MA900",
            "Sony SH800S"});
            this.comboBoxHardwareSubCategory.Location = new System.Drawing.Point(171, 188);
            this.comboBoxHardwareSubCategory.Name = "comboBoxHardwareSubCategory";
            this.comboBoxHardwareSubCategory.Size = new System.Drawing.Size(242, 21);
            this.comboBoxHardwareSubCategory.TabIndex = 19;
            this.comboBoxHardwareSubCategory.SelectedIndexChanged += new System.EventHandler(this.comboBoxHardwareSubCategory_SelectedIndexChanged);
            // 
            // labelHardwareSubCategory
            // 
            this.labelHardwareSubCategory.AutoSize = true;
            this.labelHardwareSubCategory.Location = new System.Drawing.Point(171, 170);
            this.labelHardwareSubCategory.Name = "labelHardwareSubCategory";
            this.labelHardwareSubCategory.Size = new System.Drawing.Size(103, 13);
            this.labelHardwareSubCategory.TabIndex = 18;
            this.labelHardwareSubCategory.Text = "Hardware specifics";
            // 
            // comboBoxHardwareCategory
            // 
            this.comboBoxHardwareCategory.FormattingEnabled = true;
            this.comboBoxHardwareCategory.Items.AddRange(new object[] {
            "Microscopy",
            "Flow cytometry"});
            this.comboBoxHardwareCategory.Location = new System.Drawing.Point(6, 188);
            this.comboBoxHardwareCategory.Name = "comboBoxHardwareCategory";
            this.comboBoxHardwareCategory.Size = new System.Drawing.Size(159, 21);
            this.comboBoxHardwareCategory.TabIndex = 6;
            this.comboBoxHardwareCategory.SelectedIndexChanged += new System.EventHandler(this.comboBoxHardwareCategory_SelectedIndexChanged);
            // 
            // labelHardwareCategory
            // 
            this.labelHardwareCategory.AutoSize = true;
            this.labelHardwareCategory.Location = new System.Drawing.Point(6, 170);
            this.labelHardwareCategory.Name = "labelHardwareCategory";
            this.labelHardwareCategory.Size = new System.Drawing.Size(104, 13);
            this.labelHardwareCategory.TabIndex = 17;
            this.labelHardwareCategory.Text = "Hardware category";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(6, 91);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(291, 22);
            this.textBox1.TabIndex = 6;
            // 
            // labelDSSCryptoKey
            // 
            this.labelDSSCryptoKey.AutoSize = true;
            this.labelDSSCryptoKey.Location = new System.Drawing.Point(6, 75);
            this.labelDSSCryptoKey.Name = "labelDSSCryptoKey";
            this.labelDSSCryptoKey.Size = new System.Drawing.Size(100, 13);
            this.labelDSSCryptoKey.TabIndex = 16;
            this.labelDSSCryptoKey.Text = "Cryptographic key";
            // 
            // labelLPathLastChanged
            // 
            this.labelLPathLastChanged.AutoSize = true;
            this.labelLPathLastChanged.Location = new System.Drawing.Point(6, 240);
            this.labelLPathLastChanged.Name = "labelLPathLastChanged";
            this.labelLPathLastChanged.Size = new System.Drawing.Size(241, 13);
            this.labelLPathLastChanged.TabIndex = 15;
            this.labelLPathLastChanged.Text = "Path to the lastchanged executable (optional)";
            // 
            // labelDropboxPath
            // 
            this.labelDropboxPath.AutoSize = true;
            this.labelDropboxPath.Location = new System.Drawing.Point(6, 125);
            this.labelDropboxPath.Name = "labelDropboxPath";
            this.labelDropboxPath.Size = new System.Drawing.Size(170, 13);
            this.labelDropboxPath.TabIndex = 14;
            this.labelDropboxPath.Text = "Path to the root dropbox folder";
            // 
            // textBoxDSSRootDropboxFolder
            // 
            this.textBoxDSSRootDropboxFolder.Location = new System.Drawing.Point(6, 141);
            this.textBoxDSSRootDropboxFolder.Name = "textBoxDSSRootDropboxFolder";
            this.textBoxDSSRootDropboxFolder.Size = new System.Drawing.Size(407, 22);
            this.textBoxDSSRootDropboxFolder.TabIndex = 9;
            this.textBoxDSSRootDropboxFolder.TextChanged += new System.EventHandler(this.textBoxDSSRootDropboxFolder_TextChanged);
            // 
            // textBoxDSSPathToLastChangedExecutable
            // 
            this.textBoxDSSPathToLastChangedExecutable.Location = new System.Drawing.Point(6, 260);
            this.textBoxDSSPathToLastChangedExecutable.Name = "textBoxDSSPathToLastChangedExecutable";
            this.textBoxDSSPathToLastChangedExecutable.Size = new System.Drawing.Size(407, 22);
            this.textBoxDSSPathToLastChangedExecutable.TabIndex = 10;
            this.textBoxDSSPathToLastChangedExecutable.TextChanged += new System.EventHandler(this.textBoxDSSPathToLastChangedExecutable_TextChanged);
            // 
            // buttonAccept
            // 
            this.buttonAccept.Location = new System.Drawing.Point(12, 441);
            this.buttonAccept.Name = "buttonAccept";
            this.buttonAccept.Size = new System.Drawing.Size(204, 23);
            this.buttonAccept.TabIndex = 11;
            this.buttonAccept.Text = "Ok";
            this.buttonAccept.UseVisualStyleBackColor = true;
            this.buttonAccept.Click += new System.EventHandler(this.buttonAccept_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Location = new System.Drawing.Point(232, 441);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(204, 23);
            this.buttonCancel.TabIndex = 12;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // ServerConfigurationDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(444, 485);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonAccept);
            this.Controls.Add(this.groupBoxDSS);
            this.Controls.Add(this.groupBoxOpenBISServer);
            this.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "ServerConfigurationDialog";
            this.Text = "Edit openBIS Server configuration";
            this.groupBoxOpenBISServer.ResumeLayout(false);
            this.groupBoxOpenBISServer.PerformLayout();
            this.groupBoxDSS.ResumeLayout(false);
            this.groupBoxDSS.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label labelOpenBISServerHostName;
        private System.Windows.Forms.TextBox textBoxOpenBISServerHostName;
        private System.Windows.Forms.Label labelOpenBISServerPort;
        private System.Windows.Forms.TextBox textBoxOpenBISServerPort;
        private System.Windows.Forms.CheckBox checkBoxOpenBISSErverAcceptSelfSignedCert;
        private System.Windows.Forms.TextBox textBoxDSSHostName;
        private System.Windows.Forms.Label labelDSSHostName;
        private System.Windows.Forms.Label labelDSSUnixUser;
        private System.Windows.Forms.TextBox textBoxDSSUnixUser;
        private System.Windows.Forms.Button buttonUseCryptoKey;
        private System.Windows.Forms.Button buttonGenerateCryptoKey;
        private System.Windows.Forms.GroupBox groupBoxOpenBISServer;
        private System.Windows.Forms.GroupBox groupBoxDSS;
        private System.Windows.Forms.TextBox textBoxDSSPathToLastChangedExecutable;
        private System.Windows.Forms.Label labelLPathLastChanged;
        private System.Windows.Forms.Label labelDropboxPath;
        private System.Windows.Forms.TextBox textBoxDSSRootDropboxFolder;
        private System.Windows.Forms.Button buttonAccept;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.ComboBox comboBoxOpenBISServerProtocol;
        private System.Windows.Forms.Label labelProtocol;
        private System.Windows.Forms.Label labelDSSCryptoKey;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.ComboBox comboBoxHardwareSubCategory;
        private System.Windows.Forms.Label labelHardwareSubCategory;
        private System.Windows.Forms.ComboBox comboBoxHardwareCategory;
        private System.Windows.Forms.Label labelHardwareCategory;
        private System.Windows.Forms.Label labelHardwareDescription;
    }
}