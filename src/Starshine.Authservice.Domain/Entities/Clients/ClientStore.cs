using System.Threading.Tasks;
using Starshine.IdentityServer.Configuration;
using Starshine.IdentityServer.Stores;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Options;
using Starshine.Authservice.Domain.Repositories;
using Volo.Abp.Caching;
using Volo.Abp.ObjectMapping;

namespace Starshine.Authservice.Domain.Clients;

public class ClientStore : IClientStore
{
    protected IClientRepository ClientRepository { get; }
    protected IObjectMapper ObjectMapper { get; }
    protected IDistributedCache<Starshine.IdentityServer.Models.Client> Cache { get; }
    protected IdentityServerOptions Options { get; }

    public ClientStore(
        IClientRepository clientRepository,
        IObjectMapper<StarshineAuthserviceDomainModule> objectMapper,
        IDistributedCache<Starshine.IdentityServer.Models.Client> cache,
        IOptions<IdentityServerOptions> options)
    {
        ClientRepository = clientRepository;
        ObjectMapper = objectMapper;
        Cache = cache;
        Options = options.Value;
    }

    public virtual async Task<Starshine.IdentityServer.Models.Client?> FindClientByIdAsync(string clientId)
    {
        return await GetCacheItemAsync(clientId);
    }

    protected virtual async Task<Starshine.IdentityServer.Models.Client?> GetCacheItemAsync(string clientId)
    {
        return await Cache.GetOrAddAsync(clientId, async () =>
            {
                var client = await ClientRepository.FindByClientIdAsync(clientId);
                if (client == null) return null;
                return ObjectMapper.Map<Client, Starshine.IdentityServer.Models.Client>(client);
            },
            optionsFactory: () => new DistributedCacheEntryOptions()
            {
                AbsoluteExpirationRelativeToNow = Options.Caching.ClientStoreExpiration
            },
            considerUow: true);

    }
}
