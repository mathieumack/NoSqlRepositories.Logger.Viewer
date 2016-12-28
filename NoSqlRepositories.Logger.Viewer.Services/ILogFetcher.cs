using NoSqlLogReader.Core;
using NoSqlRepositories.Core;
using System.Collections.Generic;

namespace NoSqlRepositories.Logger.Viewer.Services
{
    public interface ILogFetcher
    {
        /// <summary>
        /// Load the given repository
        /// </summary>
        /// <param name="repo"></param>
        /// <param name="dbName"></param>
        void LoadRepo(INoSQLRepository<Log> repo, string dbName, DatabaseType mode);

        string GetDBName();

        DatabaseType GetDatabaseType();

        /// <summary>
        /// Load all the logs
        /// </summary>
        IList<Log> GetLogs(IList<LogLevel> filters);

        /// <summary>
        /// Get a specific log
        /// </summary>
        /// <param name="id">Id of the log to get</param>
        /// <returns></returns>
        Log GetLogById(string id);

        /// <summary>
        /// Returns if a repository is currently loaded
        /// </summary>
        /// <returns></returns>
        bool IsLoaded();

        /// <summary>
        /// Delete a log
        /// </summary>
        /// <param name="id">id of the log to delete</param>
        void DeleteLogById(string id);

        /// <summary>
        /// Initialize the temp folder with every attachments copies
        /// </summary>
        void CreateAttachmentsCopies();

        /// <summary>
        /// Returns the list of all attachments
        /// </summary>
        /// <param name="id">id of the log to get all attachments from</param>
        /// <returns></returns>
        List<Attachment> GetAttachments(string id);
    }
}
