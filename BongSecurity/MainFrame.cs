using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Net;

namespace BongSecurity
{
    public partial class MainFrame : Form
    {
        private string m_selectedFile = "";
        private int m_gdTotCount = 0;
        private int m_completeCount = 0;
        public MainFrame(string[] args)
        {
             InitializeComponent();

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

                run();
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

        public void updateGoogleUploadProgressBar(long value, string text)
        {
            progressbar.BeginInvoke(new Action(() =>
            {
                ++m_completeCount;
                progressbar.Value = m_completeCount * 100/ m_gdTotCount;
                gdTarget.Text = "(" + m_completeCount + "/" + m_gdTotCount + ")";
            }));
        }

        private void loadSavePath()
        {
            var folderDlg = new FolderBrowserDialog();
            folderDlg.ShowNewFolderButton = true;
            DialogResult result = folderDlg.ShowDialog();
            if (result != DialogResult.OK)
            {
                return;
            }
            var path = new DirectoryInfo(folderDlg.SelectedPath);
            if(!path.Exists)
            {
                path.Create();
            }
            localFolderTextBox.Text = folderDlg.SelectedPath;
            AppConfiguration.SetAppConfig("LocalPath", folderDlg.SelectedPath);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            loadSavePath();
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
                startBackupBtn.Enabled = false;
            }
            else
            {
                startBackupBtn.Enabled = true;
            }
        }
        

        private void run()
        {
            var isSourceDelete = Convert.ToBoolean(AppConfiguration.GetAppConfig("SourceDelete"));
            var nameOnGoogleDrive = AppConfiguration.GetAppConfig("NameOnGoogleDrive");
            var destPath = AppConfiguration.GetAppConfig("LocalPath");
            var credentialPath = AppConfiguration.GetAppConfig("CredentialPath");

            if (String.IsNullOrEmpty(destPath) ||
                   String.IsNullOrEmpty(nameOnGoogleDrive) ||
                   String.IsNullOrEmpty(credentialPath) ||
                   !new FileInfo(credentialPath).Exists)
            {
                MessageBox.Show("프로그램 설정이 올바르지 않습니다.");
                startBackupBtn.Enabled = false;
                return;
            }

            /// 파일 링크 주소
            string dirName ;
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

            progressbar.Style = ProgressBarStyle.Marquee;
            progressbar.MarqueeAnimationSpeed = 30;

            //Create a tast to run copy file
            Task.Run(() =>
            {
                // To move an entire directory. To programmatically modify or combine
                // path strings, use the System.IO.Path class.

                /// 파일이면 
                /// 폴더 만들고
                /// /// 해당 폴더에 일느 동일하게 복사하기
                FileInfoExtension.CopyFolder(m_selectedFile, target);

                /// 인터넷 체크
                Program.AddLog("Internet connection check.. ");
                if (!CheckForInternetConnection())
                {
                    Program.AddLog("Failed.");
                    System.Environment.Exit(0);
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
                        return;
                    }
                }

                Program.AddLog("Compression success.. ");

                if (GoogleDriveApi.FileUploadInFolder(id, target + ".zip"))
                {
                    Program.AddLog("Upload success.");
                    ///
                    if (isSourceDelete)
                    {
                        FileInfoExtension.DeleteFolder(target + ".zip");
                        FileInfoExtension.DeleteFolder(m_selectedFile);
                    }


                    /// 원본 삭제
                    System.Environment.Exit(0);
                }

            }).GetAwaiter().OnCompleted(() => {
                /// 인터넷 체크
                Program.AddLog("Internet connection check.. ");
                if (!CheckForInternetConnection())
                {
                    Program.AddLog("Failed.");
                    System.Environment.Exit(0);
                    return;
                }

                /// root id 가져오기
                var id = GoogleDriveApi.getBackupFolderId(nameOnGoogleDrive);
                if(String.IsNullOrEmpty(id))
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
                        return;
                    }
                }

                Program.AddLog("Compression success.. ");

                if (GoogleDriveApi.FileUploadInFolder(id, target + ".zip"))
                {
                    Program.AddLog("Upload success.");
                    ///
                    if(isSourceDelete)
                    {
                        FileInfoExtension.DeleteFolder(target + ".zip");
                        FileInfoExtension.DeleteFolder(m_selectedFile);
                    }

                    
                    /// 원본 삭제
                    System.Environment.Exit(0);
                }
                
                //MessageBox.Show("You have successfully copied the file !", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
            });
        }


        public static bool CheckForInternetConnection()
        {
            try
            {
                using (var client = new WebClient())
                using (client.OpenRead("http://google.com/generate_204"))
                    return true;
            }
            catch
            {
                return false;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            run();
        }

        private void credentialBtn_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.InitialDirectory = System.Environment.CurrentDirectory;
                openFileDialog.Filter = "json files (*.json)|*.json";
                openFileDialog.FilterIndex = 2;
                //openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    credentialTextBox.Text = openFileDialog.FileName;
                    AppConfiguration.SetAppConfig("CredentialPath", openFileDialog.FileName);
                }
            }
        }
    }
}
