using MvvmCross.Plugins.File;
using NoSqlRepositories.Logger;
using System.Collections.Generic;

namespace NoSqlRepositories.Viewer.Services
{
    public interface ILogFetcher
    {
        /// <summary>
        /// Récupère une liste de Logs
        /// </summary>
        /// <returns></returns>
        IList<Log> GetLogs(IMvxFileStore fileStore);

        Log GetLogById(IMvxFileStore fileStore, string selectedLog);

        void AddFilter(LogLevel filterSpec);

        void RemoveFilter(LogLevel filterSpec);
    }
}
