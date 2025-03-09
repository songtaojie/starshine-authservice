using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Starshine.Authservice.Domain.DataSeed
{
    /// <summary>
    /// 客户端数据初始化
    /// </summary>
    public interface IClientDataSeeder
    {
        /// <summary>
        /// 创建客户端数据
        /// </summary>
        /// <returns></returns>
        Task CreateClientsAsync();
    }
}
