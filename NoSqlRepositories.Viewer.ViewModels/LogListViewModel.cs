using MvvmCross.Core.ViewModels;
using MvvmCross.Platform;
using MvvmCross.Plugins.File;
using MvvmCross.Plugins.Messenger;
using NoSqlRepositories.Viewer.Services;
using NoSqlRepositories.Viewer.ViewModels.Messenger;
using NoSqlRepositories.Logger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoSqlRepositories.Viewer.ViewModels
{
    public class LogListViewModel : MvxViewModel
    {
        #region Private Fields

        /// <summary>
        /// Liste des logs sous forme d'item
        /// </summary>
        private List<LogListItemViewModel> logList;

        #endregion

        #region Public Fields

        /// <summary>
        /// Liste des logs sous forme d'item
        /// </summary>
        public List<LogListItemViewModel> LogList
        {
            get
            {
                return logList;
            }
            set
            {
                logList = value;
                RaisePropertyChanged(() => LogList);
            }
        }

        public LogListItemViewModel SelectedItem
        {
            set
            {
                Mvx.Resolve<IMvxMessenger>().Publish<LogMessage>(new LogMessage(this, value.Id));
            }
        }

        #endregion

        #region Constructor

        public LogListViewModel(ILogFetcher fetcher,
                                IMvxFileStore fileStore,
                                IMvxMessenger messenger)
        {
            logList = new List<LogListItemViewModel>();
            IList<Log> logs = fetcher.GetLogs(fileStore);
            foreach(Log log in logs)
            {
                logList.Add(new LogListItemViewModel(messenger, log));
            }
            SelectedItem = new LogListItemViewModel(messenger, logs.First());
        }

        #endregion
    }
}
