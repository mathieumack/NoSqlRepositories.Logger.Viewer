using MvvmCross.Core.ViewModels;
using MvvmCross.Platform;
using MvvmCross.Wpf.Views;
using NoSqlRepositories.Viewer.Client.Wpf;
using System;
using System.Windows;

namespace NoSqlRepositories.Viewer.Client.Wpf
{
    /// <summary>
    /// Logique d'interaction pour App.xaml
    /// </summary>
    public partial class App : Application
    {
        private bool _setupComplete = false;

        private void DoSetup()
        {
            LoadMvxAssemblyResources();

            var presenter = new WpfPresenter(MainWindow);

            var setup = new Setup(Dispatcher, presenter);
            setup.Initialize();

            Mvx.Resolve<IMvxAppStart>().Start();

            _setupComplete = true;
        }

        protected override void OnActivated(EventArgs e)
        {
            if (!_setupComplete)
            {
                DoSetup();
            }
            base.OnActivated(e);
        }

        private void LoadMvxAssemblyResources()
        {
            int i = 0;
            bool found = false;
            while (!found)
            {
                string key = "MvxAssemblyImport" + i;
                var data = TryFindResource(key);
                if (data == null)
                    found = true;
                i++;
            }
        }
    }
}
