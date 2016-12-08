using NoSqlLogReader.Core;
using NoSqlRepositories.Core;
using System.Collections.Generic;

namespace NoSqlRepositories.Logger.Viewer.Services
{
    public interface ILogFetcher
    {
        void LoadRepo(INoSQLRepository<Log> repo);

        /// <summary>
        /// Récupère une liste de Logs
        /// </summary>
        /// <returns></returns>
        IList<Log> GetLogs(IList<LogLevel> filters);

        Log GetLogById(string id);

        bool IsLoaded();

        void CreateAttachmentsCopies();

        List<Attachment> GetAttachments(string id);
    }
}
