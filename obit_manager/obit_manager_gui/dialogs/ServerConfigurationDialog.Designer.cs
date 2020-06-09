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
            this.labelOpenBISServerURL = new System.Windows.Forms.Label();
            this.textBoxOpenBISServerURL = new System.Windows.Forms.TextBox();
            this.labelOpenBISServerPort = new System.Windows.Forms.Label();
            this.textBoxOpenBISServerPort = new System.Windows.Forms.TextBox();
            this.checkBoxOpenBISSErverAcceptSelfSignedCert = new System.Windows.Forms.CheckBox();
            this.textBoxDSSHostName = new System.Windows.Forms.TextBox();
            this.labelDSSHostName = new System.Windows.Forms.Label();
            this.labelUnixDSSUser = new System.Windows.Forms.Label();
            this.textBoxUnixDSSUser = new System.Windows.Forms.TextBox();
            this.buttonUseCryptoKey = new System.Windows.Forms.Button();
            this.buttonGenerateCryptoKey = new System.Windows.Forms.Button();
            this.groupBoxOpenBISServer = new System.Windows.Forms.GroupBox();
            this.groupBoxDSS = new System.Windows.Forms.GroupBox();
            this.labelLPathLastChanged = new System.Windows.Forms.Label();
            this.labelDropboxPath = new System.Windows.Forms.Label();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.labelServerFriendlyName = new System.Windows.Forms.Label();
            this.textBoxServerFriendlyName = new System.Windows.Forms.TextBox();
            this.groupBoxOpenBISServer.SuspendLayout();
            this.groupBoxDSS.SuspendLayout();
            this.SuspendLayout();
            // 
            // labelOpenBISServerURL
            // 
            this.labelOpenBISServerURL.AutoSize = true;
            this.labelOpenBISServerURL.Location = new System.Drawing.Point(8, 27);
            this.labelOpenBISServerURL.Name = "labelOpenBISServerURL";
            this.labelOpenBISServerURL.Size = new System.Drawing.Size(27, 13);
            this.labelOpenBISServerURL.TabIndex = 0;
            this.labelOpenBISServerURL.Text = "URL";
            // 
            // textBoxOpenBISServerURL
            // 
            this.textBoxOpenBISServerURL.Location = new System.Drawing.Point(11, 44);
            this.textBoxOpenBISServerURL.Name = "textBoxOpenBISServerURL";
            this.textBoxOpenBISServerURL.Size = new System.Drawing.Size(253, 22);
            this.textBoxOpenBISServerURL.TabIndex = 1;
            // 
            // labelOpenBISServerPort
            // 
            this.labelOpenBISServerPort.AutoSize = true;
            this.labelOpenBISServerPort.Location = new System.Drawing.Point(270, 47);
            this.labelOpenBISServerPort.Name = "labelOpenBISServerPort";
            this.labelOpenBISServerPort.Size = new System.Drawing.Size(81, 13);
            this.labelOpenBISServerPort.TabIndex = 2;
            this.labelOpenBISServerPort.Text = "Port (optional)";
            // 
            // textBoxOpenBISServerPort
            // 
            this.textBoxOpenBISServerPort.Location = new System.Drawing.Point(352, 44);
            this.textBoxOpenBISServerPort.Name = "textBoxOpenBISServerPort";
            this.textBoxOpenBISServerPort.Size = new System.Drawing.Size(63, 22);
            this.textBoxOpenBISServerPort.TabIndex = 3;
            // 
            // checkBoxOpenBISSErverAcceptSelfSignedCert
            // 
            this.checkBoxOpenBISSErverAcceptSelfSignedCert.AutoSize = true;
            this.checkBoxOpenBISSErverAcceptSelfSignedCert.Location = new System.Drawing.Point(11, 72);
            this.checkBoxOpenBISSErverAcceptSelfSignedCert.Name = "checkBoxOpenBISSErverAcceptSelfSignedCert";
            this.checkBoxOpenBISSErverAcceptSelfSignedCert.Size = new System.Drawing.Size(178, 17);
            this.checkBoxOpenBISSErverAcceptSelfSignedCert.TabIndex = 4;
            this.checkBoxOpenBISSErverAcceptSelfSignedCert.Text = "Accept self-signed certificates";
            this.checkBoxOpenBISSErverAcceptSelfSignedCert.UseVisualStyleBackColor = true;
            // 
            // textBoxDSSHostName
            // 
            this.textBoxDSSHostName.Location = new System.Drawing.Point(8, 48);
            this.textBoxDSSHostName.Name = "textBoxDSSHostName";
            this.textBoxDSSHostName.Size = new System.Drawing.Size(407, 22);
            this.textBoxDSSHostName.TabIndex = 6;
            // 
            // labelDSSHostName
            // 
            this.labelDSSHostName.AutoSize = true;
            this.labelDSSHostName.Location = new System.Drawing.Point(5, 28);
            this.labelDSSHostName.Name = "labelDSSHostName";
            this.labelDSSHostName.Size = new System.Drawing.Size(62, 13);
            this.labelDSSHostName.TabIndex = 5;
            this.labelDSSHostName.Text = "Host name";
            // 
            // labelUnixDSSUser
            // 
            this.labelUnixDSSUser.AutoSize = true;
            this.labelUnixDSSUser.Location = new System.Drawing.Point(5, 77);
            this.labelUnixDSSUser.Name = "labelUnixDSSUser";
            this.labelUnixDSSUser.Size = new System.Drawing.Size(55, 13);
            this.labelUnixDSSUser.TabIndex = 7;
            this.labelUnixDSSUser.Text = "Unix user";
            // 
            // textBoxUnixDSSUser
            // 
            this.textBoxUnixDSSUser.Location = new System.Drawing.Point(8, 97);
            this.textBoxUnixDSSUser.Name = "textBoxUnixDSSUser";
            this.textBoxUnixDSSUser.Size = new System.Drawing.Size(155, 22);
            this.textBoxUnixDSSUser.TabIndex = 8;
            // 
            // buttonUseCryptoKey
            // 
            this.buttonUseCryptoKey.Font = new System.Drawing.Font("Segoe UI", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonUseCryptoKey.Location = new System.Drawing.Point(173, 95);
            this.buttonUseCryptoKey.Name = "buttonUseCryptoKey";
            this.buttonUseCryptoKey.Size = new System.Drawing.Size(117, 25);
            this.buttonUseCryptoKey.TabIndex = 9;
            this.buttonUseCryptoKey.Text = "Use crypto key";
            this.buttonUseCryptoKey.UseVisualStyleBackColor = true;
            // 
            // buttonGenerateCryptoKey
            // 
            this.buttonGenerateCryptoKey.Font = new System.Drawing.Font("Segoe UI", 7F);
            this.buttonGenerateCryptoKey.Location = new System.Drawing.Point(300, 95);
            this.buttonGenerateCryptoKey.Name = "buttonGenerateCryptoKey";
            this.buttonGenerateCryptoKey.Size = new System.Drawing.Size(115, 25);
            this.buttonGenerateCryptoKey.TabIndex = 10;
            this.buttonGenerateCryptoKey.Text = "Generate crypto key";
            this.buttonGenerateCryptoKey.UseVisualStyleBackColor = true;
            this.buttonGenerateCryptoKey.Click += new System.EventHandler(this.buttonGenerateCryptoKey_Click);
            // 
            // groupBoxOpenBISServer
            // 
            this.groupBoxOpenBISServer.Controls.Add(this.labelOpenBISServerURL);
            this.groupBoxOpenBISServer.Controls.Add(this.textBoxOpenBISServerURL);
            this.groupBoxOpenBISServer.Controls.Add(this.labelOpenBISServerPort);
            this.groupBoxOpenBISServer.Controls.Add(this.textBoxOpenBISServerPort);
            this.groupBoxOpenBISServer.Controls.Add(this.checkBoxOpenBISSErverAcceptSelfSignedCert);
            this.groupBoxOpenBISServer.Location = new System.Drawing.Point(12, 83);
            this.groupBoxOpenBISServer.Name = "groupBoxOpenBISServer";
            this.groupBoxOpenBISServer.Size = new System.Drawing.Size(424, 100);
            this.groupBoxOpenBISServer.TabIndex = 11;
            this.groupBoxOpenBISServer.TabStop = false;
            this.groupBoxOpenBISServer.Text = "openBIS Server";
            // 
            // groupBoxDSS
            // 
            this.groupBoxDSS.Controls.Add(this.labelLPathLastChanged);
            this.groupBoxDSS.Controls.Add(this.labelDropboxPath);
            this.groupBoxDSS.Controls.Add(this.textBox2);
            this.groupBoxDSS.Controls.Add(this.textBox1);
            this.groupBoxDSS.Controls.Add(this.textBoxDSSHostName);
            this.groupBoxDSS.Controls.Add(this.labelDSSHostName);
            this.groupBoxDSS.Controls.Add(this.buttonGenerateCryptoKey);
            this.groupBoxDSS.Controls.Add(this.textBoxUnixDSSUser);
            this.groupBoxDSS.Controls.Add(this.buttonUseCryptoKey);
            this.groupBoxDSS.Controls.Add(this.labelUnixDSSUser);
            this.groupBoxDSS.Location = new System.Drawing.Point(12, 189);
            this.groupBoxDSS.Name = "groupBoxDSS";
            this.groupBoxDSS.Size = new System.Drawing.Size(424, 228);
            this.groupBoxDSS.TabIndex = 12;
            this.groupBoxDSS.TabStop = false;
            this.groupBoxDSS.Text = "Data Store Server";
            // 
            // labelLPathLastChanged
            // 
            this.labelLPathLastChanged.AutoSize = true;
            this.labelLPathLastChanged.Location = new System.Drawing.Point(6, 175);
            this.labelLPathLastChanged.Name = "labelLPathLastChanged";
            this.labelLPathLastChanged.Size = new System.Drawing.Size(241, 13);
            this.labelLPathLastChanged.TabIndex = 15;
            this.labelLPathLastChanged.Text = "Path to the lastchanged executable (optional)";
            // 
            // labelDropboxPath
            // 
            this.labelDropboxPath.AutoSize = true;
            this.labelDropboxPath.Location = new System.Drawing.Point(5, 126);
            this.labelDropboxPath.Name = "labelDropboxPath";
            this.labelDropboxPath.Size = new System.Drawing.Size(170, 13);
            this.labelDropboxPath.TabIndex = 14;
            this.labelDropboxPath.Text = "Path to the root dropbox folder";
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(8, 146);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(407, 22);
            this.textBox2.TabIndex = 13;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(8, 195);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(407, 22);
            this.textBox1.TabIndex = 12;
            // 
            // labelServerFriendlyName
            // 
            this.labelServerFriendlyName.AutoSize = true;
            this.labelServerFriendlyName.Location = new System.Drawing.Point(190, 19);
            this.labelServerFriendlyName.Name = "labelServerFriendlyName";
            this.labelServerFriendlyName.Size = new System.Drawing.Size(69, 13);
            this.labelServerFriendlyName.TabIndex = 16;
            this.labelServerFriendlyName.Text = "Server name";
            // 
            // textBoxServerFriendlyName
            // 
            this.textBoxServerFriendlyName.Location = new System.Drawing.Point(12, 35);
            this.textBoxServerFriendlyName.Name = "textBoxServerFriendlyName";
            this.textBoxServerFriendlyName.Size = new System.Drawing.Size(424, 22);
            this.textBoxServerFriendlyName.TabIndex = 17;
            // 
            // ServerConfigurationDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(444, 424);
            this.Controls.Add(this.textBoxServerFriendlyName);
            this.Controls.Add(this.labelServerFriendlyName);
            this.Controls.Add(this.groupBoxDSS);
            this.Controls.Add(this.groupBoxOpenBISServer);
            this.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "ServerConfigurationDialog";
            this.Text = "Edit openBIS Server";
            this.groupBoxOpenBISServer.ResumeLayout(false);
            this.groupBoxOpenBISServer.PerformLayout();
            this.groupBoxDSS.ResumeLayout(false);
            this.groupBoxDSS.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelOpenBISServerURL;
        private System.Windows.Forms.TextBox textBoxOpenBISServerURL;
        private System.Windows.Forms.Label labelOpenBISServerPort;
        private System.Windows.Forms.TextBox textBoxOpenBISServerPort;
        private System.Windows.Forms.CheckBox checkBoxOpenBISSErverAcceptSelfSignedCert;
        private System.Windows.Forms.TextBox textBoxDSSHostName;
        private System.Windows.Forms.Label labelDSSHostName;
        private System.Windows.Forms.Label labelUnixDSSUser;
        private System.Windows.Forms.TextBox textBoxUnixDSSUser;
        private System.Windows.Forms.Button buttonUseCryptoKey;
        private System.Windows.Forms.Button buttonGenerateCryptoKey;
        private System.Windows.Forms.GroupBox groupBoxOpenBISServer;
        private System.Windows.Forms.GroupBox groupBoxDSS;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label labelLPathLastChanged;
        private System.Windows.Forms.Label labelDropboxPath;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Label labelServerFriendlyName;
        private System.Windows.Forms.TextBox textBoxServerFriendlyName;
    }
}