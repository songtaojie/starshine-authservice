using IdentityModel;
using Starshine.Authservice.Domain.IdentityResources;
using Starshine.Authservice.Domain.Repositories;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Guids;
using Starshine.Abp.Identity;

namespace Starshine.Authservice.Domain.DataSeeder;

public class IdentityResourceDataSeeder : IIdentityResourceDataSeeder, ITransientDependency
{
    protected IIdentityClaimTypeRepository ClaimTypeRepository { get; }
    protected IIdentityResourceRepository IdentityResourceRepository { get; }
    protected IGuidGenerator GuidGenerator { get; }

    public IdentityResourceDataSeeder(
        IIdentityResourceRepository identityResourceRepository,
        IGuidGenerator guidGenerator,
        IIdentityClaimTypeRepository claimTypeRepository)
    {
        IdentityResourceRepository = identityResourceRepository;
        GuidGenerator = guidGenerator;
        ClaimTypeRepository = claimTypeRepository;
    }

    public virtual async Task CreateStandardResourcesAsync()
    {
        var resources = new[]
        {
                new Starshine.IdentityServer.Models.IdentityResources.OpenId(),
                new Starshine.IdentityServer.Models.IdentityResources.Profile(),
                new Starshine.IdentityServer.Models.IdentityResources.Email(),
                new Starshine.IdentityServer.Models.IdentityResources.Address(),
                new Starshine.IdentityServer.Models.IdentityResources.Phone(),
                new Starshine.IdentityServer.Models.IdentityResource("roles", "用户角色",  new[]{ JwtClaimTypes.Role }),
                new Starshine.IdentityServer.Models.IdentityResource("rolename", "角色名", new List<string> { "rolename" }),
            };

        foreach (var resource in resources)
        {
            foreach (var claimType in resource.UserClaims)
            {
                await AddClaimTypeIfNotExistsAsync(claimType);
            }

            await AddIdentityResourceIfNotExistsAsync(resource);
        }
    }

    protected virtual async Task AddIdentityResourceIfNotExistsAsync(Starshine.IdentityServer.Models.IdentityResource resource)
    {
        if (await IdentityResourceRepository.CheckNameExistAsync(resource.Name))
        {
            return;
        }

        await IdentityResourceRepository.InsertAsync(
            new IdentityResource(
                GuidGenerator.Create(),
                resource
            )
        );
    }

    protected virtual async Task AddClaimTypeIfNotExistsAsync(string claimType)
    {
        if (await ClaimTypeRepository.AnyAsync(claimType))
        {
            return;
        }

        await ClaimTypeRepository.InsertAsync(
            new IdentityClaimType(
                GuidGenerator.Create(),
                claimType,
                isStatic: true
            )
        );
    }
}
