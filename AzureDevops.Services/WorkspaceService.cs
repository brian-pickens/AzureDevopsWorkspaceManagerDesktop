using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace AzureDevops.Services
{

    public class WorkspaceService
    {
        private const string TFPATH =
            "Common7\\IDE\\CommonExtensions\\Microsoft\\TeamFoundation\\Team Explorer\\TF.exe";

        private readonly Uri _collection;

        public WorkspaceService(Uri collection)
        {
            _collection = collection;
        }

        public async Task<Workspaces> GetWorkspaces()
        {
            var x = new XmlSerializer(typeof(Workspaces));

            // Use vswhere.exe to locate lastest copy of installed VS instance
            var vsInstallPathProcess = new AsyncProcess("vswhere.exe", @"-prerelease -latest -property installationPath");
            var vsInstallPath = await vsInstallPathProcess.Start().Output().FirstAsync().ConfigureAwait(false);

            // Combine install path with relative TF.exe path
            var tfFullPath = Path.Combine(vsInstallPath, TFPATH);

            // Execute TF workspaces command
            var xmlLineArrayProcess = new AsyncProcess(tfFullPath, $@"workspaces /format:xml /collection:{_collection}").Start();
            var error = string.Join(Environment.NewLine, await xmlLineArrayProcess.Error().ToArrayAsync().ConfigureAwait(false));
            var output = string.Join(Environment.NewLine, await xmlLineArrayProcess.Output().ToArrayAsync().ConfigureAwait(false));

            await xmlLineArrayProcess.WaitForExitCodeAsync().ConfigureAwait(false);

            if (error.Length > 0)
            {
                throw new ApplicationException(error);
            }

            using var stream = new StringReader(output);
            return x.Deserialize(stream) as Workspaces;
        }

        public async Task Delete(WorkspacesWorkspace workspace)
        {

        }
    }

}