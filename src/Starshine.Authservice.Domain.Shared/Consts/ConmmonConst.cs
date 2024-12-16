using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Starshine.Authservice.Domain.Shared.Consts
{
    /// <summary>
    /// 通用常量
    /// </summary>
    public static class ConmmonConst
    {
        /// <summary>
        /// 超级管理员权限
        /// </summary>
        public const string SuperAdmin = "SuperAdmin";

        /// <summary>
        ///客户端
        /// </summary>
        public const string Client = "Client";

        /// <summary>
        /// 连接字符串
        /// </summary>
        public const string ConnectionStringName = "DefaultConnectionString";

        /// <summary>
        /// 数据库前缀
        /// </summary>
        public static string DbTablePrefix { get; set; } = "Auth";

        /* Enable/disable multi-tenancy easily in a single point.
     * If you will never need to multi-tenancy, you can remove
     * related modules and code parts, including this file.
     */
        public const bool IsEnabledMultiTenancy = true;
    }
}
