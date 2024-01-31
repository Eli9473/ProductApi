using Microsoft.IdentityModel.Tokens;
using Product.App.Contract.IServices;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Product.App.Services
{
    public class TokenService : ITokenService
    {
        public const string Key = "dgیش)asjhdgjassdghjkqwgheuiqgheu1یتاشu@y32173y982132hskj";
        private readonly JwtSecurityTokenHandler tokenHandler;
        private readonly SymmetricSecurityKey symmetricKey;
        public TokenService()
        {
            tokenHandler = new JwtSecurityTokenHandler();
            symmetricKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Key));
        }
        public string GenerateToken(string userId, List<string> roles)
        {
            var claims = new List<Claim>()
            {
                new Claim("userId", userId),
            };


            //var roleClaims = roles.Select(r => new Claim(ClaimTypes.Role, r));
            //claims.AddRange(roleClaims);

            roles.ForEach(r => { claims.Add(new Claim(ClaimTypes.Role, r)); });

            var descriptor = GenerateSecurityTokenDescriptor(claims);

            var jwtSecurityToken = tokenHandler.CreateJwtSecurityToken(descriptor);

            return tokenHandler.WriteToken(jwtSecurityToken);
        }

        public List<Claim> ValidateToken(string token)
        {
            var principal = tokenHandler.ValidateToken(token, new TokenValidationParameters
            {
                ValidateAudience = false,
                ValidateIssuer = false,
                RequireExpirationTime = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = symmetricKey

            }, out SecurityToken securityToken);

            return principal.Claims.ToList();
        }
        private SecurityTokenDescriptor GenerateSecurityTokenDescriptor(List<Claim> claims)
        {
            var descriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(claims),
                Issuer = "http://www.Niazi.com",
                Audience = "http://www.Backend.Niazi.com",
                SigningCredentials = new SigningCredentials(symmetricKey, SecurityAlgorithms.HmacSha256Signature)
            };
            return descriptor;
        }
    }
}
