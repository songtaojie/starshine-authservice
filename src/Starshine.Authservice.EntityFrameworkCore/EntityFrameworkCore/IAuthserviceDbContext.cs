using Microsoft.EntityFrameworkCore;
using Starshine.Authservice.Domain;
using Starshine.Authservice.Domain.ApiResources;
using Starshine.Authservice.Domain.Clients;
using Starshine.Authservice.Domain.Devices;
using Starshine.Authservice.Domain.Entities.ApiScopes;
using Starshine.Authservice.Domain.Grants;
using Starshine.Authservice.Domain.IdentityResources;
using Starshine.Authservice.Domain.Shared.Consts;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.MultiTenancy;

namespace Starshine.Authservice.EntityFrameworkCore
{
    [IgnoreMultiTenancy]
    [ConnectionStringName(ConmmonConst.ConnectionStringName)]
    public interface IAuthserviceDbContext : IEfCoreDbContext
    {
        #region ApiResource
        DbSet<ApiResource> ApiResources { get; }

        DbSet<ApiResourceSecret> ApiResourceSecrets { get; }

        DbSet<ApiResourceClaim> ApiResourceClaims { get; }

        DbSet<ApiResourceScope> ApiResourceScopes { get; }

        DbSet<ApiResourceProperty> ApiResourceProperties { get; }

        #endregion

        #region ApiScope

        DbSet<ApiScope> ApiScopes { get; }

        DbSet<ApiScopeClaim> ApiScopeClaims { get; }

        DbSet<ApiScopeProperty> ApiScopeProperties { get; }

        #endregion

        #region IdentityResource

        DbSet<IdentityResource> IdentityResources { get; }

        DbSet<IdentityResourceClaim> IdentityClaims { get; }

        DbSet<IdentityResourceProperty> IdentityResourceProperties { get; }

        #endregion

        #region Client

        DbSet<Client> Clients { get; }

        DbSet<ClientGrantType> ClientGrantTypes { get; }

        DbSet<ClientRedirectUri> ClientRedirectUris { get; }

        DbSet<ClientPostLogoutRedirectUri> ClientPostLogoutRedirectUris { get; }

        DbSet<ClientScope> ClientScopes { get; }

        DbSet<ClientSecret> ClientSecrets { get; }

        DbSet<ClientClaim> ClientClaims { get; }

        DbSet<ClientIdPRestriction> ClientIdPRestrictions { get; }

        DbSet<ClientCorsOrigin> ClientCorsOrigins { get; }

        DbSet<ClientProperty> ClientProperties { get; }

        #endregion

        DbSet<PersistedGrant> PersistedGrants { get; }

        DbSet<DeviceFlowCodes> DeviceFlowCodes { get; }
    }
}
