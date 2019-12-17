using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.TeamFoundation.Client;
using Microsoft.TeamFoundation.VersionControl.Client;
using Microsoft.VisualStudio.Services.Common;

namespace WorkspaceManager.Services
{

    public class WorkspaceService
    {
        public const string SERVER_IDENTITY_TYPE = "Microsoft.TeamFoundation.ServiceIdentity";
        public const string USER_IDENTITY = "Microsoft.IdentityModel.Claims.ClaimsIdentity";

        private readonly VersionControlServer _versionControlServer;

        public WorkspaceService(Uri projectCollectionUri, string pat)
        {
            var creds = new VssBasicCredential(string.Empty, pat);
            var vcs = new TfsTeamProjectCollection(projectCollectionUri, creds);
            _versionControlServer = vcs.GetService<VersionControlServer>();
        }

        public async Task<IEnumerable<Workspace>> GetWorkspaces(bool includeServerCreatedWorkspaces = false)
        {
            return await Task.Run(() => 
                _versionControlServer.QueryWorkspaces(null, "", "")
                    .Where(wsp => includeServerCreatedWorkspaces || wsp.OwnerIdentityType != SERVER_IDENTITY_TYPE));
        }
    }

}