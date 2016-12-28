using MvvmCross.Core.ViewModels;
using MvvmCross.Platform;
using MvvmCross.Plugins.File;
using MvvmCross.Plugins.Messenger;
using MvvX.Plugins.CouchBaseLite;
using NoSqlLogReader.Core;
using NoSqlLogReader.ViewModels.Messenger;
using NoSqlRepositories.Logger;
using NoSqlRepositories.Logger.Viewer.Services;
using NoSqlRepositories.MvvX.CouchBaseLite.Pcl;
using NoSqlRepositories.MvvX.JsonFiles.Pcl;
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
            infoFiltered = false;
            warningFiltered = false;
            errorFiltered = false;
            criticalFiltered = false;
        }
        
        private void UpdateFilters()
        {
            List<LogLevel> filters = new List<LogLevel>();
            if (InfoFiltered)
                filters.Add(LogLevel.Info);
            if (WarningFiltered)
                filters.Add(LogLevel.Warning);
            if (ErrorFiltered)
                filters.Add(LogLevel.Error);
            if (CriticalFiltered)
                filters.Add(LogLevel.Critical);
            Mvx.Resolve<IMvxMessenger>().Publish<UpdateLogListMessage>(new UpdateLogListMessage(this, filters));
        }


        public bool ConnectJsonFile()
        {
            var repo = new JsonFileRepository<Log>(Mvx.Resolve<IMvxFileStore>(), fetcher.GetDBName());
            Mvx.Resolve<ILogFetcher>().LoadRepo(repo, fetcher.GetDBName(), DatabaseType.JsonFileRepository);
            Mvx.Resolve<IMvxMessenger>().Publish<UpdateLogListMessage>(new UpdateLogListMessage(this, null));
            return true;
        }

        public bool ConnectCBDatabase()
        {
            var couchBaseLite = Mvx.Resolve<ICouchBaseLite>();
            var repo = new CouchBaseLiteRepository<Log>(couchBaseLite, fetcher.GetDBName());
            Mvx.Resolve<ILogFetcher>().LoadRepo(repo, fetcher.GetDBName(), DatabaseType.CouchBaseLite);
            Mvx.Resolve<IMvxMessenger>().Publish<UpdateLogListMessage>(new UpdateLogListMessage(this, null));
            return true;
        }

        public bool RefreshLogList()
        {
            switch (this.fetcher.GetDatabaseType())
            {
                case DatabaseType.JsonFileRepository:
                    return ConnectJsonFile();
                case DatabaseType.CouchBaseLite:
                    return ConnectCBDatabase();
                default:
                    return false;
            }
        }

        public MvxCommand RefreshLogListCommand
        {
            get
            {
                return new MvxCommand(() =>
                {
                    RefreshLogList();
                });
            }
        }
    }
}
