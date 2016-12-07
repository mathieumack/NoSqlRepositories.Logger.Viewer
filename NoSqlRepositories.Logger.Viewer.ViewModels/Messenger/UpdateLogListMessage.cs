using MvvmCross.Plugins.Messenger;
using NoSqlRepositories.Logger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoSqlLogReader.ViewModels.Messenger
{
    public class UpdateLogListMessage : MvxMessage
    {

        public IList<LogLevel> Filters { get; set; }


        public UpdateLogListMessage(object sender, IList<LogLevel> filters) : base(sender)
        {
            Filters = filters;
        }
    }
}
