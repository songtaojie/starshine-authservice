using System;
using System.Collections.Generic;
using System.Text;

namespace Hx.IdentityServer.Model.Models.Account
{
    /// <summary>
    /// 用户明细信息
    /// </summary>
    public class AccountDetailModel
    {
        /// <summary>
        /// 用户id
        /// </summary>
        public string Id { get; set; }

        public string RealName { get; set; }

        public string UserName { get; set; }

        public string Email { get; set; }

        public string Sex { get; set; }
    }
}
