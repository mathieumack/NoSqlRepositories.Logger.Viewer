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
using Newtonsoft.Json;

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

        public string DatabaseTypeName {
            get
            {
                return Enum.GetName(typeof(DatabaseType), this.DatabaseType);
            }
            set
            {
                DatabaseType choice;
                if(Enum.TryParse(value, out choice))
                {
                    DatabaseType = choice;
                }
            }
        }

        #endregion

        #region Constructor

        public ConnectionViewModel(IMvxMessenger messenger, ILogFetcher logFetcher)
        {
            var enumNames = Enum.GetNames(typeof(DatabaseType));
            EnumDatabaseType = enumNames;
            DatabaseType = DatabaseType.JsonFileRepository;


            this.LoadPreviousCredentials();
        }

        #endregion

        #region Public Methods

        public bool Connect()
        {
            if(databaseType == DatabaseType.JsonFileRepository)
                return ConnectJsonFile();
            else if(databaseType == DatabaseType.CouchBaseLite)
                return ConnectCBDatabase();
            
            return false;
        }

        public bool ConnectJsonFile()
        {
            var repo = new JsonFileRepository<Log>(Mvx.Resolve<IMvxFileStore>(), databaseName);
            Mvx.Resolve<ILogFetcher>().LoadRepo(repo, databaseName, DatabaseType.JsonFileRepository);
            Mvx.Resolve<IMvxMessenger>().Publish<UpdateLogListMessage>(new UpdateLogListMessage(this, null));
            return true;
        }

        public bool ConnectCBDatabase()
        {
            var couchBaseLite = Mvx.Resolve<ICouchBaseLite>();
            couchBaseLite.Initialize(ConnectionUrl);
            var repo = new CouchBaseLiteRepository<Log>(couchBaseLite, DatabaseName);
            Mvx.Resolve<ILogFetcher>().LoadRepo(repo, DatabaseName, DatabaseType.CouchBaseLite);
            Mvx.Resolve<IMvxMessenger>().Publish<UpdateLogListMessage>(new UpdateLogListMessage(this, null));
            return true;
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// This method loads previous connection informations
        /// </summary>
        private void LoadPreviousCredentials()
        {
            IMvxFileStore fileStore = Mvx.Resolve<IMvxFileStore>();
            if (fileStore.Exists(@"./NoSqlRepositories/Logger.Viewer/config.json"))
            {
                string content;

                if (fileStore.TryReadTextFile(@"./NoSqlRepositories/Logger.Viewer/config.json", out content))
                {
                    Credentials credentials = JsonConvert.DeserializeObject<Credentials>(content);
                    this.DatabaseName = credentials.DatabaseName;
                    this.ConnectionUrl = credentials.DatabasePath;
                    this.DatabaseType = credentials.DatabaseType;
                }
                
            }
        }

        /// <summary>
        /// Saves the current connection informations
        /// </summary>
        private void SaveCurentCredentials()
        {
            IMvxFileStore fileStore = Mvx.Resolve<IMvxFileStore>();
            if (fileStore.Exists(@"./NoSqlRepositories/Logger.Viewer/config.json"))
            {
                fileStore.DeleteFile(@"./NoSqlRepositories/Logger.Viewer/config.json");
            }

            Credentials credentials = new Credentials();
            credentials.DatabaseName = this.DatabaseName;
            credentials.DatabasePath = this.ConnectionUrl;
            credentials.DatabaseType = this.DatabaseType;

            fileStore.WriteFile(@"./NoSqlRepositories/Logger.Viewer/config.json", JsonConvert.SerializeObject(credentials, Formatting.Indented));
        }

        #endregion

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
                        SaveCurentCredentials();
                    }
                });
            }
        }

        #endregion

    }
}
