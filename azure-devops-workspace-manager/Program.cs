using System;
using System.Threading.Tasks;
using SimpleInjector;
using WorkspaceManager.Configuration;
using WorkspaceManager.Views;

namespace WorkspaceManager
{
    public class Program
    {
        private static Container Container => Bootstrapper.Container;

        [STAThread]
        static async Task Main()
        {
            SimpleMVVM.Program.Run(Container, Container.GetInstance<MainWindow>());
        }

    }
}
