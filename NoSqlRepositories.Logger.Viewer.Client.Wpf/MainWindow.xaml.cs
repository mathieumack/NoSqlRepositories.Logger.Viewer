using System.Windows;
using MahApps.Metro.Controls;

namespace NoSqlRepositories.Logger.Viewer.Client.Wpf
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
                case "FiltersBar":
                    DataContext = frameworkElement.DataContext;
                    FiltersBar.Children.Clear();
                    FiltersBar.Children.Add(frameworkElement);
                    break;
                case "Connection":
                    DataContext = frameworkElement.DataContext;
                    Connection.Content = frameworkElement;
                    Connection.Visibility = Visibility.Visible;
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
