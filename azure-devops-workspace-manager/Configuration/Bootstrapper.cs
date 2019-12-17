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
    public static class Bootstrapper
    {
        public static Container Container { get; } = new Container();

        public static void Initialize()
        {
            var configuration = GetConfiguration();

            // Create the container as usual.
            Container.Options.DefaultLifestyle = Lifestyle.Singleton;

            // Register Services:
            Uri projectCollectionUri = new Uri(configuration["projectCollectionUri"]);
            Container.Register<WorkspaceService>((() => new WorkspaceService(projectCollectionUri)));
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
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .AddJsonFile("appsettings.Development.json", true, true);

            IConfiguration config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", true, true)
                .Build();

            return config;
        }
    }
}
