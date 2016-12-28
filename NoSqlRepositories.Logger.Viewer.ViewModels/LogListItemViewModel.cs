using MvvmCross.Core.ViewModels;
using MvvmCross.Plugins.Messenger;
using System;

namespace NoSqlRepositories.Logger.Viewer.ViewModels
{
    public class LogListItemViewModel : MvxViewModel
    {
        #region Private Fields

        /// <summary>
        /// Message du log
        /// </summary>
        private string message;

        private string id;

        private string crit;

        private string creationDate;
        #endregion

        #region Public Fields

        public string Crit
        {
            get { return crit; }
            set { crit = value; }
        }

        public string Id
        {
            get
            {
                return id;
            }
        }

        public string CreationDate
        {
            get { return creationDate; }
            set { creationDate = value; }
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
            crit = LogLevelToColorString(log.Level);
            creationDate = DateTimeToString(log.SystemCreationDate);
        }

        private string DateTimeToString(DateTime systemCreationDate)
        {
            if(systemCreationDate.Date == DateTime.Now.Date)
            {
                TimeSpan diff = DateTime.Now.Subtract(systemCreationDate);
                if (diff.Hours != 0)
                    return diff.Hours + "h ago";
                else if (diff.Minutes != 0)
                    return diff.Minutes + "min ago";
                else if (diff.Seconds < 30 && diff.Seconds != 0)
                    return diff.Seconds + "s ago";
                else
                    return "Just now";
            }
            else
            {
                return systemCreationDate.Day + "/" + systemCreationDate.Month + "/" + systemCreationDate.Year;
            }

        }

        private string LogLevelToColorString(LogLevel level)
        {
            switch (level)
            {
                case LogLevel.Critical:
                    return "Purple";
                case LogLevel.Error:
                    return "Red";
                case LogLevel.Info:
                    return "Yellow";
                case LogLevel.Warning:
                    return "Orange";
                default:
                    return "Black";
            }
        }
    }
}
