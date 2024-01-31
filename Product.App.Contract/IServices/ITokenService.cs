using System.Security.Claims;

namespace Product.App.Contract.IServices
{
    public interface ITokenService
    {
        string GenerateToken(string userId, List<string> roles);
        List<Claim> ValidateToken(string token);
    }
}
