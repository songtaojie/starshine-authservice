using System;
using System.Collections.Generic;
using System.Text;

namespace Hx.IdentityServer.Model.Account
{
    public class UserPageModel
    {
        public string UserName { get; set; }

        public string LoginName { get; set; }

        public DateTime CreateTime { get; set; }

        public int AccessFailedCount { get; set; }
    }
}
