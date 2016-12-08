using System.Collections.Generic;
using NoSqlRepositories.Logger.Viewer.Services;
using System.Linq;
using NoSqlRepositories.Core;
using NoSqlLogReader.Core;
using MvvmCross.Platform;
using MvvmCross.Plugins.File;
using System.IO;

namespace NoSqlRepositories.Logger.Viewer.Core.Services
{
    public class LogFetcher : ILogFetcher
    {

        private INoSQLRepository<Log> repo;
        
        public void LoadRepo(INoSQLRepository<Log> repo)
        {
            this.repo = repo;
        }

        /// <summary>
        /// Charge la liste des logs
        /// </summary>
        public IList<Log> GetLogs(IList<LogLevel> filters)
        {
            return repo.GetAll().Where(log => !filters.Any(filter => filter == log.Level)).ToList();
        }

        public Log GetLogById(string id)
        {
            return repo.GetById(id);
        }


        public bool IsLoaded()
        {
            return (this.repo != null);
        }

        /// <summary>
        /// Initialize the temp folder with every attachments copies
        /// </summary>
        public void CreateAttachmentsCopies()
        {
            IList<Log> logs = repo.GetAll();
            IMvxFileStore fileStore = Mvx.Resolve<IMvxFileStore>();
            string tempDirPath = @"./TempAttachments";
            // Create/Clear temp dir
            if (fileStore.FolderExists(tempDirPath))
            {
                fileStore.DeleteFolder(tempDirPath, true);
            }
            fileStore.EnsureFolderExists(tempDirPath);
            

            foreach(Log log in logs)
            {
                IList<string> attachmentsNames = repo.GetAttachmentNames(log.Id);
                if (attachmentsNames.Count > 0)
                    fileStore.EnsureFolderExists(tempDirPath + "/" + log.Id);
                foreach(string attachmentName in attachmentsNames)
                {
                    Stream stream = repo.GetAttachment(log.Id, attachmentName);
                    stream.Seek(0, SeekOrigin.Begin);
                    StreamReader sr = new StreamReader(stream);
                    //fileStore.WriteFile(tempDirPath + "/" + log.Id + "/" + attachmentName, sr.ReadToEnd());
                    int b = stream.ReadByte();
                    Stream tempFile = fileStore.OpenWrite(tempDirPath + "/" + log.Id + "/" + attachmentName);
                    while (b != -1) {
                        tempFile.WriteByte((byte)b);
                        b = stream.ReadByte();
                    }
                    
                }
            }
        }

        public List<Attachment> GetAttachments(string id)
        {
            List<Attachment> attachments = new List<Attachment>();
            foreach(string name in repo.GetAttachmentNames(id))
            {
                attachments.Add(new Attachment(name, @"./TempAttachments/" + id + "/" + name));
            }
            return attachments;
        }
    }
}
