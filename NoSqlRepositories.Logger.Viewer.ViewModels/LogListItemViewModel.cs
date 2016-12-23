using MvvmCross.Core.ViewModels;
using MvvmCross.Plugins.Messenger;
using NoSqlRepositories.Logger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

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
            string dateInfo = "";
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
                //if (diff.Hours > 0)
                //    dateInfo += diff.Hours + "h";
                //if(diff.Minutes > 0)
                //    dateInfo += diff.Minutes + "min";
                //if (diff.Hours == 0 && diff.Minutes == 0)
                //    return "Just now";
                //return dateInfo + " ago";
            }
            else
            {
                return systemCreationDate.Day + "/" + systemCreationDate.Month + "/" + systemCreationDate.Year;
            }

        }

        private string LogLevelToColorString(LogLevel level)
        {
            if (level == LogLevel.Critical)
                return "Purple";
            else if (level == LogLevel.Error)
                return "Red";
            else if (level == LogLevel.Info)
                return "Yellow";
            else if (level == LogLevel.Warning)
                return "Orange";
            return "Black";
        }
    }
}
