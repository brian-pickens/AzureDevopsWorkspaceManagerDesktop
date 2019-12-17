using System;
using System.IO;
using Microsoft.Extensions.Configuration;
using SimpleInjector;
using SimpleMVVM.Framework;
using WorkspaceManager.Services;
using WorkspaceManager.ViewModels;
using WorkspaceManager.Views;

namespace WorkspaceManager.Configuration
{
    public static class Configuration
    {
        public static Container Container { get; } = new Container();

        public static void Initialize()
        {
            var configuration = GetConfiguration();

            // Create the container as usual.
            Container.Options.DefaultLifestyle = Lifestyle.Singleton;

            // Register Services:
            Uri endpoint = new Uri(configuration["RestApi"]);
            string pat = configuration["Pat"];
            Container.Register<WorkspaceService>((() => new WorkspaceService(endpoint, pat)));
//
//            // Register Framework
            Container.Register<IServiceProvider>(() => Container);
            Container.Register<GlobalViewModel>();
            Container.Register<ICurrentView, GlobalViewModel>();
            Container.Register<IViewModelNavigator, GlobalViewModel>();
//
//            // Register Views
            Container.Register<App>();
            Container.Register<MainWindow>();
            Container.Register<WorkspaceListView>();
//
//            // Register ViewModels
            Container.Register<MainWindowViewModel>();
            Container.Register<WorkspaceListViewModel>();

            Container.Verify();
        }

        public static IConfiguration GetConfiguration()
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .AddJsonFile("appsettings.development.json", true, true)
                .AddUserSecrets<Program>();

            return config.Build();
        }
    }
}
