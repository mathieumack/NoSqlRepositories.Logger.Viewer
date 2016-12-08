using MvvmCross.Core.ViewModels;
using MvvmCross.Platform;
using MvvmCross.Plugins.File;
using MvvmCross.Plugins.Messenger;
using NoSqlRepositories.Logger.Viewer.Services;
using NoSqlRepositories.Logger.Viewer.ViewModels.Messenger;
using NoSqlRepositories.Logger;
using System;
using System.Collections;
using System.Reflection;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Text.RegularExpressions;

namespace NoSqlRepositories.Logger.Viewer.ViewModels
{
    public class LogDetailViewModel : MvxViewModel
    {
        #region Private Fields

        /// <summary>
        /// Message du log
        /// </summary>
        private string message;

        /// <summary>
        /// Message long du log
        /// </summary>
        private string longMessage;

        /// <summary>
        /// Niveau critique du log
        /// </summary>
        private LogLevel level;

        /// <summary>
        /// Objet aditionnel au log
        /// </summary>
        private string contentLog;

        private readonly MvxSubscriptionToken messengerToken;

        private ILogFetcher fetcher;

        private readonly IMvxFileStore fileStore;

        #endregion

        #region Public Fields

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

        /// <summary>
        /// Message long du log
        /// </summary>
        public string LongMessage
        {
            get
            {
                return longMessage;
            }
            set
            {
                longMessage = value;
                RaisePropertyChanged(() => LongMessage);
            }
        }

        /// <summary>
        /// Niveau critique du log
        /// </summary>
        public LogLevel Level
        {
            get
            {
                return level;
            }
            set
            {
                level = value;
                RaisePropertyChanged(() => Level);
            }
        }

        /// <summary>
        /// Objet additionnel au log
        /// </summary>
        public string ContentLog
        {
            get
            {
                return (contentLog==null?contentLog:HighlightJson(contentLog));
            }
            set
            {
                contentLog = value;
                RaisePropertyChanged(() => ContentLog);
            }
        }

        #endregion

        #region Constructor

        public LogDetailViewModel(IMvxMessenger messenger,
                                  ILogFetcher fetcher,
                                  IMvxFileStore fileStore)
        {
            this.fileStore = fileStore;
            this.fetcher = fetcher;
            messengerToken = messenger.Subscribe<LogMessage>(UpdateData);

            
        }

        #endregion

        #region Messenger

        /// <summary>
        /// Met à jour les details du log
        /// </summary>
        /// <param name="logMessage"></param>
        private void UpdateData(LogMessage logMessage)
        {
            if(logMessage.SelectedLog != null)
            {
                Log selectedLog = fetcher.GetLogById(logMessage.SelectedLog);
                this.Message = selectedLog.Message;
                this.LongMessage = selectedLog.LongMessage;
                this.Level = selectedLog.Level;
                this.ContentLog = JValue.Parse(selectedLog.ContentLog).ToString(Formatting.Indented);
            }

           
        }

        #endregion

        #region Private Methods

        public string HighlightJson(string json)
        {
            return Regex.Replace(
                json,
                @"(¤(\\u[a-zA-Z0-9]{4}|\\[^u]|[^\\¤])*¤(\s*:)?|\b(true|false|null)\b|-?\d+(?:\.\d*)?(?:[eE][+\-]?\d+)?)".Replace('¤', '"'),
                match => {
                    var cls = "number";
                    if (Regex.IsMatch(match.Value, @"^¤".Replace('¤', '"')))
                    {
                        if (Regex.IsMatch(match.Value, ":$"))
                        {
                            cls = "key";
                        }
                        else
                        {
                            cls = "string";
                        }
                    }
                    else if (Regex.IsMatch(match.Value, "true|false"))
                    {
                        cls = "boolean";
                    }
                    else if (Regex.IsMatch(match.Value, "null"))
                    {
                        cls = "null";
                    }
                    return "<span class=\"" + cls + "\">" + match + "</span>";
                });
        }

        #endregion
    }
}
