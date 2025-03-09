using Starshine.Abp.IdentityServer.Repositories;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Guids;
using Volo.Abp.ObjectMapping;

namespace Starshine.Authservice.Domain.DataSeed
{
    public class ClientDataSeeder : IClientDataSeeder, ITransientDependency
    {
        private readonly IClientRepository _clientRepository;
        private readonly IGuidGenerator _guidGenerator;
        public ClientDataSeeder(IClientRepository clientRepository,
            IGuidGenerator guidGenerator)
        {
            _clientRepository = clientRepository;
            _guidGenerator = guidGenerator;
        }

        public virtual async Task CreateClientsAsync()
        {

            foreach (var client in ClientDataSeedConfig.Clients)
            {
                await AddClientIfNotExistsAsync(client);
            }
        }

        protected virtual async Task AddClientIfNotExistsAsync(Starshine.IdentityServer.Models.Client client)
        {
            if (await _clientRepository.CheckClientIdExistAsync(client.ClientId))
            {
                return;
            }
            var dbClient = client.ToClientEntity(_guidGenerator.Create());
            await _clientRepository.InsertAsync(dbClient);
        }

    }
}
