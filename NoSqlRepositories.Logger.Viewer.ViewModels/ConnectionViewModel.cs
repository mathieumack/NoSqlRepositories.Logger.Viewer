using MvvmCross.Core.ViewModels;
using MvvmCross.Plugins.Messenger;

namespace NoSqlLogReader.ViewModels
{
    public class ConnectionViewModel : MvxViewModel
    {
        #region Private Fields
        
        private readonly MvxSubscriptionToken messengerToken;

        #endregion

        #region Public Fields
        
        #endregion

        #region Constructor

        public ConnectionViewModel(IMvxMessenger messenger)
        {
            //messengerToken = Mvx.Resolve<IMvxMessenger>().Subscribe<UpdateLogListMessage>(UpdateLogs);
        }

        #endregion


    }
}
