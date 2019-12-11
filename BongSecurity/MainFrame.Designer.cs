using System.ComponentModel;

namespace BongSecurity
{
    partial class MainFrame
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;

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
            this.statusLabel = new System.Windows.Forms.Label();
            this.localFolderTextBox = new System.Windows.Forms.TextBox();
            this.localFolderBtn = new System.Windows.Forms.Button();
            this.localFolderLabel = new System.Windows.Forms.Label();
            this.sourceFolderDeleteAfterCopy = new System.Windows.Forms.CheckBox();
            this.googleDriveLabel = new System.Windows.Forms.Label();
            this.googleDriveTextBox = new System.Windows.Forms.TextBox();
            this.sourceFolderDeleteAfterCopyToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.registryToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.startCancelBtn = new System.Windows.Forms.Button();
            this.credentialLabel = new System.Windows.Forms.Label();
            this.credentialBtn = new System.Windows.Forms.Button();
            this.credentialTextBox = new System.Windows.Forms.TextBox();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.goToEnableApiImg = new System.Windows.Forms.PictureBox();
            this.registryImg = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.goToEnableApiImg)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.registryImg)).BeginInit();
            this.SuspendLayout();
            // 
            // progressbar
            // 
            this.progressbar.Enabled = false;
            this.progressbar.Location = new System.Drawing.Point(13, 178);
            this.progressbar.Name = "progressbar";
            this.progressbar.Size = new System.Drawing.Size(306, 21);
            this.progressbar.TabIndex = 0;
            // 
            // statusLabel
            // 
            this.statusLabel.AutoSize = true;
            this.statusLabel.Location = new System.Drawing.Point(13, 160);
            this.statusLabel.Name = "statusLabel";
            this.statusLabel.Size = new System.Drawing.Size(41, 12);
            this.statusLabel.TabIndex = 1;
            this.statusLabel.Text = "Ready";
            // 
            // localFolderTextBox
            // 
            this.localFolderTextBox.Location = new System.Drawing.Point(131, 88);
            this.localFolderTextBox.Name = "localFolderTextBox";
            this.localFolderTextBox.ReadOnly = true;
            this.localFolderTextBox.Size = new System.Drawing.Size(160, 21);
            this.localFolderTextBox.TabIndex = 5;
            // 
            // localFolderBtn
            // 
            this.localFolderBtn.Location = new System.Drawing.Point(297, 88);
            this.localFolderBtn.Name = "localFolderBtn";
            this.localFolderBtn.Size = new System.Drawing.Size(22, 23);
            this.localFolderBtn.TabIndex = 6;
            this.localFolderBtn.Text = "...";
            this.localFolderBtn.UseVisualStyleBackColor = true;
            this.localFolderBtn.Click += new System.EventHandler(this.button2_Click);
            // 
            // localFolderLabel
            // 
            this.localFolderLabel.Location = new System.Drawing.Point(13, 88);
            this.localFolderLabel.Name = "localFolderLabel";
            this.localFolderLabel.Size = new System.Drawing.Size(112, 23);
            this.localFolderLabel.TabIndex = 7;
            this.localFolderLabel.Text = "로컬 저장 경로";
            this.localFolderLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // sourceFolderDeleteAfterCopy
            // 
            this.sourceFolderDeleteAfterCopy.Enabled = false;
            this.sourceFolderDeleteAfterCopy.Location = new System.Drawing.Point(13, 26);
            this.sourceFolderDeleteAfterCopy.Name = "sourceFolderDeleteAfterCopy";
            this.sourceFolderDeleteAfterCopy.Size = new System.Drawing.Size(24, 24);
            this.sourceFolderDeleteAfterCopy.TabIndex = 8;
            this.sourceFolderDeleteAfterCopy.UseVisualStyleBackColor = true;
            // 
            // googleDriveLabel
            // 
            this.googleDriveLabel.Location = new System.Drawing.Point(11, 111);
            this.googleDriveLabel.Name = "googleDriveLabel";
            this.googleDriveLabel.Size = new System.Drawing.Size(114, 21);
            this.googleDriveLabel.TabIndex = 11;
            this.googleDriveLabel.Text = "구글 드라이브 경로";
            this.googleDriveLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // googleDriveTextBox
            // 
            this.googleDriveTextBox.Location = new System.Drawing.Point(131, 111);
            this.googleDriveTextBox.Name = "googleDriveTextBox";
            this.googleDriveTextBox.Size = new System.Drawing.Size(160, 21);
            this.googleDriveTextBox.TabIndex = 9;
            // 
            // startCancelBtn
            // 
            this.startCancelBtn.Enabled = false;
            this.startCancelBtn.Location = new System.Drawing.Point(104, 220);
            this.startCancelBtn.Name = "startCancelBtn";
            this.startCancelBtn.Size = new System.Drawing.Size(117, 28);
            this.startCancelBtn.TabIndex = 12;
            this.startCancelBtn.Text = "Start";
            this.startCancelBtn.UseVisualStyleBackColor = true;
            this.startCancelBtn.Click += new System.EventHandler(this.button1_Click);
            // 
            // credentialLabel
            // 
            this.credentialLabel.Location = new System.Drawing.Point(13, 65);
            this.credentialLabel.Name = "credentialLabel";
            this.credentialLabel.Size = new System.Drawing.Size(112, 21);
            this.credentialLabel.TabIndex = 15;
            this.credentialLabel.Text = "인증파일";
            this.credentialLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // credentialBtn
            // 
            this.credentialBtn.Location = new System.Drawing.Point(297, 65);
            this.credentialBtn.Name = "credentialBtn";
            this.credentialBtn.Size = new System.Drawing.Size(22, 23);
            this.credentialBtn.TabIndex = 14;
            this.credentialBtn.Text = "...";
            this.credentialBtn.UseVisualStyleBackColor = true;
            this.credentialBtn.Click += new System.EventHandler(this.credentialBtn_Click);
            // 
            // credentialTextBox
            // 
            this.credentialTextBox.Location = new System.Drawing.Point(131, 65);
            this.credentialTextBox.Name = "credentialTextBox";
            this.credentialTextBox.ReadOnly = true;
            this.credentialTextBox.Size = new System.Drawing.Size(160, 21);
            this.credentialTextBox.TabIndex = 13;
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.WorkerReportsProgress = true;
            this.backgroundWorker1.WorkerSupportsCancellation = true;
            // 
            // goToEnableApiImg
            // 
            this.goToEnableApiImg.Enabled = false;
            this.goToEnableApiImg.Image = global::BongSecurity.Properties.Resources._240px_Google_Drive_logo__1_;
            this.goToEnableApiImg.Location = new System.Drawing.Point(63, 26);
            this.goToEnableApiImg.Name = "goToEnableApiImg";
            this.goToEnableApiImg.Size = new System.Drawing.Size(24, 24);
            this.goToEnableApiImg.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.goToEnableApiImg.TabIndex = 18;
            this.goToEnableApiImg.TabStop = false;
            this.goToEnableApiImg.Click += new System.EventHandler(this.goToGoogleDriveApi_Click);
            // 
            // registryImg
            // 
            this.registryImg.Enabled = false;
            this.registryImg.Image = global::BongSecurity.Properties.Resources.regedit;
            this.registryImg.Location = new System.Drawing.Point(34, 26);
            this.registryImg.Name = "registryImg";
            this.registryImg.Size = new System.Drawing.Size(24, 24);
            this.registryImg.TabIndex = 17;
            this.registryImg.TabStop = false;
            this.registryImg.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // MainFrame
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(334, 255);
            this.Controls.Add(this.goToEnableApiImg);
            this.Controls.Add(this.registryImg);
            this.Controls.Add(this.credentialLabel);
            this.Controls.Add(this.credentialBtn);
            this.Controls.Add(this.credentialTextBox);
            this.Controls.Add(this.startCancelBtn);
            this.Controls.Add(this.googleDriveLabel);
            this.Controls.Add(this.googleDriveTextBox);
            this.Controls.Add(this.sourceFolderDeleteAfterCopy);
            this.Controls.Add(this.localFolderLabel);
            this.Controls.Add(this.localFolderBtn);
            this.Controls.Add(this.localFolderTextBox);
            this.Controls.Add(this.statusLabel);
            this.Controls.Add(this.progressbar);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainFrame";
            this.Text = "Bong\'s Security";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.goToEnableApiImg)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.registryImg)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private void InitializeBackgroundWorker()
        {
            backgroundWorker1.DoWork +=
                new DoWorkEventHandler(backgroundWorker1_DoWork);
            backgroundWorker1.RunWorkerCompleted +=
                new RunWorkerCompletedEventHandler(
            backgroundWorker1_RunWorkerCompleted);
            backgroundWorker1.ProgressChanged +=
                new ProgressChangedEventHandler(
            backgroundWorker1_ProgressChanged);
        }

        #endregion

        private void connectEvent()
        {
            this.localFolderTextBox.TextChanged += new System.EventHandler(this.readyToStartBackup);
            this.googleDriveTextBox.TextChanged += new System.EventHandler(this.readyToStartBackup);
            this.sourceFolderDeleteAfterCopy.CheckedChanged += new System.EventHandler(this.sourceDeleteChanged);
        }

        private System.Windows.Forms.ProgressBar progressbar;
        private System.Windows.Forms.Label statusLabel;
        
        private System.Windows.Forms.TextBox localFolderTextBox;
        private System.Windows.Forms.Button localFolderBtn;
        private System.Windows.Forms.Label localFolderLabel;
        private System.Windows.Forms.CheckBox sourceFolderDeleteAfterCopy;
        private System.Windows.Forms.Label googleDriveLabel;
        private System.Windows.Forms.TextBox googleDriveTextBox;
        private System.Windows.Forms.ToolTip sourceFolderDeleteAfterCopyToolTip;
        private System.Windows.Forms.ToolTip registryToolTip;
        private System.Windows.Forms.Button startCancelBtn;
        private System.Windows.Forms.Label credentialLabel;
        private System.Windows.Forms.Button credentialBtn;
        private System.Windows.Forms.TextBox credentialTextBox;
        private System.Windows.Forms.PictureBox registryImg;
        private System.Windows.Forms.PictureBox goToEnableApiImg;
    }
}

