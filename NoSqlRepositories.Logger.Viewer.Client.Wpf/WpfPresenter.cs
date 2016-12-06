using System.Linq;
using System.Windows;
using MvvmCross.Core.ViewModels;
using MvvmCross.Wpf.Views;
using NoSqlRepositories.Logger.Viewer.Client.Wpf.Attributes;
using NoSqlRepositories.Logger.Viewer.Client.Wpf;

namespace NoSqlRepositories.Logger.Viewer.Client.Wpf
{
    public class WpfPresenter : MvxWpfViewPresenter
    {
        private readonly Window mainWindow;

        public WpfPresenter(Window mainWindow)
        {
            this.mainWindow = mainWindow;
        }


        /// <summary>
        /// Display the requested view model
        /// </summary>
        /// <param name="request"></param>
        public override void Show(MvxViewModelRequest request)
        {
            base.Show(request);
        }

        /// <summary>
        /// Manage the views display
        /// </summary>
        /// <param name="frameworkElement"></param>
        public override void Present(FrameworkElement frameworkElement)
        {
            // this is really hacky - do it using attributes isnt
            var attribute = frameworkElement
                                .GetType()
                                .GetCustomAttributes(typeof(RegionAttribute), true)
                                .FirstOrDefault() as RegionAttribute;

            var regionName = attribute == null ? null : attribute.Name;
            (mainWindow as MainWindow).PresentInRegion(frameworkElement, regionName);
        }

    }
}
