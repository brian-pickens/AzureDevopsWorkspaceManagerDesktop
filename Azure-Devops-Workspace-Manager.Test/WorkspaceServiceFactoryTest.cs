using Xunit;
using Xunit.Abstractions;

namespace WorkspaceManager.Test
{
    public class WorkspaceServiceFactoryTest
    {

        private readonly ITestOutputHelper _output;

        public WorkspaceServiceFactoryTest(ITestOutputHelper output)
        {
            this._output = output;
        }

        [Fact]
        public void Test_CreateWorkspaceService()
        {
//            var factory = new WorkspaceServiceFactory();
//            var workspaceService = factory.Create();
//            workspace
        }

    }
}
