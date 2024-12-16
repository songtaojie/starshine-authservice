using Microsoft.EntityFrameworkCore;
using Starshine.Authservice.Domain;
using Starshine.Authservice.Domain.ApiResources;
using Starshine.Authservice.Domain.Clients;
using Starshine.Authservice.Domain.ApiScopes;
using Starshine.Authservice.Domain.IdentityResources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Starshine.Authservice.EntityFrameworkCore
{
    public static class StarshineAuthserviceEfCoreQueryableExtensions
    {
        public static IQueryable<ApiResource> IncludeDetails(this IQueryable<ApiResource> queryable, bool include = true)
        {
            if (!include)
            {
                return queryable;
            }

            return queryable
                .Include(x => x.Secrets)
                .Include(x => x.UserClaims)
                .Include(x => x.Scopes)
                .Include(x => x.Properties);
        }

        public static IQueryable<ApiScope> IncludeDetails(this IQueryable<ApiScope> queryable, bool include = true)
        {
            if (!include)
            {
                return queryable;
            }

            return queryable
                .Include(x => x.UserClaims)
                .Include(x => x.Properties);
        }


        public static IQueryable<IdentityResource> IncludeDetails(this IQueryable<IdentityResource> queryable, bool include = true)
        {
            if (!include)
            {
                return queryable;
            }

            return queryable
                .Include(x => x.UserClaims)
                .Include(x => x.Properties);
        }

        public static IQueryable<Client> IncludeDetails(this IQueryable<Client> queryable, bool include = true)
        {
            if (!include)
            {
                return queryable;
            }

            return queryable
                .Include(x => x.AllowedGrantTypes)
                .Include(x => x.RedirectUris)
                .Include(x => x.PostLogoutRedirectUris)
                .Include(x => x.AllowedScopes)
                .Include(x => x.ClientSecrets)
                .Include(x => x.Claims)
                .Include(x => x.IdentityProviderRestrictions)
                .Include(x => x.AllowedCorsOrigins)
                .Include(x => x.Properties);
        }
    }
}
