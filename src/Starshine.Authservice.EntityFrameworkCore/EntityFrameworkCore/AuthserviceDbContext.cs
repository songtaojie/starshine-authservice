using Microsoft.EntityFrameworkCore;
using Starshine.Authservice.Domain.Clients;
using Starshine.Authservice.Domain.Devices;
using Starshine.Authservice.Domain.Entities.ApiScopes;
using Starshine.Authservice.Domain.IdentityResources;
using Starshine.Authservice.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.MultiTenancy;
using Starshine.Authservice.Domain.Shared.Consts;
using Starshine.Authservice.Domain.ApiResources;
using Starshine.Authservice.Domain.Grants;

namespace Starshine.Authservice.EntityFrameworkCore
{
    [IgnoreMultiTenancy]
    [ConnectionStringName(ConmmonConst.ConnectionStringName)]
    public class AuthserviceDbContext : AbpDbContext<AuthserviceDbContext>, IAuthserviceDbContext
    {
        #region ApiResource

        public DbSet<ApiResource> ApiResources { get; set; }

        public DbSet<ApiResourceSecret> ApiResourceSecrets { get; set; }

        public DbSet<ApiResourceClaim> ApiResourceClaims { get; set; }

        public DbSet<ApiResourceScope> ApiResourceScopes { get; set; }

        public DbSet<ApiResourceProperty> ApiResourceProperties { get; set; }

        #endregion

        #region ApiScope

        public DbSet<ApiScope> ApiScopes { get; set; }

        public DbSet<ApiScopeClaim> ApiScopeClaims { get; set; }

        public DbSet<ApiScopeProperty> ApiScopeProperties { get; set; }

        #endregion

        #region IdentityResource

        public DbSet<IdentityResource> IdentityResources { get; set; }

        public DbSet<IdentityResourceClaim> IdentityClaims { get; set; }

        public DbSet<IdentityResourceProperty> IdentityResourceProperties { get; set; }

        #endregion

        #region Client

        public DbSet<Client> Clients { get; set; }

        public DbSet<ClientGrantType> ClientGrantTypes { get; set; }

        public DbSet<ClientRedirectUri> ClientRedirectUris { get; set; }

        public DbSet<ClientPostLogoutRedirectUri> ClientPostLogoutRedirectUris { get; set; }

        public DbSet<ClientScope> ClientScopes { get; set; }

        public DbSet<ClientSecret> ClientSecrets { get; set; }

        public DbSet<ClientClaim> ClientClaims { get; set; }

        public DbSet<ClientIdPRestriction> ClientIdPRestrictions { get; set; }

        public DbSet<ClientCorsOrigin> ClientCorsOrigins { get; set; }

        public DbSet<ClientProperty> ClientProperties { get; set; }

        #endregion

        public DbSet<PersistedGrant> PersistedGrants { get; set; }

        public DbSet<DeviceFlowCodes> DeviceFlowCodes { get; set; }

        public AuthserviceDbContext(DbContextOptions<AuthserviceDbContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ConfigureIdentityServer();
        }
    }
}
