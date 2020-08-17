namespace obit_manager_gui.dialogs
{
    partial class DatamoverConfigurationDialog
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
            this.labelDatamoverServiceName = new System.Windows.Forms.Label();
            this.textBoxDatamoverServiceName = new System.Windows.Forms.TextBox();
            this.labelDatamoverServiceUser = new System.Windows.Forms.Label();
            this.textBoxDatamoverServiceUser = new System.Windows.Forms.TextBox();
            this.buttonDatamoverDataDir = new System.Windows.Forms.Button();
            this.labelDatamoverDataDir = new System.Windows.Forms.Label();
            this.buttonFindDatamoverServiceUser = new System.Windows.Forms.Button();
            this.buttonCreateDatamoverServiceUser = new System.Windows.Forms.Button();
            this.labelFolderExists = new System.Windows.Forms.Label();
            this.buttonCreateFolder = new System.Windows.Forms.Button();
            this.buttonSetPermissions = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // buttonCancel
            // 
            this.buttonCancel.Location = new System.Drawing.Point(233, 121);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(204, 23);
            this.buttonCancel.TabIndex = 14;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // buttonAccept
            // 
            this.buttonAccept.Location = new System.Drawing.Point(13, 121);
            this.buttonAccept.Name = "buttonAccept";
            this.buttonAccept.Size = new System.Drawing.Size(204, 23);
            this.buttonAccept.TabIndex = 13;
            this.buttonAccept.Text = "Ok";
            this.buttonAccept.UseVisualStyleBackColor = true;
            this.buttonAccept.Click += new System.EventHandler(this.buttonAccept_Click);
            // 
            // labelDatamoverServiceName
            // 
            this.labelDatamoverServiceName.AutoSize = true;
            this.labelDatamoverServiceName.Location = new System.Drawing.Point(12, 18);
            this.labelDatamoverServiceName.Name = "labelDatamoverServiceName";
            this.labelDatamoverServiceName.Size = new System.Drawing.Size(125, 13);
            this.labelDatamoverServiceName.TabIndex = 15;
            this.labelDatamoverServiceName.Text = "Datamover service name";
            // 
            // textBoxDatamoverServiceName
            // 
            this.textBoxDatamoverServiceName.Location = new System.Drawing.Point(143, 14);
            this.textBoxDatamoverServiceName.Name = "textBoxDatamoverServiceName";
            this.textBoxDatamoverServiceName.Size = new System.Drawing.Size(301, 20);
            this.textBoxDatamoverServiceName.TabIndex = 16;
            this.textBoxDatamoverServiceName.TextChanged += new System.EventHandler(this.textBoxDatamoverServiceName_TextChanged);
            // 
            // labelDatamoverServiceUser
            // 
            this.labelDatamoverServiceUser.AutoSize = true;
            this.labelDatamoverServiceUser.Location = new System.Drawing.Point(12, 44);
            this.labelDatamoverServiceUser.Name = "labelDatamoverServiceUser";
            this.labelDatamoverServiceUser.Size = new System.Drawing.Size(119, 13);
            this.labelDatamoverServiceUser.TabIndex = 17;
            this.labelDatamoverServiceUser.Text = "Datamover service user";
            // 
            // textBoxDatamoverServiceUser
            // 
            this.textBoxDatamoverServiceUser.Location = new System.Drawing.Point(143, 40);
            this.textBoxDatamoverServiceUser.Name = "textBoxDatamoverServiceUser";
            this.textBoxDatamoverServiceUser.Size = new System.Drawing.Size(189, 20);
            this.textBoxDatamoverServiceUser.TabIndex = 18;
            this.textBoxDatamoverServiceUser.TextChanged += new System.EventHandler(this.textBoxDatamoverServiceUser_TextChanged);
            // 
            // buttonDatamoverDataDir
            // 
            this.buttonDatamoverDataDir.Location = new System.Drawing.Point(143, 63);
            this.buttonDatamoverDataDir.Name = "buttonDatamoverDataDir";
            this.buttonDatamoverDataDir.Size = new System.Drawing.Size(301, 23);
            this.buttonDatamoverDataDir.TabIndex = 20;
            this.buttonDatamoverDataDir.Text = "...";
            this.buttonDatamoverDataDir.UseVisualStyleBackColor = true;
            this.buttonDatamoverDataDir.Click += new System.EventHandler(this.buttonDatamoverDataDir_Click);
            // 
            // labelDatamoverDataDir
            // 
            this.labelDatamoverDataDir.AutoSize = true;
            this.labelDatamoverDataDir.Location = new System.Drawing.Point(12, 70);
            this.labelDatamoverDataDir.Name = "labelDatamoverDataDir";
            this.labelDatamoverDataDir.Size = new System.Drawing.Size(112, 13);
            this.labelDatamoverDataDir.TabIndex = 19;
            this.labelDatamoverDataDir.Text = "Datamover data folder";
            // 
            // buttonFindDatamoverServiceUser
            // 
            this.buttonFindDatamoverServiceUser.Location = new System.Drawing.Point(338, 39);
            this.buttonFindDatamoverServiceUser.Name = "buttonFindDatamoverServiceUser";
            this.buttonFindDatamoverServiceUser.Size = new System.Drawing.Size(50, 23);
            this.buttonFindDatamoverServiceUser.TabIndex = 21;
            this.buttonFindDatamoverServiceUser.Text = "Find";
            this.buttonFindDatamoverServiceUser.UseVisualStyleBackColor = true;
            this.buttonFindDatamoverServiceUser.Click += new System.EventHandler(this.buttonDatamoverServiceUser_Click);
            // 
            // buttonCreateDatamoverServiceUser
            // 
            this.buttonCreateDatamoverServiceUser.Location = new System.Drawing.Point(394, 39);
            this.buttonCreateDatamoverServiceUser.Name = "buttonCreateDatamoverServiceUser";
            this.buttonCreateDatamoverServiceUser.Size = new System.Drawing.Size(50, 23);
            this.buttonCreateDatamoverServiceUser.TabIndex = 22;
            this.buttonCreateDatamoverServiceUser.Text = "Create";
            this.buttonCreateDatamoverServiceUser.UseVisualStyleBackColor = true;
            // 
            // labelFolderExists
            // 
            this.labelFolderExists.AutoSize = true;
            this.labelFolderExists.Location = new System.Drawing.Point(12, 97);
            this.labelFolderExists.Name = "labelFolderExists";
            this.labelFolderExists.Size = new System.Drawing.Size(130, 13);
            this.labelFolderExists.TabIndex = 23;
            this.labelFolderExists.Text = "Data folder does not exist.";
            // 
            // buttonCreateFolder
            // 
            this.buttonCreateFolder.Location = new System.Drawing.Point(226, 92);
            this.buttonCreateFolder.Name = "buttonCreateFolder";
            this.buttonCreateFolder.Size = new System.Drawing.Size(106, 23);
            this.buttonCreateFolder.TabIndex = 24;
            this.buttonCreateFolder.Text = "Create folder";
            this.buttonCreateFolder.UseVisualStyleBackColor = true;
            this.buttonCreateFolder.Click += new System.EventHandler(this.buttonCreateFolder_Click);
            // 
            // buttonSetPermissions
            // 
            this.buttonSetPermissions.Location = new System.Drawing.Point(338, 92);
            this.buttonSetPermissions.Name = "buttonSetPermissions";
            this.buttonSetPermissions.Size = new System.Drawing.Size(106, 23);
            this.buttonSetPermissions.TabIndex = 25;
            this.buttonSetPermissions.Text = "Set permissions";
            this.buttonSetPermissions.UseVisualStyleBackColor = true;
            this.buttonSetPermissions.Click += new System.EventHandler(this.buttonSetPermissions_Click);
            // 
            // DatamoverConfigurationDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(449, 152);
            this.Controls.Add(this.buttonSetPermissions);
            this.Controls.Add(this.buttonCreateFolder);
            this.Controls.Add(this.labelFolderExists);
            this.Controls.Add(this.buttonCreateDatamoverServiceUser);
            this.Controls.Add(this.buttonFindDatamoverServiceUser);
            this.Controls.Add(this.buttonDatamoverDataDir);
            this.Controls.Add(this.labelDatamoverDataDir);
            this.Controls.Add(this.labelDatamoverServiceUser);
            this.Controls.Add(this.textBoxDatamoverServiceUser);
            this.Controls.Add(this.labelDatamoverServiceName);
            this.Controls.Add(this.textBoxDatamoverServiceName);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonAccept);
            this.Name = "DatamoverConfigurationDialog";
            this.Text = "Edit Datamover configuration";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Button buttonAccept;
        private System.Windows.Forms.Label labelDatamoverServiceName;
        private System.Windows.Forms.TextBox textBoxDatamoverServiceName;
        private System.Windows.Forms.Label labelDatamoverServiceUser;
        private System.Windows.Forms.TextBox textBoxDatamoverServiceUser;
        private System.Windows.Forms.Button buttonDatamoverDataDir;
        private System.Windows.Forms.Label labelDatamoverDataDir;
        private System.Windows.Forms.Button buttonFindDatamoverServiceUser;
        private System.Windows.Forms.Button buttonCreateDatamoverServiceUser;
        private System.Windows.Forms.Label labelFolderExists;
        private System.Windows.Forms.Button buttonCreateFolder;
        private System.Windows.Forms.Button buttonSetPermissions;
    }
}