using System.Threading.Tasks;

namespace Starshine.Authservice.Domain.DataSeeder;

public interface IIdentityResourceDataSeeder
{
    Task CreateStandardResourcesAsync();
}
