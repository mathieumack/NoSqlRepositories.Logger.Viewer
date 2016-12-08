using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoSqlLogReader.Core
{
    public class Attachment
    {
        public string Name { get; set; }

        public string Path { get; set; }

        public Attachment(string Name, string Path)
        {
            this.Name = Name;
            this.Path = Path;
        }
    }
}
