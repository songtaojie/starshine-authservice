using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hx.IdentityServer.Entity
{
    public class AspNetUserTokens: IdentityUserToken<string>
    {
        [MaxLength(100)]
        public override string LoginProvider { get => base.LoginProvider; set => base.LoginProvider = value; }

        [MaxLength(100)]
        public override string Name { get => base.Name; set => base.Name = value; }
    }
}
