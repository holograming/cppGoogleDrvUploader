using System;
using System.Windows.Forms;
using System.IO;
using System.ComponentModel;

namespace BongSecurity
{
    public partial class MainFrame : Form
    {
        private string m_selectedFile = "";

        enum Error { SUCCESS, NOT_READY, FAILED_INTERNET_CONNECTION, FAILED_COMPRESSION, FAILED_UPLOAD  };

        public MainFrame(string[] args)
        {
            InitializeComponent();
            InitializeBackgroundWorker();

            if (args.Length == 0 || String.IsNullOrEmpty(args[0]) || !FileInfoExtension.isValidInput(args[0]))
            {
                MessageBox.Show("선택한 파일이 없으므로 프로그램을 종료합니다.");
                Program.AddLog("파일 또는 폴더 입력이 없습니다.");
                System.Environment.Exit(0);
                return;
            }

            m_selectedFile = args[0];

            if (!Convert.ToBoolean(AppConfiguration.GetAppConfig("DevMode")))
            {
                sourceFolderDeleteAfterCopy.Checked = Convert.ToBoolean(AppConfiguration.GetAppConfig("SourceDelete"));
                googleDriveTextBox.Text = AppConfiguration.GetAppConfig("NameOnGoogleDrive");
                localFolderTextBox.Text = AppConfiguration.GetAppConfig("LocalPath");
                credentialTextBox.Text = AppConfiguration.GetAppConfig("CredentialPath");
                connectEvent();

                if (String.IsNullOrEmpty(localFolderTextBox.Text) || 
                    String.IsNullOrEmpty(googleDriveTextBox.Text) ||
                    String.IsNullOrEmpty(credentialTextBox.Text) || 
                    !new FileInfo(credentialTextBox.Text).Exists)
                {
                    return;
                }

                backgroundWorker1.RunWorkerAsync();
            }
            else
            {
                Program.AddLog("==================================================");
                Program.AddLog("==================   DevMode   ===================");
                Program.AddLog("==================================================");
                connectEvent();
            }

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            progressbar.Style = ProgressBarStyle.Continuous;
            progressbar.Minimum = 0;
            progressbar.Maximum = 100;
            progressbar.MarqueeAnimationSpeed = 0;

            sourceFolderDeleteAfterCopyToolTip.SetToolTip(sourceFolderDeleteAfterCopy, "원본파일 삭제");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var folderDlg = new FolderBrowserDialog();
            folderDlg.ShowNewFolderButton = true;
            DialogResult result = folderDlg.ShowDialog();
            if (result != DialogResult.OK)
            {
                return;
            }
            var path = new DirectoryInfo(folderDlg.SelectedPath);
            if (!path.Exists)
            {
                path.Create();
            }
            localFolderTextBox.Text = folderDlg.SelectedPath;
            AppConfiguration.SetAppConfig("LocalPath", folderDlg.SelectedPath);
        }

        private void sourceDeleteChanged(object sender, EventArgs e)
        {
            var t = Convert.ToString(sourceFolderDeleteAfterCopy.Checked);
            AppConfiguration.SetAppConfig("SourceDelete", t);
        }

        private void readyToStartBackup(object sender, EventArgs e)
        {
            if(sender == googleDriveTextBox)
            {
                AppConfiguration.SetAppConfig("NameOnGoogleDrive", googleDriveTextBox.Text);
            }
            if(String.IsNullOrEmpty(googleDriveTextBox.Text) || 
               String.IsNullOrEmpty(localFolderTextBox.Text) ||
               String.IsNullOrEmpty(credentialTextBox.Text) ||
               !FileInfoExtension.isDirectory(localFolderTextBox.Text))
            {
                startCancelBtn.Enabled = false;
            }
            else
            {
                startCancelBtn.Enabled = true;
            }
        }


        private void button1_Click(object sender, EventArgs e)
        {
            backgroundWorker1.RunWorkerAsync();
        }

