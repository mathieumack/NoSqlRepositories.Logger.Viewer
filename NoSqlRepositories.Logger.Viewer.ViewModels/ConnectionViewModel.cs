using MvvmCross.Core.ViewModels;
using MvvmCross.Platform;
using MvvmCross.Plugins.Messenger;
using NoSqlLogReader.Core;
using NoSqlLogReader.ViewModels.Messenger;
using NoSqlRepositories.Core;
using NoSqlRepositories.JsonFiles.Net;
using NoSqlRepositories.Logger;
using NoSqlRepositories.Logger.Viewer.Services;
using NoSqlRepositories.MvvX.CouchBaseLite.Pcl;
using System;
using System.Collections.Generic;

namespace NoSqlLogReader.ViewModels
{
    public class ConnectionViewModel : MvxViewModel
    {
        #region Private Fields

        private DatabaseType databaseType;

        private string connectionUrl;

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

        public IEnumerable<string> EnumDatabaseType { get; set; }

        #endregion

        #region Constructor

        public ConnectionViewModel(IMvxMessenger messenger, ILogFetcher logFetcher)
        {
            var enumNames = Enum.GetNames(typeof(DatabaseType));
            EnumDatabaseType = enumNames;

            //TODO: Remove debug lines
            DatabaseType = DatabaseType.JsonFileRepository;
            connectionUrl = ".";
        }

        #endregion

        public bool Connect()
        {
            if(databaseType == DatabaseType.JsonFileRepository)
            {
                try
                { 
                    JsonFileRepository<Log> repo = new JsonFileRepository<Log>(connectionUrl, "Logs");
                    Mvx.Resolve<ILogFetcher>().LoadRepo(repo);
                    Mvx.Resolve<IMvxMessenger>().Publish<UpdateLogListMessage>(new UpdateLogListMessage(this, null));
                    return true;
                }
                catch(Exception e)
                {
                    return false;
                }
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
                    Connect();
                });
            }
        }

        #endregion

    }
}
