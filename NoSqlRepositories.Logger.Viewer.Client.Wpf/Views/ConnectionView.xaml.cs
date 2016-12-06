using MvvmCross.Wpf.Views;
using NoSqlRepositories.Logger.Viewer.Client.Wpf.Attributes;

namespace NoSqlLogReader.UI.Wpf.Views
{
    /// <summary>
    /// Logique d'interaction pour LogFilterView.xaml
    /// </summary>
    [Region("Flyout")]
    public partial class ConnectionView : MvxWpfView
    {
        public ConnectionView()
        {
            InitializeComponent();
        }
    }
}
