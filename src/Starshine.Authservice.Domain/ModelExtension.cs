using Starshine.Abp.IdentityServer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Starshine.Authservice.Domain
{
    internal static class ModelExtension
    {
        public static Client ToClientEntity(this Starshine.IdentityServer.Models.Client client,Guid id)
        {
            var clientEntity = new Client(id)
            {
                ClientId = client.ClientId,
                ClientName = client.ClientName,
                Description = client.Description,
                ClientUri = client.ClientUri,
                LogoUri = client.LogoUri,
                Enabled = client.Enabled,
                ProtocolType = client.ProtocolType,
                RequireClientSecret = client.RequireClientSecret,
                RequireConsent = client.RequireConsent,
                AllowRememberConsent = client.AllowRememberConsent,
                AlwaysIncludeUserClaimsInIdToken = client.AlwaysIncludeUserClaimsInIdToken,
                RequirePkce = client.RequirePkce,
                AllowPlainTextPkce = client.AllowPlainTextPkce,
                RequireRequestObject = client.RequireRequestObject,
                AllowAccessTokensViaBrowser = client.AllowAccessTokensViaBrowser,
                FrontChannelLogoutUri = client.FrontChannelLogoutUri,
                FrontChannelLogoutSessionRequired = client.FrontChannelLogoutSessionRequired,
                BackChannelLogoutUri = client.BackChannelLogoutUri,
                BackChannelLogoutSessionRequired = client.BackChannelLogoutSessionRequired,
                AllowOfflineAccess = client.AllowOfflineAccess,
                IdentityTokenLifetime = client.IdentityTokenLifetime,
                AllowedIdentityTokenSigningAlgorithms = string.Join(",", client.AllowedIdentityTokenSigningAlgorithms),
                AccessTokenLifetime = client.AccessTokenLifetime,
                AuthorizationCodeLifetime = client.AuthorizationCodeLifetime,
                ConsentLifetime = client.ConsentLifetime,
                AbsoluteRefreshTokenLifetime = client.AbsoluteRefreshTokenLifetime,
                SlidingRefreshTokenLifetime = client.SlidingRefreshTokenLifetime,
                RefreshTokenUsage = (int)client.RefreshTokenUsage,
                UpdateAccessTokenClaimsOnRefresh = client.UpdateAccessTokenClaimsOnRefresh,
                RefreshTokenExpiration = (int)client.RefreshTokenExpiration,
                AccessTokenType = (int)client.AccessTokenType,
                EnableLocalLogin = client.EnableLocalLogin,
                IncludeJwtId = client.IncludeJwtId,
                AlwaysSendClientClaims = client.AlwaysSendClientClaims,
                ClientClaimsPrefix = client.ClientClaimsPrefix,
                PairWiseSubjectSalt = client.PairWiseSubjectSalt,
                UserSsoLifetime = client.UserSsoLifetime,
                UserCodeType = client.UserCodeType,
                DeviceCodeLifetime = client.DeviceCodeLifetime,
                ConcurrencyStamp = Guid.NewGuid().ToString("N"),
            };
            if (client.AllowedScopes != null && client.AllowedScopes.Count > 0)
            {
                foreach (var item in client.AllowedScopes)
                {
                    clientEntity.AddScope(item);
                }
            }
            if (client.IdentityProviderRestrictions != null && client.IdentityProviderRestrictions.Count > 0)
            {
                foreach (var item in client.IdentityProviderRestrictions)
                {
                    clientEntity.AddIdentityProviderRestriction(item);
                }
            }
            if (client.AllowedCorsOrigins != null && client.AllowedCorsOrigins.Count > 0)
            {
                foreach (var item in client.AllowedCorsOrigins)
                {
                    clientEntity.AddCorsOrigin(item);
                }
            }
            if (client.AllowedGrantTypes != null && client.AllowedGrantTypes.Count > 0)
            {
                foreach (var item in client.AllowedGrantTypes)
                {
                    clientEntity.AddGrantType(item);
                }
            }
            if (client.AllowedScopes != null && client.AllowedScopes.Count > 0)
            {
                foreach (var item in client.AllowedScopes)
                {
                    clientEntity.AddScope(item);
                }
            }
            if (client.ClientSecrets != null && client.ClientSecrets.Count > 0)
            {
                foreach (var item in client.ClientSecrets)
                {
                    clientEntity.AddSecret(item.Value,item.Expiration,item.Type,item.Description);
                }
            }
            if (client.Claims != null && client.Claims.Count > 0)
            {
                foreach (var item in client.Claims)
                {
                    clientEntity.AddClaim(item.Type, item.Value);
                }
            }
            if (client.RedirectUris != null && client.RedirectUris.Count > 0)
            {
                foreach (var item in client.RedirectUris)
                {
                    clientEntity.AddRedirectUri(item);
                }
            }
            if (client.PostLogoutRedirectUris != null && client.PostLogoutRedirectUris.Count > 0)
            {
                foreach (var item in client.PostLogoutRedirectUris)
                {
                    clientEntity.AddPostLogoutRedirectUri(item);
                }
            }
            return clientEntity;
        }
    }
}
