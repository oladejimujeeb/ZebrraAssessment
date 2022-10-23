using IdentityModel;
using IdentityServer4;
using IdentityServer4.Models;
using IdentityServer4.Test;
using System.Collections.Generic;
using System.Security.Claims;

namespace IdenitityServer.Configure
{
    public class AuthenticationService
    {
        public static IEnumerable<IdentityResource> IdentityResources =>
        new IdentityResource[]
        {
            new IdentityResources.OpenId(),
            new IdentityResources.Profile(),
        };
        public static List<TestUser> TestUsers =>
        new List<TestUser>
        {
            new TestUser
            {
                SubjectId = "a0f25-b964-409f-bloge-c9249b4",
                Username = "mujib",
                Password = "zebrra",
                Claims =
                {
                    new Claim(JwtClaimTypes.Name, "Oladeji Mujib"),
                    new Claim(JwtClaimTypes.GivenName, "Mulad"),
                    new Claim(JwtClaimTypes.FamilyName, "Oladeji"),
                    new Claim(JwtClaimTypes.WebSite, "https://oladejimujib@gmail.com"),
                    new Claim(JwtClaimTypes.Issuer, "Mulad"),
                },
            }
        };

        public static IEnumerable<Client> Clients =>
           new Client[]
           {
                new Client
                {
                    ClientId = "complaint.client",
                    ClientName = "Client Credentials Client",
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPasswordAndClientCredentials,
                    ClientSecrets = { new Secret("secret".Sha256()) },
                    AllowedScopes = { IdentityServerConstants.StandardScopes.OpenId,"Complain","Product" },
                    AccessTokenLifetime = 900
                },

                new Client
                {
                    ClientId ="MVCClient",
                    ClientName = "MVC Client",
                    ClientSecrets = {new Secret ("secret".Sha256()) },
                    AllowedGrantTypes= GrantTypes.ResourceOwnerPasswordAndClientCredentials,
                    AllowOfflineAccess = true,
                    AllowedScopes ={IdentityServerConstants.StandardScopes.OpenId, IdentityServerConstants.StandardScopes.Profile,"Complain"},
                    RequirePkce = true,
                    RequireConsent = true,
                    AllowPlainTextPkce = false
                }
           };
       
        public static IEnumerable<ApiScope> ApiScopes => new ApiScope[]
        {
           new ApiScope("Complain"),
           new ApiScope("Product")
        };

        public static IEnumerable<ApiResource> ApiResources()
        {
            ApiResource[] apiResources =
            {
                new ApiResource("myApi")
                {
                    Scopes = new List<string>{ "Complain","Product" },
                    ApiSecrets = new List<Secret>{ new Secret("supersecret".Sha256()) },

                }
            };
            return apiResources;
        }
    }
}
