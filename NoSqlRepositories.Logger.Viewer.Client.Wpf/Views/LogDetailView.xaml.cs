using NoSqlRepositories.Logger.Viewer.Client.Wpf.Attributes;
using System.Windows.Controls;
using MvvmCross.Wpf.Views;

namespace NoSqlRepositories.Logger.Viewer.Client.Wpf.Views
{
    /// <summary>
    /// Logique d'interaction pour LogDetailView.xaml
    /// </summary>
    [Region("RightPanel")]
    public partial class LogDetailView : MvxWpfView
    {
        public LogDetailView()
        {
            InitializeComponent();
        }
    }
}
