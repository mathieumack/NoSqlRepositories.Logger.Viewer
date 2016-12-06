using NoSqlRepositories.Viewer.Client.Wpf.Attributes;
using System.Windows.Controls;
using MvvmCross.Wpf.Views;

namespace NoSqlRepositories.Viewer.Client.Wpf.Views
{
    /// <summary>
    /// Logique d'interaction pour LogDetailView.xaml
    /// </summary>
    [Region("LogDetail")]
    public partial class LogDetailView : MvxWpfView
    {
        public LogDetailView()
        {
            InitializeComponent();
        }
    }
}
