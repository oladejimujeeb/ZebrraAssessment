using ComplaintService.cs.Interface;
using IdentityModel.Client;
using Microsoft.Extensions.Logging;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace ComplaintService.cs.Service
{
    public class GenerateToken : IGenerateToken
    {
        private DiscoveryDocumentResponse _discDocument { get; set; }
        private readonly ILogger<GenerateToken> _logger;
        public GenerateToken(ILogger<GenerateToken> logger)
        {
            using var httpclient = new HttpClient();
            _logger = logger;
            
            _discDocument = httpclient.GetDiscoveryDocumentAsync("https://localhost:5001/.well-known/openid-configuration").Result;
            
        }
        public async Task<TokenResponse> GetToken(string scope)
        {
            using HttpClient client = new();
            var tokenResponse = await client.RequestClientCredentialsTokenAsync
                (
                    new ClientCredentialsTokenRequest
                    {
                        ClientSecret = "secret",
                        Address = _discDocument.TokenEndpoint,
                        ClientId = "MVCClient",
                        Scope = scope,
                    }
                );

            if (tokenResponse.IsError)
            {
                _logger.LogError(tokenResponse.HttpErrorReason);
                throw new Exception($"Token Error {tokenResponse.ErrorType}");
            }
                
            return tokenResponse;
        }
    }
}
