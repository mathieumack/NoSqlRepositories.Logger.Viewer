using MvvmCross.Core.ViewModels;
using MvvmCross.Plugins.File;
using MvvmCross.Plugins.Messenger;
using NoSqlRepositories.Logger.Viewer.Services;
using NoSqlLogReader.ViewModels;

namespace NoSqlRepositories.Logger.Viewer.ViewModels
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
            ShowViewModel<ConnectionViewModel>();
            ShowViewModel<LogListViewModel>();
            ShowViewModel<LogDetailViewModel>();
            ShowViewModel<LogFilterViewModel>();
        }

        #endregion
    }
}
