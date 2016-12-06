using MvvmCross.Core.ViewModels;
using MvvmCross.Platform;
using MvvmCross.Plugins.File;
using MvvmCross.Plugins.Messenger;
using NoSqlRepositories.Viewer.Services;
using NoSqlRepositories.Viewer.ViewModels.Messenger;
using NoSqlRepositories.Logger;
using System;
using System.Collections;
using System.Reflection;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace NoSqlRepositories.Viewer.ViewModels
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
        private object contentLog;

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
        public object ContentLog
        {
            get
            {
                return contentLog;
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
            Log selectedLog = fetcher.GetLogById(this.fileStore, logMessage.SelectedLog);

            this.Message = selectedLog.Message;
            this.LongMessage = selectedLog.LongMessage;
            this.Level = selectedLog.Level;
            this.ContentLog = FormatLogObject(selectedLog.ContentLog);
        }

        #endregion

        #region Private Methods

        private string FormatLogObject(object o)
        {
            string message = "";
            if (!IsIterable(o))
            {
                Type objectType = o.GetType();
                if (objectType.GetTypeInfo().IsGenericType || objectType.Namespace.StartsWith("System"))
                {
                    // Object générique
                    return o.ToString();
                }
                else
                {
                    // Objet non générique -> On itère sur les propriétés
                    foreach (PropertyInfo prop in objectType.GetProperties())
                    {
                        if (prop.CanRead)
                        {
                            object oValue = prop.GetValue(o);
                            message += prop.Name + " : " + oValue.ToString() + "\n";
                        }
                    }
                }
            }
            else
            {
                var enumerable = o as System.Collections.IEnumerable;
                message += "[ ";
                foreach(var item in enumerable)
                {
                    message += item.ToString() + " ";
                }
                message += "]";
            }
            return message;
        }


        private bool IsList(object o)
        {
            return o is IList &&
               o.GetType().GetTypeInfo().IsGenericType &&
               o.GetType().GetGenericTypeDefinition().IsAssignableFrom(typeof(List<>));
        }

        private bool IsDictionary(object o)
        {
            return o is IDictionary &&
               o.GetType().GetTypeInfo().IsGenericType &&
               o.GetType().GetGenericTypeDefinition().IsAssignableFrom(typeof(Dictionary<,>));
        }

        private bool IsIterable(object o)
        {
            return IsDictionary(o) || IsList(o) || o is JArray;
        }
        #endregion
    }
}
