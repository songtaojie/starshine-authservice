using Starshine.Authservice.Domain.IdentityResources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Starshine.Authservice.Domain.Repositories
{
    public interface IIdentityResourceRepository : IBasicRepository<IdentityResource, Guid>
    {
        Task<List<IdentityResource>> GetListByScopeNameAsync(
            string[] scopeNames,
            bool includeDetails = false,
            CancellationToken cancellationToken = default
        );

        Task<List<IdentityResource>> GetListAsync(
            string sorting,
            int skipCount,
            int maxResultCount,
            string? filter = null,
            bool includeDetails = false,
            CancellationToken cancellationToken = default
        );

        Task<long> GetCountAsync(
            string? filter = null,
            CancellationToken cancellationToken = default
        );

        Task<IdentityResource?> FindByNameAsync(
            string name,
            bool includeDetails = true,
            CancellationToken cancellationToken = default
        );

        Task<bool> CheckNameExistAsync(
            string name,
            Guid? expectedId = null,
            CancellationToken cancellationToken = default
         );
    }
}
