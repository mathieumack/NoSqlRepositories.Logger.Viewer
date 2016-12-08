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
            DataContext = frameworkElement.DataContext;
            switch (regionName)
            {
                case "LeftPanel":
                    LeftPanel.Children.Clear();
                    LeftPanel.Children.Add(frameworkElement);
                    break;
                case "RightPanel":
                    RightPanel.Children.Clear();
                    RightPanel.Children.Add(frameworkElement);
                    break;
                case "FiltersBar":
                    FiltersBar.Children.Clear();
                    FiltersBar.Children.Add(frameworkElement);
                    break;
                case "Connection":
                    Connection.Children.Clear();
                    Connection.Children.Add(frameworkElement);
                    break;
                default:
                    //DataContext = frameworkElement.DataContext;
                    //Main.Children.Clear();
                    //Main.Children.Add(frameworkElement);
                    break;
            }
        }

        private void ToggleButton_Click(object sender, RoutedEventArgs e)
        {
            if (RightPanel.Visibility == Visibility.Hidden)
                RightPanel.Visibility = Visibility.Visible;
            else
                RightPanel.Visibility = Visibility.Hidden;
        }
    }
}
