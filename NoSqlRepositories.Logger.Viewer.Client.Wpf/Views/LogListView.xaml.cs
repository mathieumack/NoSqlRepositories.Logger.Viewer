using MvvmCross.Wpf.Views;
using NoSqlRepositories.Logger.Viewer.Client.Wpf.Attributes;
using System.Windows.Controls;

namespace NoSqlRepositories.Logger.Viewer.Client.Wpf.Views
{
    /// <summary>
    /// Logique d'interaction pour LogListView.xaml
    /// </summary>
    [Region("LeftPanel")]
    public partial class LogListView : MvxWpfView
    {
        public LogListView()
        {
            InitializeComponent();
        }
    }
}
