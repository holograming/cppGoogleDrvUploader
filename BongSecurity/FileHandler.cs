using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Compression;
using System.Windows.Forms;


namespace BongSecurity
{

    public static class FileInfoExtension
    {
        public static void CopyTo(this FileInfo file, FileInfo destination, Action<int> progressCallback)
        {
            const int bufferSize = 1024 * 1024;
            byte[] buffer = new byte[bufferSize], buffer2 = new byte[bufferSize];
            bool swap = false;
            int progress = 0, reportedProgress = 0, read = 0;
            long len = file.Length;
            float flen = len;
            Task writer = null;
            using (var source = file.OpenRead())
            using (var dest = destination.OpenWrite())
            {
                dest.SetLength(source.Length);
                for (long size = 0; size < len; size += read)
                {
                    if ((progress = ((int)((size / flen) * 100))) != reportedProgress)
                        progressCallback(reportedProgress = progress);
                    read = source.Read(swap ? buffer : buffer2, 0, bufferSize);
                    writer?.Wait();
                    writer = dest.WriteAsync(swap ? buffer : buffer2, 0, read);
                    swap = !swap;
                }
                writer?.Wait();
            }
        }

        static public void CopyFolder(string sourceFolder, string destFolder)
        {
            if (isDirectory(sourceFolder))
            {
                if (!Directory.Exists(destFolder))
                    Directory.CreateDirectory(destFolder);
                string[] files = Directory.GetFiles(sourceFolder);
                foreach (string file in files)
                {
                    string name = Path.GetFileName(file);
                    string dest = Path.Combine(destFolder, name);
                    File.Copy(file, dest);
                }
                string[] folders = Directory.GetDirectories(sourceFolder);
                foreach (string folder in folders)
                {
                    string name = Path.GetFileName(folder);
                    string dest = Path.Combine(destFolder, name);
                    CopyFolder(folder, dest);
                }
            }
            else
            {
                File.Copy(sourceFolder, destFolder);
            }
        }

        static public void DeleteFolder(string source)
        {
            if(isDirectory(source))
            {
                if (Directory.Exists(source))
                {
                    Directory.Delete(source, true);
                }
            }
            else
            {
                File.Delete(source);
            }
        }


        public static bool Compression(string src, string dest)
        {
            Program.AddLog("Compression : " + src + ", " + dest + "/");

            if (System.IO.File.Exists(dest))
            {
                MessageBox.Show("동일한 파일이 존재합니다.!");
                return false;
            }

            /// 자기 자신 패스에는 안됨.
            try
            {
                ZipFile.CreateFromDirectory(src, dest, CompressionLevel.Fastest, true, Encoding.UTF8);
                Program.AddLog("Compression success.");
            }
            catch (IOException e)
            {
                Program.AddLog("[ " + e.GetType().Name + " ]" + "File compression error.");
                return false;
            }
            return true;
        }

        public static bool isValidInput(string src)
        {
            var file = new FileInfo(src);
            return (file.Exists || Directory.Exists(src) && isDirectory(src));
        }

        public static bool isDirectory(string src)
        {
            System.IO.FileAttributes fa = System.IO.File.GetAttributes(src);

            if ((fa & FileAttributes.Directory) == FileAttributes.Directory)
                return true;

            return false;
        }
    }

    public class CustomSearcher
    {
        public static List<string> GetDirectories(string path, string searchPattern = "*",
            SearchOption searchOption = SearchOption.AllDirectories)
        {
            if (searchOption == SearchOption.TopDirectoryOnly)
                return Directory.GetDirectories(path, searchPattern).ToList();

            var directories = new List<string>(GetDirectories(path, searchPattern));

            for (var i = 0; i < directories.Count; i++)
                directories.AddRange(GetDirectories(directories[i], searchPattern));

            return directories;
        }

        private static List<string> GetDirectories(string path, string searchPattern)
        {
            try
            {
                return Directory.GetDirectories(path, searchPattern).ToList();
            }
            catch (UnauthorizedAccessException)
            {
                return new List<string>();
            }
        }
    }

}
