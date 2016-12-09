using MvvmCross.Core.ViewModels;
using MvvmCross.Platform;
using MvvmCross.Plugins.File;
using MvvmCross.Plugins.Messenger;
using NoSqlLogReader.Core;
using NoSqlLogReader.ViewModels.Messenger;
using NoSqlRepositories.Logger;
using NoSqlRepositories.Logger.Viewer.Services;
using NoSqlRepositories.MvvX.JsonFiles.Pcl;
using System;
using System.Collections.Generic;

namespace NoSqlLogReader.ViewModels
{
    public class ConnectionViewModel : MvxViewModel
    {
        #region Private Fields

        private DatabaseType databaseType;

        private string connectionUrl;

        private string databaseName;

        #endregion

        #region Public Fields


        public string ConnectionUrl
        {
            get
            {
                return connectionUrl;
            }
            set
            {
                connectionUrl = value;
                RaisePropertyChanged(() => ConnectionUrl);
            }
        }

        public DatabaseType DatabaseType
        {
            get
            {
                return databaseType;
            }
            set
            {
                databaseType = value;
                RaisePropertyChanged(() => DatabaseType);
            }
        }

        public string DatabaseName
        {
            get
            {
                return databaseName;
            }
            set
            {
                databaseName = value;
                RaisePropertyChanged(() => DatabaseName);
            }
        }

        public IEnumerable<string> EnumDatabaseType { get; set; }

        #endregion

        #region Constructor

        public ConnectionViewModel(IMvxMessenger messenger, ILogFetcher logFetcher)
        {
            var enumNames = Enum.GetNames(typeof(DatabaseType));
            EnumDatabaseType = enumNames;
            databaseType = DatabaseType.JsonFileRepository;
        }

        #endregion

        public bool Connect()
        {
            if(databaseType == DatabaseType.JsonFileRepository)
            {
                var repo = new JsonFileRepository<Log>(Mvx.Resolve<IMvxFileStore>(), databaseName);
                Mvx.Resolve<ILogFetcher>().LoadRepo(repo);
                Mvx.Resolve<IMvxMessenger>().Publish<UpdateLogListMessage>(new UpdateLogListMessage(this, null));
                return true;
            }
            else if(databaseType == DatabaseType.CouchBaseLite)
            {
                //TODO: CBL Connect
                //CouchBaseLiteRepository<Log> repo = new CouchBaseLiteRepository<Log>(...);
            }
            
            return false;
        }

        #region Commands

        public MvxCommand ConnectCommand
        {
            get
            {
                return new MvxCommand(() =>
                {
                    if (Connect())
                    {
                        Mvx.Resolve<ILogFetcher>().CreateAttachmentsCopies();
                    }
                });
            }
        }

        #endregion

    }
}
