using Autofac;
using MvvmCross.Core.ViewModels;
using MvvmCross.Platform;
using MvvX.Plugins.AssemblyFinder;
using System.Linq;
using NoSqlRepositories.Viewer.Services;
using NoSqlRepositories.Viewer.Core.Services;

namespace NoSqlRepositories.Viewer.ViewModels
{
    public class App : MvxApplication
    {
        /// <summary>
        /// Constructeur
        /// </summary>
        /// <param name="container"></param>
        public App(IContainer container)
        {
            RegisterComponents(container);
        }

        private void RegisterComponents(IContainer container)
        {
            var typeFinder = Mvx.Resolve<IAssemblyFinder>();

            var builder = new ContainerBuilder();
            
            var servicesEntities = typeFinder.FindClassesOfType(typeof(MvxViewModel)).ToList();
            Mvx.Trace("IoC loading - MvxViewModel " + servicesEntities.Count + " item(s).");
            foreach (var servicesEntity in servicesEntities)
            {
                Mvx.Trace("IoC loading - MvxViewModel : " + servicesEntity.ToString());
                builder.RegisterType(servicesEntity).AsSelf().InstancePerDependency();
            }
            
            builder.Update(container);

            Mvx.RegisterSingleton<ILogFetcher>(new LogFetcher());

            // Start ViewModel
            Mvx.RegisterSingleton<IMvxAppStart>(new MvxAppStart<MainViewModel>());
        }
    }
}
