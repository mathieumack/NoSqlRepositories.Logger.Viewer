using MvvmCross.Wpf.Views;
using NoSqlRepositories.Viewer.Client.Wpf.Attributes;
using System.Windows.Controls;

namespace NoSqlRepositories.Viewer.Client.Wpf.Views
{
    /// <summary>
    /// Logique d'interaction pour LogListView.xaml
    /// </summary>
    [Region("LogList")]
    public partial class LogListView : MvxWpfView
    {
        public LogListView()
        {
            InitializeComponent();
        }
    }
}
