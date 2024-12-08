using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Starshine.Authservice.Domain
{
    public class AspNetUserLogins : IdentityUserLogin<string>
    {
        [MaxLength(100)]
        public override string LoginProvider { get => base.LoginProvider; set => base.LoginProvider = value; }
        [MaxLength(36)]
        public override string ProviderKey { get => base.ProviderKey; set => base.ProviderKey = value; }
    }
}
