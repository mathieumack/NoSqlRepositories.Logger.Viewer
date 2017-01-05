using Autofac;
using MvvmCross.Core.ViewModels;
using MvvmCross.Platform;
using MvvX.Plugins.AssemblyFinder;
using System.Linq;
using NoSqlRepositories.Logger.Viewer.Services;
using NoSqlRepositories.Logger.Viewer.Core.Services;
using MvvX.Plugins.HockeyApp;

namespace NoSqlRepositories.Logger.Viewer.ViewModels
{
    public class App : MvxApplication
    {
        private readonly string hockeyAppKeyId;
        private readonly string applicationVersion;

        /// <summary>
        /// Constructeur
        /// </summary>
        /// <param name="container"></param>
        public App(IContainer container, string hockeyAppKeyId, string applicationVersion)
        {
            this.hockeyAppKeyId = hockeyAppKeyId;
            this.applicationVersion = applicationVersion;

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

            // Start hockeyApp configuration
            var hockeyClient = Mvx.Resolve<IHockeyClient>();
            if (!string.IsNullOrWhiteSpace(hockeyAppKeyId))
                hockeyClient.Configure(hockeyAppKeyId, applicationVersion, true, true, true);
            else
                hockeyClient.Configure("na", applicationVersion, false, false, false);

            hockeyClient.TrackEvent("Start application.");

            Mvx.RegisterSingleton<ILogFetcher>(new LogFetcher());

            // Start ViewModel
            Mvx.RegisterSingleton<IMvxAppStart>(new MvxAppStart<MainViewModel>());
        }
    }
}
