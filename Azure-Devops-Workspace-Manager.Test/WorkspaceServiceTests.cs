using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.TeamFoundation.VersionControl.Client;
using WorkspaceManager.Services;
using Xunit;
using Xunit.Abstractions;

namespace AzureDevops.Services.Test
{
    public class WorkspaceServiceTests
    {

        private readonly ITestOutputHelper _output;

        public WorkspaceServiceTests(ITestOutputHelper output)
        {
            this._output = output;
        }

        [Fact]
        public async Task Test_GetListOfWorkspaceServices()
        {
            var workspaceService = new WorkspaceService(new Uri("http://chtfs1:8080/tfs/defaultcollection"));
            var workspacesTask = await workspaceService.GetWorkspaces();
            var workspaces = workspacesTask as Workspace[] ?? workspacesTask.ToArray();
            Assert.NotEmpty(workspaces);

            foreach (var workspace in workspaces)
            {
                _output.WriteLine($"workspace: {workspace.Name} | owner: {workspace.OwnerIdentityType}");
            }

        }

        [Fact]
        public async Task Test_GetListOfWorkspaceServicesIncludeServerCreatedWorkspaces()
        {
            var workspaceService = new WorkspaceService(new Uri("http://chtfs1:8080/tfs/defaultcollection"));
            var workspacesTask = await workspaceService.GetWorkspaces(true);
            var workspaces = workspacesTask as Workspace[] ?? workspacesTask.ToArray();
            Assert.NotEmpty(workspaces);
            Assert.NotEmpty(workspaces.Where(wsp => wsp.OwnerIdentityType == "Microsoft.TeamFoundation.ServiceIdentity"));

            foreach (var workspace in workspaces)
            {
                _output.WriteLine($"workspace: {workspace.Name} | owner: {workspace.OwnerIdentityType}");
            }

        }

    }
}