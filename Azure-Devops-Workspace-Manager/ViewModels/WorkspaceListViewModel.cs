using System.Collections.ObjectModel;
using Microsoft.TeamFoundation.VersionControl.Client;
using SimpleMVVM.Framework;
using WorkspaceManager.Services;

namespace WorkspaceManager.ViewModels
{
    public class WorkspaceListViewModel : ViewModel
    {
        private readonly WorkspaceService _workspaceService;

        public WorkspaceListViewModel(WorkspaceService workspaceService)
        {
            _workspaceService = workspaceService;

            // Default Bindings
            this["WorkspaceOwners"] = new ObservableCollection<string>();
            this["Workspaces"] = new ObservableCollection<Workspace>();
            this["IsSearchEnabled"] = true;

            // Commands
            this["DeleteCommand"] = new DelegateCommand(DeleteWorkspaceCommand);
            this["DeleteSelectedCommand"] = new DelegateCommand(DeleteSelectedWorkspacesCommand);
        }

        private void DeleteWorkspaceCommand()
        {
            throw new System.NotImplementedException();
        }

        private void DeleteSelectedWorkspacesCommand()
        {
            throw new System.NotImplementedException();
        }
    }
}
