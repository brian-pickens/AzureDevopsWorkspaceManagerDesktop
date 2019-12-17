using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.TeamFoundation.VersionControl.Client;
using WorkspaceManager.Configuration;
using WorkspaceManager.Services;
using Xunit;
using Xunit.Abstractions;

namespace WorkspaceManager.Test
{
    public class WorkspaceServiceTests
    {

        private readonly ITestOutputHelper _output;
        private Uri _uri;
        private string _pat;

        public WorkspaceServiceTests(ITestOutputHelper output)
        {
            _output = output;
            var config = Configuration.Configuration.GetConfiguration();
            _uri = new Uri(config["RestApi"]);
            _pat = config["Pat"];
        }

        [Fact]
        public async Task Test_GetListOfWorkspaceServices()
        {
            var workspaceService = new WorkspaceService(_uri, _pat);
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
            var workspaceService = new WorkspaceService(_uri, _pat);
            var workspacesTask = await workspaceService.GetWorkspaces(true);
            var workspaces = workspacesTask as Workspace[] ?? workspacesTask.ToArray();
            Assert.NotEmpty(workspaces);
            Assert.NotEmpty(workspaces.Where(wsp => wsp.OwnerIdentityType == WorkspaceService.SERVER_IDENTITY_TYPE));

            foreach (var workspace in workspaces)
            {
                _output.WriteLine($"workspace: {workspace.Name} | owner: {workspace.OwnerIdentityType}");
            }

        }

    }
}