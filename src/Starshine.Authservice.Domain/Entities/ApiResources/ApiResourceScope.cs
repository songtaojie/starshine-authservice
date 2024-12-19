using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities;
using Volo.Abp;
using JetBrains.Annotations;

namespace Starshine.Authservice.Domain.ApiResources
{
    public class ApiResourceScope : Entity<Guid>
    {
        public virtual Guid ApiResourceId { get; protected set; }

        public virtual string Scope { get; set; }

        protected ApiResourceScope()
        {

        }

        public virtual bool Equals(Guid apiResourceId, [NotNull] string scope)
        {
            return ApiResourceId == apiResourceId && Scope == scope;
        }

        protected internal ApiResourceScope(
            Guid apiResourceId,
            [NotNull] string scope)
        {
            Check.NotNull(scope, nameof(scope));

            ApiResourceId = apiResourceId;
            Scope = scope;
        }

        public override object[] GetKeys()
        {
            return [ApiResourceId, Scope];
        }
    }
}
