using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Drive.v3;
using Google.Apis.Services;
using Google.Apis.Util.Store;

using System.Windows.Forms;
/*
Google drive API class

    upload
    search
    create
*/

namespace BongSecurity
{
    public class GoogleDriveFiles
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public long? Size { get; set; }
        public long? Version { get; set; }
        public DateTime? CreatedTime { get; set; }
        public IList<string> Parents { get; set; }
    }

    class GoogleDriveApi
    {

        public static string[] Scopes = { Google.Apis.Drive.v3.DriveService.Scope.Drive };
        public static Google.Apis.Drive.v3.DriveService GetService_v3()
        {
            UserCredential credential;
            using (var stream = new FileStream("credentials.json", FileMode.Open, FileAccess.Read))
            {
                //String FolderPath = @"D:\";
                //String FilePath = Path.Combine(FolderPath, "DriveServiceCredentials.json");

                credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                    GoogleClientSecrets.Load(stream).Secrets,
                    Scopes,
                    "user",
                    CancellationToken.None,
                    new FileDataStore("SuchPropsCredentials.json", true)).Result;
            }

            //Create Drive API service.
            Google.Apis.Drive.v3.DriveService service = new Google.Apis.Drive.v3.DriveService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = "SuchPropsUploader",
            });

            return service;
        }

        public static string CreateFolder(string FolderName)
        {
            Google.Apis.Drive.v3.DriveService service = GetService_v3();

            Google.Apis.Drive.v3.Data.File FileMetaData = new Google.Apis.Drive.v3.Data.File();
            FileMetaData.Name = FolderName;
            FileMetaData.MimeType = "application/vnd.google-apps.folder";

            Google.Apis.Drive.v3.FilesResource.CreateRequest request;

            request = service.Files.Create(FileMetaData);
            request.Fields = "id";
            var file = request.Execute();
            return file.Id;
            //Console.WriteLine("Folder ID: " + file.Id);
        }

        public static void FileUploadInFolder(string folderId, string localfilepath, System.Windows.Forms.ProgressBar prog)
        {
            System.IO.FileAttributes fa = System.IO.File.GetAttributes(localfilepath);

            if ((fa & FileAttributes.Directory) == FileAttributes.Directory)
                return;

            var ext = Path.GetExtension(localfilepath);

            if (string.IsNullOrEmpty(ext))
                return;


            var service = GetService_v3();
            var fileMetadata = new Google.Apis.Drive.v3.Data.File()
            {
                Name = Path.GetFileName(localfilepath),
                Parents = new List<string>
                {
                    folderId
                }
            };

            var mimeType = MimeTypes.MimeTypeMap.GetMimeType(ext);
            FilesResource.CreateMediaUpload request;
            using (var stream = new System.IO.FileStream(localfilepath,
                                    System.IO.FileMode.Open))
            {
                request = service.Files.Create(
                    fileMetadata, stream, mimeType);
                request.Fields = "id";
                //request.Upload();

                var task = request.UploadAsync();
                task.ContinueWith(t =>
                {
                    // Remeber to clean the stream.
                    stream.Dispose();
                });

                request.ProgressChanged += Upload_ProgressChanged;
                request.ResponseReceived += Upload_ResponseReceived;

            }
            var file = request.ResponseBody;


        }


        static void Upload_ProgressChanged(Google.Apis.Upload.IUploadProgress progress)
        {
            Console.WriteLine(progress.Status + " " + progress.BytesSent);

            
        }

        static void Upload_ResponseReceived(Google.Apis.Drive.v3.Data.File file)
        {
            Console.WriteLine(file.Name + " was uploaded successfully");
        }


        public static List<GoogleDriveFiles> GetDriveFiles()
        {
            Google.Apis.Drive.v3.DriveService service = GetService_v3();

            // Define parameters of request.
            Google.Apis.Drive.v3.FilesResource.ListRequest FileListRequest = service.Files.List();
            FileListRequest.Fields = "nextPageToken, files(createdTime, id, name, size, version, trashed, parents)";

            // List files.
            IList<Google.Apis.Drive.v3.Data.File> files = FileListRequest.Execute().Files;
            List<GoogleDriveFiles> FileList = new List<GoogleDriveFiles>();

            if (files != null && files.Count > 0)
            {
                foreach (var file in files)
                {
                    GoogleDriveFiles File = new GoogleDriveFiles
                    {
                        Id = file.Id,
                        Name = file.Name,
                        Size = file.Size,
                        Version = file.Version,
                        CreatedTime = file.CreatedTime,
                        Parents = file.Parents
                    };
                    FileList.Add(File);
                }
            }
            return FileList;
        }
    }


}
