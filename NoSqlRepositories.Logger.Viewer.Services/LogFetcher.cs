using System.Collections.Generic;
using NoSqlRepositories.Logger.Viewer.Services;
using System.Linq;
using NoSqlRepositories.Core;

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
    }
}
