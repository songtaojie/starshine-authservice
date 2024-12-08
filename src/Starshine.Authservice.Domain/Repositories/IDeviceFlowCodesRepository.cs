using Starshine.Authservice.Domain.Devices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Starshine.Authservice.Domain.Repositories
{
    public interface IDeviceFlowCodesRepository : IBasicRepository<DeviceFlowCodes, Guid>
    {
        Task<DeviceFlowCodes?> FindByUserCodeAsync(
            string userCode,
            CancellationToken cancellationToken = default
        );

        Task<DeviceFlowCodes> FindByDeviceCodeAsync(
            string deviceCode,
            CancellationToken cancellationToken = default
        );

        Task<List<DeviceFlowCodes>> GetListByExpirationAsync(
            DateTime maxExpirationDate,
            int maxResultCount,
            CancellationToken cancellationToken = default
        );

        Task DeleteExpirationAsync(
            DateTime maxExpirationDate,
            CancellationToken cancellationToken = default
        );
    }
}
