using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoSqlLogReader.Core
{
    public class Credentials
    {
        public string DatabaseName { get; set; }

        public string DatabasePath { get; set; }

        public DatabaseType DatabaseType { get; set; }
    }
}
