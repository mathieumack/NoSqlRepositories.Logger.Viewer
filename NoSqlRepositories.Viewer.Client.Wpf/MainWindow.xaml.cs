using System.Windows;
using NoSqlRepositories.Viewer.Client.Wpf.Views;
using System.Windows.Controls;

namespace NoSqlRepositories.Viewer.Client.Wpf
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Present the view in the right region
        /// </summary>
        /// <param name="frameworkElement"></param>
        /// <param name="regionName"></param>
        public void PresentInRegion(FrameworkElement frameworkElement, string regionName)
        {
            switch (regionName)
            {
                case "LogList":
                    DataContext = frameworkElement.DataContext;
                    LeftPanel.Children.Clear();
                    LeftPanel.Children.Add(frameworkElement);
                    break;
                case "LogDetail":
                    DataContext = frameworkElement.DataContext;
                    RightPanel.Children.Clear();
                    RightPanel.Children.Add(frameworkElement);
                    break;
                default:
                    //DataContext = frameworkElement.DataContext;
                    //Main.Children.Clear();
                    //Main.Children.Add(frameworkElement);
                    break;
            }
        }
    }
}
