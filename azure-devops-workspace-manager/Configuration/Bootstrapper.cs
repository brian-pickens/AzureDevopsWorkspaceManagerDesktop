using System;
using SimpleInjector;
using SimpleMVVM.Framework;
using WorkspaceManager.Services;
using WorkspaceManager.ViewModels;
using WorkspaceManager.Views;

namespace WorkspaceManager.Configuration
{
    public static class Bootstrapper
    {
        public static Container Container { get; }

        static Bootstrapper()
        {
            // Create the container as usual.
            Container = new Container();
            Container.Options.DefaultLifestyle = Lifestyle.Singleton;

            // Register Services:
            Container.Register<WorkspaceService>();
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
    }
}