        private void credentialBtn_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.InitialDirectory = System.Environment.CurrentDirectory;
                openFileDialog.Filter = "json files (*.json)|*.json";
                openFileDialog.FilterIndex = 2;
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    credentialTextBox.Text = openFileDialog.FileName;
                    AppConfiguration.SetAppConfig("CredentialPath", openFileDialog.FileName);
                }
            }
        }

        // This event handler is where the actual,
        // potentially time-consuming work is done.
        private void backgroundWorker1_DoWork(object sender,
            DoWorkEventArgs e)
        {
            // Get the BackgroundWorker that raised this event.
            BackgroundWorker worker = sender as BackgroundWorker;
            
            e.Result = Error.SUCCESS;
            var isSourceDelete = Convert.ToBoolean(AppConfiguration.GetAppConfig("SourceDelete"));
            var nameOnGoogleDrive = AppConfiguration.GetAppConfig("NameOnGoogleDrive");
            var destPath = AppConfiguration.GetAppConfig("LocalPath");
            var credentialPath = AppConfiguration.GetAppConfig("CredentialPath");

            if (String.IsNullOrEmpty(destPath) ||
                String.IsNullOrEmpty(nameOnGoogleDrive) ||
                String.IsNullOrEmpty(credentialPath) ||
                !new FileInfo(credentialPath).Exists)
            {
                e.Result = Error.NOT_READY;
                return;
            }
            worker.ReportProgress(0, "Start security (0/3)");
            /// 파일 링크 주소
            string dirName;
            var info = new FileInfo(m_selectedFile);
            if (!FileInfoExtension.isDirectory(m_selectedFile))
            {
                dirName = Path.GetFileNameWithoutExtension(m_selectedFile);
                Program.AddLog("Input file source : " + dirName);
            }
            else
            {
                dirName = new DirectoryInfo(m_selectedFile).Name;
            }

            var target = destPath + "\\" + DateTime.Now.ToString("[yyyy_MM_dd_ss]_") + dirName;
            Program.AddLog("Start copy to " + target);

            //Create a tast to run copy file

            FileInfoExtension.CopyFolder(m_selectedFile, target);
            worker.ReportProgress(30, "Start security (1/3)");

            /// 인터넷 체크
            Program.AddLog("Internet connection check.. ");
            if (!GoogleDriveApi.CheckForInternetConnection())
            {
                Program.AddLog("Failed.");
                e.Result = Error.FAILED_INTERNET_CONNECTION;

                return;
            }

            /// root id 가져오기
            var id = GoogleDriveApi.getBackupFolderId(nameOnGoogleDrive);
            if (String.IsNullOrEmpty(id))
            {
                id = GoogleDriveApi.CreateFolder(nameOnGoogleDrive);
            }

            Program.AddLog("Start google drive upload.. " + id);

            //Google upload
            if (FileInfoExtension.isDirectory(m_selectedFile))
            {
                // 압축
                if (!FileInfoExtension.Compression(m_selectedFile, target + ".zip"))
                {
                    e.Result = Error.FAILED_COMPRESSION;
                    return;
                }
            }

            worker.ReportProgress(80, "Start security (2/3)");
            Program.AddLog("Compression success.. ");
            if (GoogleDriveApi.FileUploadInFolder(id, target + ".zip"))
            {
                worker.ReportProgress(100, "Start bong's security (3/3)");

                Program.AddLog("Upload success.");
                ///
                if (isSourceDelete)
                {
                    FileInfoExtension.DeleteFolder(target + ".zip");
                    FileInfoExtension.DeleteFolder(m_selectedFile);
                }
            }
            else
            {
                e.Result = Error.FAILED_UPLOAD;
            }
        }

        // This event handler deals with the results of the
        // background operation.
        private void backgroundWorker1_RunWorkerCompleted(
            object sender, RunWorkerCompletedEventArgs e)
        {
            // First, handle the case where an exception was thrown.
            if (e.Error != null)
            {
                MessageBox.Show(e.Error.Message);
            }
            else if (e.Cancelled)
            {
                statusLabel.Text = "Canceled";
            }
            else
            {
                var error = (Error)e.Result;
                if (Error.SUCCESS == error)
                {
                    MessageBox.Show("Sucess");
                    System.Environment.Exit(0);
                }
                else if (Error.NOT_READY == error)
                {
                    MessageBox.Show("NOT_READY");
                }
                else if (Error.FAILED_INTERNET_CONNECTION == error)
                {
                    MessageBox.Show("FAILED_INTERNET_CONNECTION");
                }
                else if (Error.FAILED_COMPRESSION == error)
                {
                    MessageBox.Show("FAILED_COMPRESSION");
                }
                else if (Error.FAILED_UPLOAD == error)
                {
                    MessageBox.Show("FAILED_UPLOAD");
                }

                startCancelBtn.Enabled = true;
            }
        }

        // This event handler updates the progress bar.
        private void backgroundWorker1_ProgressChanged(object sender,
            ProgressChangedEventArgs e)
        {
            if(e.ProgressPercentage == 0)
            {
                progressbar.Style = ProgressBarStyle.Marquee;
                progressbar.MarqueeAnimationSpeed = 30;
            }
            this.statusLabel.Text = e.UserState.ToString();
        }

    }
}
