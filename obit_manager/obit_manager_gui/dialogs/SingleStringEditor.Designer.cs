﻿namespace obit_manager_gui.dialogs
{
    partial class SingleStringEditor
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
            this.labelString = new System.Windows.Forms.Label();
            this.textBoxString = new System.Windows.Forms.TextBox();
            this.buttonOk = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // labelString
            // 
            this.labelString.AutoSize = true;
            this.labelString.Location = new System.Drawing.Point(13, 13);
            this.labelString.Name = "labelString";
            this.labelString.Size = new System.Drawing.Size(33, 13);
            this.labelString.TabIndex = 0;
            this.labelString.Text = "Label";
            // 
            // textBoxString
            // 
            this.textBoxString.Location = new System.Drawing.Point(13, 30);
            this.textBoxString.Name = "textBoxString";
            this.textBoxString.Size = new System.Drawing.Size(312, 20);
            this.textBoxString.TabIndex = 1;
            this.textBoxString.TextChanged += new System.EventHandler(this.textBoxString_TextChanged);
            this.textBoxString.Validating += new System.ComponentModel.CancelEventHandler(this.textBoxString_Validating);
            this.textBoxString.Validated += new System.EventHandler(this.textBoxString_Validated);
            // 
            // buttonOk
            // 
            this.buttonOk.Location = new System.Drawing.Point(13, 57);
            this.buttonOk.Name = "buttonOk";
            this.buttonOk.Size = new System.Drawing.Size(153, 23);
            this.buttonOk.TabIndex = 2;
            this.buttonOk.Text = "Ok";
            this.buttonOk.UseVisualStyleBackColor = true;
            this.buttonOk.Click += new System.EventHandler(this.buttonOk_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Location = new System.Drawing.Point(172, 57);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(153, 23);
            this.buttonCancel.TabIndex = 3;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // SingleStringEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(337, 88);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonOk);
            this.Controls.Add(this.textBoxString);
            this.Controls.Add(this.labelString);
            this.Name = "SingleStringEditor";
            this.Text = "SingleStringEditor";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelString;
        private System.Windows.Forms.TextBox textBoxString;
        private System.Windows.Forms.Button buttonOk;
        private System.Windows.Forms.Button buttonCancel;
    }
}