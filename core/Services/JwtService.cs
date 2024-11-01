using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using core.Data;
using core.Data.Contexts;
using core.Data.Outbound;
using core.Errors;
using Microsoft.IdentityModel.Tokens;

namespace core.Services
{
    public class JwtService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly JwtSecurityTokenHandler _tokenHandler;
        private readonly SymmetricSecurityKey _securityKey;
        private readonly SigningCredentials _credentials;

        public JwtService(
            IServiceProvider serviceProvider,
            JwtSettings jwtSettings,
            JwtSecurityTokenHandler tokenHandler)
        {
            _serviceProvider = serviceProvider;
            _tokenHandler = tokenHandler;

            _securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.SecretKey));
            _credentials = new SigningCredentials(_securityKey, SecurityAlgorithms.HmacSha512);
        }

        public OutboundToken GenerateToken(LoginResult.Succeeded auth)
        {
            var claims = new List<Claim>
            {
                new("UserId", auth.UserId.ToString()),
                new("UserName", auth.UserName),
            };

            var SecToken = new JwtSecurityToken(
                "Sigmon AI",
                audience: null,
                claims: claims,
                expires: DateTime.Now.AddHours(8),
                signingCredentials: _credentials);
            
            var token = _tokenHandler.WriteToken(SecToken);

            System.Console.WriteLine(token);

            return new OutboundToken(token);
        }

        public void ValidateToken(string jwt)
        {
            ClaimsPrincipal? claims;

            try
            {
                claims = _tokenHandler.ValidateToken(jwt,
                        new TokenValidationParameters()
                        {
                            ValidateIssuer = true,
                            ValidateLifetime = true,
                            ValidateIssuerSigningKey = true,
                            ValidateAudience = false,
                            ValidIssuer = "Sigmon AI",
                            IssuerSigningKey = _securityKey
                        },
                        out var validatedToken);
            }
            catch (SecurityTokenException ex)
            {
                throw new InvalidTokenException("Unable to validate token and its claims.", ex);
            }

            using var scope = _serviceProvider.CreateScope();
            var userContext = scope.ServiceProvider.GetRequiredService<UserContext>();
            
            userContext.Fill(new ContextData
            {
                UserId = Guid.Parse(claims.FindFirst("UserId")!.Value),
                UserName = claims.FindFirst("UserName")!.Value,
            });
        }
    }
}