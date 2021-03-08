using System;
using System.Collections.Generic;
using System.Text;

namespace Hx.IdentityServer.Model.Account
{
    public class UserPageModel
    {
        public string Id { get; set; }

        public string UserName { get; set; }

        public string RealName { get; set; }

        public DateTime CreateTime { get; set; }

        public int AccessFailedCount { get; set; }
    }
}
