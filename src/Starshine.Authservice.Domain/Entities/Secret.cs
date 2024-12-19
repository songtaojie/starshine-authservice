using IdentityServer4;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities;
using Volo.Abp;
using JetBrains.Annotations;

namespace Starshine.Authservice.Domain
{
    /// <summary>
    /// 秘密
    /// </summary>
    public abstract class Secret : Entity<Guid>
    {
        public virtual string Type { get; protected set; }

        public virtual string Value { get; set; }

        public virtual string Description { get; set; }

        public virtual DateTime? Expiration { get; set; }

        protected Secret()
        {

        }

        protected Secret(
            [NotNull] string value,
            DateTime? expiration = null,
            string type = IdentityServerConstants.SecretTypes.SharedSecret,
            string description = null)
        {
            Check.NotNull(value, nameof(value));

            Value = value;
            Expiration = expiration;
            Type = type;
            Description = description;
        }
    }
}
