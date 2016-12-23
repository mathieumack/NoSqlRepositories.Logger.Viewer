using MvvmCross.Core.ViewModels;
using MvvmCross.Platform;
using MvvmCross.Plugins.File;
using MvvmCross.Plugins.Messenger;
using NoSqlRepositories.Logger.Viewer.Services;
using NoSqlRepositories.Logger.Viewer.ViewModels.Messenger;
using NoSqlRepositories.Logger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NoSqlLogReader.ViewModels.Messenger;
using System.Collections.ObjectModel;

namespace NoSqlRepositories.Logger.Viewer.ViewModels
{
    public class LogListViewModel : MvxViewModel
    {
        #region Private Fields

        /// <summary>
        /// Liste des logs sous forme d'item
        /// </summary>
        //private List<LogListItemViewModel> logList;

        private readonly ILogFetcher fetcher;

        private readonly IMvxFileStore fileStore;

        private readonly IMvxMessenger messenger;

        private readonly MvxSubscriptionToken messengerToken;

        #endregion

        #region Public Fields

        /// <summary>
        /// Liste des logs sous forme d'item
        /// </summary>
        public ObservableCollection<LogListItemViewModel> LogList
        {
            get
            ;
            set
           ;
        }

        public LogListItemViewModel SelectedItem
        {
            set
            {
                if (value != null)
                    Mvx.Resolve<IMvxMessenger>().Publish<LogMessage>(new LogMessage(this, value.Id));
            }
        }

        #endregion

        #region Constructor

        public LogListViewModel(ILogFetcher fetcher,
                                IMvxFileStore fileStore,
                                IMvxMessenger messenger)
        {
            messengerToken = Mvx.Resolve<IMvxMessenger>().Subscribe<UpdateLogListMessage>(UpdateLogs);
            this.fetcher = fetcher;
            this.fileStore = fileStore;
            this.messenger = messenger;
            LogList = new ObservableCollection<LogListItemViewModel>();
            
        }
        
        public void UpdateLogs(UpdateLogListMessage sender = null)
        {
            
            if(fetcher.IsLoaded()) { 
                List<LogLevel> filters = (sender != null ? (sender.Filters != null?(List<LogLevel>)sender.Filters:new List<LogLevel>()) : new List<LogLevel>());
                IList<Log> logs = fetcher.GetLogs(filters);
                logs = logs.OrderByDescending(l => l.SystemCreationDate).ToList();
                SelectedItem =  new LogListItemViewModel(messenger, new Log());
                LogList.Clear();
                foreach (Log log in logs)
                {
                    LogList.Add(new LogListItemViewModel(messenger, log));
                }
            }
        }

        #endregion


    }
}
