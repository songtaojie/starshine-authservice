using Starshine.Authservice.Domain.Clients;
using Starshine.Authservice.Domain.Repositories;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Guids;
using Volo.Abp.Identity;
using Volo.Abp.ObjectMapping;

namespace Starshine.Authservice.Domain.DataSeeder
{
    public class ClientDataSeeder : IClientDataSeeder, ITransientDependency
    {
        private readonly IClientRepository _clientRepository;
        private readonly IGuidGenerator _guidGenerator;
        private readonly IObjectMapper _objectMapper;
        public ClientDataSeeder(IClientRepository clientRepository,
            IGuidGenerator guidGenerator,
            IObjectMapper objectMapper)
        {
            _clientRepository = clientRepository;
            _guidGenerator = guidGenerator;
            _objectMapper = objectMapper;
        }

        public virtual async Task CreateClientsAsync()
        {

            foreach (var client in ClientDataSeedConfig.Clients)
            {
                await AddClientIfNotExistsAsync(client);
            }
        }

        protected virtual async Task AddClientIfNotExistsAsync(IdentityServer4.Models.Client client)
        {
            if (await _clientRepository.CheckClientIdExistAsync(client.ClientId))
            {
                return;
            }
            var dbClient = _objectMapper.Map<IdentityServer4.Models.Client, Client>(client);
            dbClient.SetId(_guidGenerator.Create());
            await _clientRepository.InsertAsync(dbClient);
        }

    }
}
