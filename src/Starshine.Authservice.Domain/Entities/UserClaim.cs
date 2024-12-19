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
    /// 用户声明
    /// </summary>
    public abstract class UserClaim : Entity<Guid>
    {
        
        /// <summary>
        /// 类型
        /// </summary>
        public virtual string Type { get; protected set; }

        protected UserClaim()
        {

        }

        protected UserClaim([NotNull] string type)
        {
            Check.NotNull(type, nameof(type));
            Type = type;
        }
    }
}
