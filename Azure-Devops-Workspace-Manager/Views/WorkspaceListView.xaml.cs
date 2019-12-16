using WorkspaceManager.ViewModels;

namespace WorkspaceManager.Views
{
    /// <summary>
    /// Interaction logic for WorkspaceListView.xaml
    /// </summary>
    public partial class WorkspaceListView
    {
        public WorkspaceListView(WorkspaceListViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}
