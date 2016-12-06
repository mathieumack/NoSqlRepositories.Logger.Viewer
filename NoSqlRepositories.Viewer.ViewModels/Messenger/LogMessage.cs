using MvvmCross.Plugins.Messenger;
using NoSqlRepositories.Logger;

namespace NoSqlRepositories.Viewer.ViewModels.Messenger
{
    public class LogMessage : MvxMessage
    {
        /// <summary>
        /// Le log sélectionné
        /// </summary>
        public string SelectedLog { get; set; }

        /// <summary>
        /// Constructeur du LogMessage
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="log"></param>
        public LogMessage(object sender, string logId) : base(sender)
        {
            SelectedLog = logId;
        }
    }
}
