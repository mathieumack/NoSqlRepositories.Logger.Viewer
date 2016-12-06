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
    public class MainViewModel : MvxViewModel
    {

        #region Private Fields
        /// <summary>
        /// Liste des logs
        /// </summary>
        private LogListViewModel logList;

        /// <summary>
        /// Détails du log sélectionné
        /// </summary>
        private LogDetailViewModel logDetail;

        #endregion

        #region Public Fields

        /// <summary>
        /// Liste des logs
        /// </summary>
        public LogListViewModel LogList
        {
            get
            {
                return logList;
            }

            set
            {
                logList = value;
            }
        }



        /// <summary>
        /// Détails du log sélectionné
        /// </summary>
        public LogDetailViewModel LogDetail
        {
            get
            {
                return logDetail;
            }

            set
            {
                logDetail = value;
            }
        }

        #endregion

        #region Constructor

        /// <summary>
        /// Constructeur de la vue principale
        /// </summary>
        /// <param name="messenger"></param>
        /// <param name="fileStore"></param>
        /// <param name="fetcher"></param>
        public MainViewModel(IMvxMessenger messenger,
                            ILogFetcher fetcher,
                            IMvxFileStore fileStore)
        {
            ShowViewModel<LogListViewModel>();
            ShowViewModel<LogDetailViewModel>();
        }

        #endregion
    }
}
