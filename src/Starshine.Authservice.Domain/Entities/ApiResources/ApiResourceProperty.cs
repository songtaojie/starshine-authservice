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
    public class ApiResourceProperty : Entity
    {
        public virtual Guid ApiResourceId { get; protected set; }

        public virtual string Key { get; set; }

        public virtual string Value { get; set; }

        protected ApiResourceProperty()
        {

        }

        public virtual bool Equals(Guid aiResourceId, [NotNull] string key, string value)
        {
            return ApiResourceId == aiResourceId && Key == key && Value == value;
        }

        protected internal ApiResourceProperty(Guid aiResourceId, [NotNull] string key, [NotNull] string value)
        {
            Check.NotNull(key, nameof(key));

            ApiResourceId = aiResourceId;
            Key = key;
            Value = value;
        }

        public override object[] GetKeys()
        {
            return [ApiResourceId, Key];
        }
    }
}
