using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Starshine.Authservice.Domain.Shared
{
    public class AbpClaimsServiceOptions
    {
        public List<string> RequestedClaims { get; }

        public AbpClaimsServiceOptions()
        {
            RequestedClaims = new List<string>();
        }
    }
}
