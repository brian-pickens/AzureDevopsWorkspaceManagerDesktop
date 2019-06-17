using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.TeamFoundation.Client;
using Microsoft.TeamFoundation.VersionControl.Client;

namespace WorkspaceManager.Services
{

    public class WorkspaceService
    {

        const string SERVER_IDENTITY = "Microsoft.TeamFoundation.ServiceIdentity";

        private readonly VersionControlServer _versionControlServer;

        public WorkspaceService(Uri projectCollectionUri)
        {
            var vcs = new TfsTeamProjectCollection(projectCollectionUri);
            _versionControlServer = vcs.GetService<VersionControlServer>();
        }

        public async Task<IEnumerable<Workspace>> GetWorkspaces(bool includeServerCreatedWorkspaces = false)
        {
            return await Task.Run(() => 
                _versionControlServer.QueryWorkspaces(null, "", "")
                    .Where(wsp => wsp.OwnerIdentityType != (includeServerCreatedWorkspaces ? "" : SERVER_IDENTITY)));
        }
    }

}