using MvvmCross.Core.ViewModels;
using MvvmCross.Platform;
using MvvmCross.Plugins.Messenger;
using NoSqlLogReader.ViewModels.Messenger;
using NoSqlRepositories.Logger;
using NoSqlRepositories.Viewer.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoSqlLogReader.ViewModels
{
    public class LogFilterViewModel : MvxViewModel
    {

        private bool infoFiltered;

        private bool warningFiltered;

        private bool errorFiltered;

        private bool criticalFiltered;

        private readonly ILogFetcher fetcher;

        private readonly IMvxMessenger messenger;


        public bool InfoFiltered {
            get
            {
                return infoFiltered;
            }
            set
            {
                infoFiltered = value;
                UpdateFilters();
                RaisePropertyChanged(() => InfoFiltered);
            }
        }

        public bool WarningFiltered
        {
            get
            {
                return warningFiltered;
            }
            set
            {
                warningFiltered = value;
                UpdateFilters();
                RaisePropertyChanged(() => WarningFiltered);
            }
        }

        public bool ErrorFiltered
        {
            get
            {
                return errorFiltered;
            }
            set
            {
                errorFiltered = value;
                UpdateFilters();
                RaisePropertyChanged(() => ErrorFiltered);
            }
        }
        public bool CriticalFiltered
        {
            get
            {
                return criticalFiltered;
            }
            set
            {
                criticalFiltered = value;
                UpdateFilters();
                RaisePropertyChanged(() => CriticalFiltered);
            }
        }


        public LogFilterViewModel(ILogFetcher fetcher, IMvxMessenger messenger)
        {
            this.fetcher = fetcher;
            this.messenger = messenger;
            InfoFiltered = false;
            WarningFiltered = false;
            ErrorFiltered = false;
            CriticalFiltered = false;
        }
        
        private void UpdateFilters()
        {
            if (InfoFiltered)
                fetcher.AddFilter(LogLevel.Info);
            else
                fetcher.RemoveFilter(LogLevel.Info);

            if (WarningFiltered)
                fetcher.AddFilter(LogLevel.Warning);
            else
                fetcher.RemoveFilter(LogLevel.Warning);

            if (ErrorFiltered)
                fetcher.AddFilter(LogLevel.Error);
            else
                fetcher.RemoveFilter(LogLevel.Error);

            if (CriticalFiltered)
                fetcher.AddFilter(LogLevel.Critical);
            else
                fetcher.RemoveFilter(LogLevel.Critical);

            Mvx.Resolve<IMvxMessenger>().Publish<UpdateLogListMessage>(new UpdateLogListMessage(this));
        }


    }
}
