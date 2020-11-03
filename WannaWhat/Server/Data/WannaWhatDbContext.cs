using IdentityServer4.EntityFramework.Options;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Identity;

using WannaWhat.Shared.Models;

namespace WannaWhat.Server.Data
{
    public class WannaWhatDbContext : ApiAuthorizationDbContext<WannaWhatUser>
    {
        public WannaWhatDbContext(
            DbContextOptions options,
            IOptions<OperationalStoreOptions> operationalStoreOptions) : base(options, operationalStoreOptions)
        {
        }
    }
}
