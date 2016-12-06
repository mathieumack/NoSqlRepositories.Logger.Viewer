using System.Collections.Generic;
using NoSqlRepositories.Logger;
using MvvmCross.Plugins.File;
using MvvmCross.Platform;
using NoSqlRepositories.Viewer.Services;
using System.Linq;
using NoSqlRepositories.MvvX.JsonFiles.Pcl;
using System.Collections;

namespace NoSqlRepositories.Viewer.Core.Services
{
    public class LogFetcher : ILogFetcher
    {
        private readonly string jsonPath = "C:\\Sources\\FirstNoSqlRepositories.Viewer\\NoSqlRepositories.Viewer\\NoSqlRepositories.Viewer\\";

        private IList<Log> logs;

        private List<LogLevel> filter;

        public LogFetcher()
        {
            logs = new List<Log>();
            filter = new List<LogLevel>();
        }

        /// <summary>
        /// Charge la liste des logs
        /// </summary>
        public IList<Log> GetLogs(IMvxFileStore fileStore)
        {
            JsonFileRepository<Log> repo = new JsonFileRepository<Log>(fileStore, "Logs");
            this.logs = repo.GetAll().Where(log => !filter.Any(filter => filter == log.Level)).ToList();
            return this.logs;
        }

        public Log GetLogById(IMvxFileStore fileStore, string queryId)
        {
            if (logs.Count < 1)
                this.GetLogs(fileStore);


            Log result = this.logs.Where(log => log.Id == queryId).First();
            return result;
        }

        public void AddFilter(LogLevel filterSpec)
        {
            if (!this.filter.Contains(filterSpec))
                this.filter.Add(filterSpec);
        }

        public void RemoveFilter(LogLevel filterSpec)
        {
            if (this.filter.Contains(filterSpec))
                this.filter.Remove(filterSpec);
        }

    }
}
