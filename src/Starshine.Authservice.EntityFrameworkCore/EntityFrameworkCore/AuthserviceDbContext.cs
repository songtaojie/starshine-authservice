using Microsoft.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;
using Starshine.Authservice.Domain.Shared.Consts;
using Starshine.Abp.Identity.EntityFrameworkCore;
using Starshine.Abp.Identity;
using Volo.Abp.DependencyInjection;
using Starshine.Abp.PermissionManagement;
using Starshine.Abp.TenantManagement.EntityFrameworkCore;
using Starshine.Abp.PermissionManagement.EntityFrameworkCore;
using Starshine.Abp.TenantManagement.Entities;
using Starshine.Abp.IdentityServer.Entities;
using Starshine.Abp.IdentityServer.EntityFrameworkCore;

namespace Starshine.Authservice.EntityFrameworkCore
{
    [ReplaceDbContext(typeof(IIdentityDbContext))]
    [ReplaceDbContext(typeof(ITenantManagementDbContext))]
    [ReplaceDbContext(typeof(IPermissionManagementDbContext))]
    [ReplaceDbContext(typeof(IIdentityServerDbContext))]
    [ConnectionStringName(AuthserviceConst.ConnectionStringName)]
    public class AuthserviceDbContext : AbpDbContext<AuthserviceDbContext>,
        IIdentityDbContext,
        IIdentityServerDbContext,
        ITenantManagementDbContext,
        IPermissionManagementDbContext
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


        #region IIdentityDbContext
        public DbSet<IdentityUser> Users { get; set; }
        public DbSet<IdentityRole> Roles { get; set; }
        public DbSet<IdentityClaimType> ClaimTypes { get; set; }
        public DbSet<OrganizationUnit> OrganizationUnits { get; set; }
        public DbSet<IdentitySecurityLog> SecurityLogs { get; set; }
        public DbSet<IdentityLinkUser> LinkUsers { get; set; }

        public DbSet<IdentityUserDelegation> UserDelegations { get; set; }

        public DbSet<IdentitySession> Sessions { get; set; }

        #endregion

        #region  ITenantManagementDbContext
        public DbSet<Tenant> Tenants { get; set; }

        public DbSet<TenantConnectionString> TenantConnectionStrings { get; set; }


        #endregion

        #region  ITenantManagementDbContext
        public DbSet<PermissionGroupDefinitionRecord> PermissionGroups { get; set; }
        public DbSet<PermissionDefinitionRecord> Permissions { get; set; }
        public DbSet<PermissionGrant> PermissionGrants { get; set; }
        #endregion


        public AuthserviceDbContext(DbContextOptions<AuthserviceDbContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ConfigurePermissionManagement();
            builder.ConfigureIdentity();
            builder.ConfigureTenantManagement();
            builder.ConfigureIdentityServer();
        }
    }
}
