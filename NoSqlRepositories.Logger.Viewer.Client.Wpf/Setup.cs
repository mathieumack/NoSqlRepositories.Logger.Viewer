using MvvmCross.Wpf.Platform;
using MvvmCross.Wpf.Views;
using System.Windows.Threading;
using MvvmCross.Core.ViewModels;
using Autofac;
using MvvmCross.Platform.IoC;
using MvvX.Autofac.Extras;
using System.Configuration;
using System.Reflection;

namespace NoSqlRepositories.Logger.Viewer.Client.Wpf
{
    public class Setup : MvxWpfSetup
    {

        private IContainer container;

        public Setup(Dispatcher uiThreadDispatcher, IMvxWpfViewPresenter presenter) 
            : base(uiThreadDispatcher, presenter)
        {
        }

        protected override IMvxApplication CreateApp()
        {
            return new ViewModels.App(container, 
                ConfigurationManager.AppSettings["hockeyAppId"],
                Assembly.GetExecutingAssembly().GetName().Version.ToString());
        }

        protected override IMvxIoCProvider CreateIocProvider()
        {
            container = new ContainerBuilder().Build();
            return new AutofacMvxIocProvider(container);
        }
    }
}
