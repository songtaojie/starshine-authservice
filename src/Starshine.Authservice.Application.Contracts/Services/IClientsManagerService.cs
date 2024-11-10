using Microsoft.AspNetCore.Mvc;
using Starshine.Authservice.Application.Contracts.Dtos.ClientManager;
using Starshine.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Starshine.Authservice.Application.Contracts.Services
{
    public interface IClientsManagerService
    {
        Task<PagedListResult<ClientPageResultDto>> GetPage(ClientPageParamDto param);

        Task<ClientDetailResultDto> GetById(string id);

        Task<bool> AddOrUpdate(ClientCreateParamDto param);

        Task<bool> DeleteById(string id);

        Task<PagedListResult<ClientScPageResultDto>>  GetScPage(ClientPageParamDto param);

        Task<ClientScDetailResultDto> GetScById(string id);

        Task<bool> UpdateSc(ClientScCreateParamDto param);

        Task<PagedListResult<ClientRuPageResultDto>> GetRuPage(ClientPageParamDto param);

        Task<ClientRuDetailResultDto> GetRuById(string id);

        Task<bool> UpdateRu(ClientRuCreateParamDto param);
    }
}
