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
using NoSqlLogReader.ViewModels.Messenger;
using System.Collections.ObjectModel;

namespace NoSqlRepositories.Viewer.ViewModels
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
            UpdateLogs();
        }
        
        public void UpdateLogs(UpdateLogListMessage sender = null)
        {
            IList<Log> logs = fetcher.GetLogs(fileStore);
            LogList.Clear();
            foreach (Log log in logs)
            {
                LogList.Add(new LogListItemViewModel(messenger, log));
            }
            if (logs.Count > 0)
                SelectedItem = new LogListItemViewModel(messenger, logs.First());
        }

        #endregion


    }
}
