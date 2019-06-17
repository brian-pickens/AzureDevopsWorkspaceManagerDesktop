using System;
using System.Diagnostics;
using System.Threading.Tasks;
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
            var workspaces = await workspaceService.GetWorkspaces();
            Assert.IsType<Workspaces>(workspaces);
            Assert.NotEmpty(workspaces.Items);

            foreach (var workspace in workspaces.Items)
            {
                _output.WriteLine($"workspace: {workspace.name} | owner: {workspace.owner}");
            }

        }

//        [Fact]
//        public void Test()
//        {
//            var foo = Process.Start(new ProcessStartInfo()
//            {
//                FileName = "C:\\Program Files (x86)\\Microsoft Visual Studio\\2019\\Professional\\Common7\\IDE\\CommonExtensions\\Microsoft\\TeamFoundation\\Team Explorer\\TF.exe"
//            })
//        }

    }
}
