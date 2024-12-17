﻿using Microsoft.EntityFrameworkCore;
using Starshine.Authservice.Domain.IdentityResources;
using Starshine.Authservice.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Starshine.Authservice.EntityFrameworkCore.Repositories
{
    public class IdentityResourceRepository : EfCoreRepository<IAuthserviceDbContext, IdentityResource, Guid>, IIdentityResourceRepository
    {
        public IdentityResourceRepository(IDbContextProvider<IAuthserviceDbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }

        public virtual async Task<List<IdentityResource>> GetListByScopeNameAsync(
            string[] scopeNames,
            bool includeDetails = false,
            CancellationToken cancellationToken = default)
        {
            return await (await GetDbSetAsync())
                .IncludeDetails(includeDetails)
                .Where(identityResource => scopeNames.Contains(identityResource.Name))
                .ToListAsync(GetCancellationToken(cancellationToken));
        }

        [Obsolete("Use WithDetailsAsync method.")]
        public override IQueryable<IdentityResource> WithDetails()
        {
            return GetQueryable().IncludeDetails();
        }

        public override async Task<IQueryable<IdentityResource>> WithDetailsAsync()
        {
            return (await GetQueryableAsync()).IncludeDetails();
        }

        public virtual async Task<List<IdentityResource>> GetListAsync(string sorting, int skipCount, int maxResultCount,
            string? filter, bool includeDetails = false, CancellationToken cancellationToken = default)
        {
            return await (await GetDbSetAsync())
                .IncludeDetails(includeDetails)
                .WhereIf(!filter.IsNullOrWhiteSpace(), x => x.Name.Contains(filter!) ||
                         x.Description.Contains(filter!) ||
                         x.DisplayName.Contains(filter!))
                .OrderBy(sorting.IsNullOrWhiteSpace() ? nameof(IdentityResource.Name) : sorting)
                .PageBy(skipCount, maxResultCount)
                .ToListAsync(GetCancellationToken(cancellationToken));
        }

        public virtual async Task<long> GetCountAsync(string? filter = null, CancellationToken cancellationToken = default)
        {
            return await (await GetDbSetAsync())
                .WhereIf(!filter.IsNullOrWhiteSpace(),
                    x => x.Name.Contains(filter!) ||
                         x.Description.Contains(filter!) ||
                         x.DisplayName.Contains(filter!))
                .LongCountAsync(GetCancellationToken(cancellationToken));
        }

        public virtual async Task<IdentityResource?> FindByNameAsync(
            string name,
            bool includeDetails = true,
            CancellationToken cancellationToken = default)
        {
            return await (await GetDbSetAsync())
                .IncludeDetails(includeDetails)
                .OrderBy(x => x.Id)
                .FirstOrDefaultAsync(x => x.Name == name, GetCancellationToken(cancellationToken));
        }

        public virtual async Task<bool> CheckNameExistAsync(string name, Guid? expectedId = null, CancellationToken cancellationToken = default)
        {
            return await (await GetDbSetAsync()).AnyAsync(ir => ir.Id != expectedId && ir.Name == name, GetCancellationToken(cancellationToken));
        }
    }
}