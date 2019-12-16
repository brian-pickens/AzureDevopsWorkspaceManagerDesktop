using SimpleMVVM.Framework;

namespace WorkspaceManager.ViewModels
{
    public class MainWindowViewModel : ViewModel
    {
        private readonly ICurrentView _frameworkCurrentView;
        private readonly GlobalViewModel _globalViewModel;

        public MainWindowViewModel(ICurrentView framework, GlobalViewModel globalViewModel)
        {
            _frameworkCurrentView = framework;
            _globalViewModel = globalViewModel;

            // Default Bindings
            this["IsLoaderIndicatorVisible"] = false;

            // Change Events
            _frameworkCurrentView.ExecuteOnViewChange(new DelegateCommand(CurrentViewOnChangeEvent));
            _globalViewModel.ExecuteOnPropertyChange("LoaderIndicatorEnabled", new DelegateCommand(ToggleLoader));
        }

        private void CurrentViewOnChangeEvent()
        {
            this["CurrentView"] = _frameworkCurrentView.CurrentView;
        }

        private void ToggleLoader()
        {
            this["IsLoaderIndicatorVisible"] = _globalViewModel["LoaderIndicatorEnabled"];
        }
    }
}
