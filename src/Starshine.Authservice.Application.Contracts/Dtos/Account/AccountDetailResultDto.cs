using System;
using System.Collections.Generic;
using System.Text;

namespace Starshine.Authservice.Application.Contracts.Dtos.Account
{
    /// <summary>
    /// 用户明细信息
    /// </summary>
    public class AccountDetailResultDto
    {
        /// <summary>
        /// 用户id
        /// </summary>
        public string Id { get; set; }

        public string RealName { get; set; }

        public string UserName { get; set; }

        public string Email { get; set; }

        public int Sex { get; set; }

        /// <summary>
        /// 生日
        /// </summary>
        public DateTime Birthday { get; set; }
    }
}
