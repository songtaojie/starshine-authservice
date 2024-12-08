using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Starshine.Authservice.Domain.ApiResources
{
    public class ApiResourceClaim : UserClaim
    {
        public virtual Guid ApiResourceId { get; set; }

        protected ApiResourceClaim()
        {

        }

        public virtual bool Equals(Guid apiResourceId, [NotNull] string type)
        {
            return ApiResourceId == apiResourceId && Type == type;
        }

        protected internal ApiResourceClaim(Guid apiResourceId, [NotNull] string type)
            : base(type)
        {
            ApiResourceId = apiResourceId;
        }

        public override object[] GetKeys()
        {
            return [ApiResourceId, Type];
        }
    }
}
