using MvvmCross.Wpf.Platform;
using MvvmCross.Wpf.Views;
using System.Windows.Threading;
using MvvmCross.Core.ViewModels;
using Autofac;
using MvvmCross.Platform.IoC;
using MvvX.Autofac.Extras;

namespace NoSqlRepositories.Viewer.Client.Wpf
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
            return new ViewModels.App(container);
        }

        protected override IMvxIoCProvider CreateIocProvider()
        {
            container = new ContainerBuilder().Build();
            return new AutofacMvxIocProvider(container);
        }
    }
}
