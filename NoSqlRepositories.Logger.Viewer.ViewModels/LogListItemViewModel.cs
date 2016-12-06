using MvvmCross.Core.ViewModels;
using MvvmCross.Plugins.Messenger;
using NoSqlRepositories.Logger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoSqlRepositories.Logger.Viewer.ViewModels
{
    public class LogListItemViewModel : MvxViewModel
    {
        #region Private Fields

        /// <summary>
        /// Message du log
        /// </summary>
        private string message;

        #endregion

        #region Public Fields

        private string id;
        public string Id
        {
            get
            {
                return id;
            }
        }

        /// <summary>
        /// Message du log
        /// </summary>
        public string Message
        {
            get
            {
                return message;
            }
            set
            {
                message = value;
                RaisePropertyChanged(() => Message);
            }
        }

        #endregion

        public LogListItemViewModel(IMvxMessenger messenger, Log log)
        {
            message = log.Message;
            id = log.Id;
        }
    }
}
