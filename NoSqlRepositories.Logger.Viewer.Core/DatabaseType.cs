using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoSqlLogReader.Core
{
    public enum DatabaseType
    {
        /// <summary>
        /// Connect to a JsonRepository
        /// </summary>
        JsonFileRepository = 1,

        /// <summary>
        /// Connect to a CouchBaseLite db
        /// </summary>
        CouchBaseLite = 2,
    }
}
