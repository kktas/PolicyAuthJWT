using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace PolicyAuthJWT.Config.Auth.JwtService
{
    public class JwtService : IJwtService
    {
        private readonly string _secretKey;
        private readonly string _issuer;
        private readonly string _audience;
        private readonly int _expirationInMinutes;
        private readonly int _longExpirationInMinutes;

        public JwtService(string secretKey, string issuer, string audience, int expirationInMinutes, int longExpirationInMinutes)
        {
            _secretKey = secretKey;
            _issuer = issuer;
            _audience = audience;
            _expirationInMinutes = expirationInMinutes;
            _longExpirationInMinutes = longExpirationInMinutes;
        }

        public string GenerateToken(string userId, string username, string[] roles, bool keepLoggedIn)
        {
            var expirationMinutes = keepLoggedIn ? _longExpirationInMinutes : _expirationInMinutes;
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, userId),
                new Claim(ClaimTypes.Name, username),
                new Claim(ClaimTypes.Role, string.Join(",",roles)),
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_secretKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                _issuer,
                _audience,
                claims,
                expires: DateTime.Now.AddMinutes(expirationMinutes),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
