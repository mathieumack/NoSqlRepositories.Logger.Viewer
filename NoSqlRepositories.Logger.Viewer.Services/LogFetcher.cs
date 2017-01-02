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

        private string dbName;

        private DatabaseType mode;
        
        /// <summary>
        /// Load the given repository
        /// </summary>
        /// <param name="repo"></param>
        public void LoadRepo(INoSQLRepository<Log> repo, string dbName, DatabaseType mode)
        {
            this.repo = repo;
            this.dbName = dbName;
            this.mode = mode;
        }

        public DatabaseType GetDatabaseType()
        {
            return this.mode;
        }

        public string GetDBName()
        {
            return this.dbName;
        }

        /// <summary>
        /// Load all the logs
        /// </summary>
        public IList<Log> GetLogs(IList<LogLevel> filters)
        {
            return repo.GetAll().Where(log => !filters.Any(filter => filter == log.Level)).ToList();
        }

        /// <summary>
        /// Get a specific log
        /// </summary>
        /// <param name="id">Id of the log to get</param>
        /// <returns></returns>
        public Log GetLogById(string id)
        {
            return repo.GetById(id);
        }

        /// <summary>
        /// Returns if a repository is currently loaded
        /// </summary>
        /// <returns></returns>
        public bool IsLoaded()
        {
            return (this.repo != null);
        }

        /// <summary>
        /// Delete a log
        /// </summary>
        /// <param name="id">id of the log to delete</param>
        public void DeleteLogById(string id)
        {
            if(!string.IsNullOrWhiteSpace(id))
                repo.Delete(id);
        }

        /// <summary>
        /// Initialize the temp folder with every attachments copies
        /// </summary>
        public void CreateAttachmentsCopies()
        {
            IList<Log> logs = repo.GetAll();
            IMvxFileStore fileStore = Mvx.Resolve<IMvxFileStore>();
            string tempDirPath = @"./NoSqlRepositories/Logger.Viewer/TempAttachments";
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
                    int b = stream.ReadByte();
                    Stream tempFile = fileStore.OpenWrite(tempDirPath + "/" + log.Id + "/" + attachmentName);
                    while (b != -1) {
                        tempFile.WriteByte((byte)b);
                        b = stream.ReadByte();
                    }
                    
                }
            }
        }

        /// <summary>
        /// Returns the list of all attachments
        /// </summary>
        /// <param name="id">id of the log to get all attachments from</param>
        /// <returns></returns>
        public List<Attachment> GetAttachments(string id)
        {
            List<Attachment> attachments = new List<Attachment>();
            foreach(string name in repo.GetAttachmentNames(id))
            {
                attachments.Add(new Attachment(name, @"./NoSqlRepositories/Logger.Viewer/TempAttachments" + id + "/" + name));
            }
            return attachments;
        }
    }
}
