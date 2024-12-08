using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Starshine.Authservice.Domain
{
    public class AllowedCorsOriginsCacheItem
    {
        public const string AllOrigins = "AllOrigins";

        public string[] AllowedOrigins { get; set; }
    }
}
