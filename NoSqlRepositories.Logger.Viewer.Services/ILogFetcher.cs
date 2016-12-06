using System.Collections.Generic;

namespace NoSqlRepositories.Logger.Viewer.Services
{
    public interface ILogFetcher
    {
        /// <summary>
        /// Récupère une liste de Logs
        /// </summary>
        /// <returns></returns>
        IList<Log> GetLogs(IList<LogLevel> filters);
    }
}
