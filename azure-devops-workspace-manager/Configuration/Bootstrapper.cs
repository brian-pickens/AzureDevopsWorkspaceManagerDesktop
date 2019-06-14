using System;
using Serilog;
using Serilog.Core;
using Serilog.Extensions.Logging;
using SimpleInjector;
using SimpleMVVM.Framework;
using SimpleMVVM.Serilog;
using WorkspaceManager.Views;
using ILogger = Microsoft.Extensions.Logging.ILogger;

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
            
            var sink = new BindableSink();
            var levelSwitch = new LoggingLevelSwitch();
            var serilogger = new LoggerConfiguration()
                .Enrich.FromLogContext()
                .MinimumLevel.ControlledBy(levelSwitch)
                .WriteTo.BindableSink(sink)
                .CreateLogger();

            var logger = new SerilogLoggerProvider(serilogger).CreateLogger("test_logger");

            // Register Services:
            Container.Register<LoggingLevelSwitch>(() => levelSwitch);
            Container.Register<BindableSink>(() => sink);
            Container.Register<ILogger>(() => logger);
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
//            Container.Register<ServicesListViewModel>();
//            Container.Register<InstallViewModel>();
//            Container.Register<LogView>();
//
//            // Register ViewModels
//            Container.Register<MainWindowViewModel>();
//            Container.Register<ServicesListView>();
//            Container.Register<InstallView>();
//            Container.Register<LogViewViewModel>();

            Container.Verify();
        }
    }
}
