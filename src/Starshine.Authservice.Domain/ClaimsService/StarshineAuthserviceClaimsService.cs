using IdentityModel;
using IdentityServer4.Services;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.JsonWebTokens;
using Starshine.Authservice.Domain.Shared;
using System.Security.Claims;
using Volo.Abp.Security.Claims;

namespace Starshine.Authservice.Domain
{

    public class StarshineAuthserviceClaimsService : DefaultClaimsService
    {
        protected readonly AbpClaimsServiceOptions Options;

        private static readonly string[] AdditionalOptionalClaimNames =
        {
            AbpClaimTypes.TenantId,
            AbpClaimTypes.ImpersonatorTenantId,
            AbpClaimTypes.ImpersonatorUserId,
            AbpClaimTypes.Name,
            AbpClaimTypes.SurName,
            JwtRegisteredClaimNames.UniqueName,
            JwtClaimTypes.PreferredUserName,
            JwtClaimTypes.GivenName,
            JwtClaimTypes.FamilyName,
        };

        public StarshineAuthserviceClaimsService(
            IProfileService profile,
            ILogger<DefaultClaimsService> logger,
            IOptions<AbpClaimsServiceOptions> options)
            : base(profile, logger)
        {
            Options = options.Value;
        }

        protected override IEnumerable<string> FilterRequestedClaimTypes(IEnumerable<string> claimTypes)
        {
            return base.FilterRequestedClaimTypes(claimTypes)
                .Union(Options.RequestedClaims);
        }

        protected override IEnumerable<Claim> GetOptionalClaims(ClaimsPrincipal subject)
        {
            return base.GetOptionalClaims(subject)
                .Union(GetAdditionalOptionalClaims(subject));
        }

        protected virtual IEnumerable<Claim> GetAdditionalOptionalClaims(ClaimsPrincipal subject)
        {
            foreach (var claimName in AdditionalOptionalClaimNames)
            {
                var claim = subject.FindFirst(claimName);
                if (claim != null)
                {
                    yield return claim;
                }
            }
        }
    }
}
