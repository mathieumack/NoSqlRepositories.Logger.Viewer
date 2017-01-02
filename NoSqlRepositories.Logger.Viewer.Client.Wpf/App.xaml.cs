using MvvmCross.Core.ViewModels;
using MvvmCross.Platform;
using MvvmCross.Wpf.Views;
using NoSqlRepositories.Logger.Viewer.Client.Wpf;
using System;
using System.Windows;
using CefSharp;

namespace NoSqlRepositories.Logger.Viewer.Client.Wpf
{
    /// <summary>
    /// Logique d'interaction pour App.xaml
    /// </summary>
    public partial class App : Application
    {
        private bool _setupComplete = false;

        private void DoSetup()
        {
            var settings = new CefSettings();
            settings.CefCommandLineArgs.Add("disable-gpu", "1");
            settings.CefCommandLineArgs.Add("disable-gpu-compositing", "1");
            settings.BrowserSubprocessPath = @"x86\CefSharp.BrowserSubprocess.exe";
            Cef.Initialize(settings, performDependencyCheck: false, browserProcessHandler: null);

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
