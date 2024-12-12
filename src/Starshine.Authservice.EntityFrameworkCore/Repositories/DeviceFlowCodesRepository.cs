﻿using Microsoft.EntityFrameworkCore;
using Starshine.Authservice.Domain.Devices;
using Starshine.Authservice.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Starshine.Authservice.EntityFrameworkCore.Repositories
{
    public class DeviceFlowCodesRepository : EfCoreRepository<IAuthserviceDbContext, DeviceFlowCodes, Guid>,IDeviceFlowCodesRepository
    {
        public DeviceFlowCodesRepository(IDbContextProvider<IAuthserviceDbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }

        public virtual async Task<DeviceFlowCodes?> FindByUserCodeAsync(
            string userCode,
            CancellationToken cancellationToken = default)
        {
            return await (await GetDbSetAsync())
                .OrderBy(d => d.Id)
                .FirstOrDefaultAsync(d => d.UserCode == userCode, GetCancellationToken(cancellationToken));
        }

        public virtual async Task<DeviceFlowCodes?> FindByDeviceCodeAsync(
            string deviceCode,
            CancellationToken cancellationToken = default)
        {
            return await (await GetDbSetAsync())
                .OrderBy(d => d.Id)
                .FirstOrDefaultAsync(d => d.DeviceCode == deviceCode, GetCancellationToken(cancellationToken));
        }

        public virtual async Task<List<DeviceFlowCodes>> GetListByExpirationAsync(DateTime maxExpirationDate, int maxResultCount,
            CancellationToken cancellationToken = default)
        {
            return await (await GetDbSetAsync())
                .Where(x => x.Expiration != null && x.Expiration < maxExpirationDate)
                .OrderBy(x => x.ClientId)
                .Take(maxResultCount)
                .ToListAsync(GetCancellationToken(cancellationToken));
        }

        public virtual async Task DeleteExpirationAsync(DateTime maxExpirationDate, CancellationToken cancellationToken = default)
        {
            await DeleteDirectAsync(x => x.Expiration != null && x.Expiration < maxExpirationDate, cancellationToken);
        }
    }
}
