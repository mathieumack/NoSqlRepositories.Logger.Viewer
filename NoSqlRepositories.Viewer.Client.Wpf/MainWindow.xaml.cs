using System.Windows;
using NoSqlRepositories.Viewer.Client.Wpf.Views;
using System.Windows.Controls;
using MahApps.Metro.Controls;

namespace NoSqlRepositories.Viewer.Client.Wpf
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
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
                case "LeftPanel":
                    DataContext = frameworkElement.DataContext;
                    LeftPanel.Children.Clear();
                    LeftPanel.Children.Add(frameworkElement);
                    break;
                case "RightPanel":
                    DataContext = frameworkElement.DataContext;
                    RightPanel.Children.Clear();
                    RightPanel.Children.Add(frameworkElement);
                    break;
                case "Navbar":
                    DataContext = frameworkElement.DataContext;
                    Navbar.Children.Clear();
                    Navbar.Children.Add(frameworkElement);
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
