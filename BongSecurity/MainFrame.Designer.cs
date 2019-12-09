namespace BongSecurity
{
    partial class MainFrame
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainFrame));
            this.progressbar = new System.Windows.Forms.ProgressBar();
            this.gdTarget = new System.Windows.Forms.Label();
            this.localFolderTextBox = new System.Windows.Forms.TextBox();
            this.localFolderBtn = new System.Windows.Forms.Button();
            this.localFolderLabel = new System.Windows.Forms.Label();
            this.sourceFolderDeleteAfterCopy = new System.Windows.Forms.CheckBox();
            this.googleDriveLabel = new System.Windows.Forms.Label();
            this.googleDriveTextBox = new System.Windows.Forms.TextBox();
            this.sourceFolderDeleteAfterCopyToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.startBackupBtn = new System.Windows.Forms.Button();
            this.credentialLabel = new System.Windows.Forms.Label();
            this.credentialBtn = new System.Windows.Forms.Button();
            this.credentialTextBox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // progressbar
            // 
            this.progressbar.Enabled = false;
            this.progressbar.Location = new System.Drawing.Point(34, 178);
            this.progressbar.Name = "progressbar";
            this.progressbar.Size = new System.Drawing.Size(411, 21);
            this.progressbar.TabIndex = 0;
            // 
            // gdTarget
            // 
            this.gdTarget.AutoSize = true;
            this.gdTarget.Location = new System.Drawing.Point(34, 160);
            this.gdTarget.Name = "gdTarget";
            this.gdTarget.Size = new System.Drawing.Size(41, 12);
            this.gdTarget.TabIndex = 1;
            this.gdTarget.Text = "Ready";
            // 
            // localFolderTextBox
            // 
            this.localFolderTextBox.Location = new System.Drawing.Point(152, 88);
            this.localFolderTextBox.Name = "localFolderTextBox";
            this.localFolderTextBox.ReadOnly = true;
            this.localFolderTextBox.Size = new System.Drawing.Size(265, 21);
            this.localFolderTextBox.TabIndex = 5;
            // 
            // localFolderBtn
            // 
            this.localFolderBtn.Location = new System.Drawing.Point(423, 88);
            this.localFolderBtn.Name = "localFolderBtn";
            this.localFolderBtn.Size = new System.Drawing.Size(22, 23);
            this.localFolderBtn.TabIndex = 6;
            this.localFolderBtn.Text = "...";
            this.localFolderBtn.UseVisualStyleBackColor = true;
            this.localFolderBtn.Click += new System.EventHandler(this.button2_Click);
            // 
            // localFolderLabel
            // 
            this.localFolderLabel.Location = new System.Drawing.Point(34, 88);
            this.localFolderLabel.Name = "localFolderLabel";
            this.localFolderLabel.Size = new System.Drawing.Size(112, 23);
            this.localFolderLabel.TabIndex = 7;
            this.localFolderLabel.Text = "로컬 저장 경로";
            this.localFolderLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // sourceFolderDeleteAfterCopy
            // 
            this.sourceFolderDeleteAfterCopy.AutoSize = true;
            this.sourceFolderDeleteAfterCopy.Location = new System.Drawing.Point(34, 26);
            this.sourceFolderDeleteAfterCopy.Name = "sourceFolderDeleteAfterCopy";
            this.sourceFolderDeleteAfterCopy.Size = new System.Drawing.Size(15, 14);
            this.sourceFolderDeleteAfterCopy.TabIndex = 8;
            this.sourceFolderDeleteAfterCopy.UseVisualStyleBackColor = true;
            // 
            // googleDriveLabel
            // 
            this.googleDriveLabel.Location = new System.Drawing.Point(32, 111);
            this.googleDriveLabel.Name = "googleDriveLabel";
            this.googleDriveLabel.Size = new System.Drawing.Size(114, 21);
            this.googleDriveLabel.TabIndex = 11;
            this.googleDriveLabel.Text = "구글 드라이브 경로";
            this.googleDriveLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // googleDriveTextBox
            // 
            this.googleDriveTextBox.Location = new System.Drawing.Point(152, 111);
            this.googleDriveTextBox.Name = "googleDriveTextBox";
            this.googleDriveTextBox.Size = new System.Drawing.Size(160, 21);
            this.googleDriveTextBox.TabIndex = 9;
            // 
            // startBackupBtn
            // 
            this.startBackupBtn.Enabled = false;
            this.startBackupBtn.Location = new System.Drawing.Point(184, 224);
            this.startBackupBtn.Name = "startBackupBtn";
            this.startBackupBtn.Size = new System.Drawing.Size(117, 28);
            this.startBackupBtn.TabIndex = 12;
            this.startBackupBtn.Text = "Start";
            this.startBackupBtn.UseVisualStyleBackColor = true;
            this.startBackupBtn.Click += new System.EventHandler(this.button1_Click);
            // 
            // credentialLabel
            // 
            this.credentialLabel.Location = new System.Drawing.Point(34, 65);
            this.credentialLabel.Name = "credentialLabel";
            this.credentialLabel.Size = new System.Drawing.Size(112, 21);
            this.credentialLabel.TabIndex = 15;
            this.credentialLabel.Text = "인증파일";
            this.credentialLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // credentialBtn
            // 
            this.credentialBtn.Location = new System.Drawing.Point(423, 65);
            this.credentialBtn.Name = "credentialBtn";
            this.credentialBtn.Size = new System.Drawing.Size(22, 23);
            this.credentialBtn.TabIndex = 14;
            this.credentialBtn.Text = "...";
            this.credentialBtn.UseVisualStyleBackColor = true;
            this.credentialBtn.Click += new System.EventHandler(this.credentialBtn_Click);
            // 
            // credentialTextBox
            // 
            this.credentialTextBox.Location = new System.Drawing.Point(152, 65);
            this.credentialTextBox.Name = "credentialTextBox";
            this.credentialTextBox.ReadOnly = true;
            this.credentialTextBox.Size = new System.Drawing.Size(265, 21);
            this.credentialTextBox.TabIndex = 13;
            // 
            // MainFrame
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(584, 334);
            this.Controls.Add(this.credentialLabel);
            this.Controls.Add(this.credentialBtn);
            this.Controls.Add(this.credentialTextBox);
            this.Controls.Add(this.startBackupBtn);
            this.Controls.Add(this.googleDriveLabel);
            this.Controls.Add(this.googleDriveTextBox);
            this.Controls.Add(this.sourceFolderDeleteAfterCopy);
            this.Controls.Add(this.localFolderLabel);
            this.Controls.Add(this.localFolderBtn);
            this.Controls.Add(this.localFolderTextBox);
            this.Controls.Add(this.gdTarget);
            this.Controls.Add(this.progressbar);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainFrame";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private void connectEvent()
        {
            this.localFolderTextBox.TextChanged += new System.EventHandler(this.readyToStartBackup);
            this.googleDriveTextBox.TextChanged += new System.EventHandler(this.readyToStartBackup);
            this.sourceFolderDeleteAfterCopy.CheckedChanged += new System.EventHandler(this.sourceDeleteChanged);
        }

        private System.Windows.Forms.ProgressBar progressbar;
        private System.Windows.Forms.Label gdTarget;
        
        private System.Windows.Forms.TextBox localFolderTextBox;
        private System.Windows.Forms.Button localFolderBtn;
        private System.Windows.Forms.Label localFolderLabel;
        private System.Windows.Forms.CheckBox sourceFolderDeleteAfterCopy;
        private System.Windows.Forms.Label googleDriveLabel;
        private System.Windows.Forms.TextBox googleDriveTextBox;
        private System.Windows.Forms.ToolTip sourceFolderDeleteAfterCopyToolTip;
        private System.Windows.Forms.Button startBackupBtn;
        private System.Windows.Forms.Label credentialLabel;
        private System.Windows.Forms.Button credentialBtn;
        private System.Windows.Forms.TextBox credentialTextBox;
    }
}

