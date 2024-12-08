﻿using Starshine.Authservice.Domain.ApiResources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Starshine.Authservice.Domain.Repositories
{
    public interface IApiResourceRepository : IBasicRepository<ApiResource, Guid>
    {
        Task<ApiResource?> FindByNameAsync(
            string apiResourceName,
            bool includeDetails = true,
            CancellationToken cancellationToken = default
        );

        Task<List<ApiResource>> FindByNameAsync(
            string[] apiResourceNames,
            bool includeDetails = true,
            CancellationToken cancellationToken = default
        );

        Task<List<ApiResource>> GetListByScopesAsync(
            string[] scopeNames,
            bool includeDetails = false,
            CancellationToken cancellationToken = default
        );

        Task<List<ApiResource>> GetListAsync(
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

        Task<bool> CheckNameExistAsync(
            string name,
            Guid? expectedId = null,
            CancellationToken cancellationToken = default
        );
    }
}
