using IdentityModel.Client;
using System.Threading.Tasks;

namespace ComplaintService.cs.Interface
{
    public interface IGenerateToken
    {
        Task<TokenResponse> GetToken(string scope);
    }
}
