using Starshine.Abp.IdentityServer.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Repositories;

namespace Starshine.Authservice.Domain.DataSeed
{
    public class AuthserviceDataSeederContributor : IDataSeedContributor, ITransientDependency
    {
        private readonly IClientDataSeeder _clientDataSeeder;
        private readonly IdentityResourceDataSeeder _identityResourceDataSeeder;

        public AuthserviceDataSeederContributor(IClientDataSeeder clientDataSeeder,
            IdentityResourceDataSeeder identityResourceDataSeeder)
        {
            _clientDataSeeder = clientDataSeeder;
            _identityResourceDataSeeder = identityResourceDataSeeder;
        }

        public async Task SeedAsync(DataSeedContext context)
        {
            await _clientDataSeeder.CreateClientsAsync();
            await _identityResourceDataSeeder.CreateStandardResourcesAsync();
        }
    }
}
