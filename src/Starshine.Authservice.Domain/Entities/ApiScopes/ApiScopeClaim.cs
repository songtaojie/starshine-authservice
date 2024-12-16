using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Starshine.Authservice.Domain.ApiScopes
{
    public class ApiScopeClaim : UserClaim
    {
        public Guid ApiScopeId { get; protected set; }

        protected ApiScopeClaim()
        {

        }

        public virtual bool Equals(Guid apiScopeId, [NotNull] string type)
        {
            return ApiScopeId == apiScopeId && Type == type;
        }

        protected internal ApiScopeClaim(Guid apiScopeId, [NotNull] string type)
            : base(type)
        {
            ApiScopeId = apiScopeId;
        }

        public override object[] GetKeys()
        {
            return new object[] { ApiScopeId, Type };
        }
    }
}
