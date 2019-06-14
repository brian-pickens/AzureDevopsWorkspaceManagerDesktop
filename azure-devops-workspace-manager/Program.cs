using System;
using SimpleInjector;
using WorkspaceManager.Configuration;
using WorkspaceManager.Views;

namespace WorkspaceManager
{
    public class Program
    {
        private static Container Container => Bootstrapper.Container;

        [STAThread]
        static void Main()
        {
            SimpleMVVM.Program.Run(Container, Container.GetInstance<MainWindow>());
        }

    }
}
