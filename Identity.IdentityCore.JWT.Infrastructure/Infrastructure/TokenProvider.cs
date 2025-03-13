using Identity.IdentityCore.JWT.Models;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.JsonWebTokens;
using System;

namespace Identity.IdentityCore.JWT.Infrastructure
{

    /// <summary>
    /// Provides functionality to create JWT tokens for authenticated users.
    /// </summary>
    internal sealed class TokenProvider
    {
        private readonly int ExpirationInHours = 2;

        public string Create(User user)
        {
            string secretKey = Environment.GetEnvironmentVariable("JWT_SETTINGS_KEY");

            if (string.IsNullOrEmpty(secretKey))
            {
                throw new ArgumentNullException("JWT_SETTINGS_KEY", "JWT secret key is missing in environment variables.");
            }

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));

            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                    new Claim(JwtRegisteredClaimNames.Email, user.Email),
                    new Claim("email_verified", user.IsEmailVerified.ToString())
                    //TODO add roles here
                }),
                Expires = DateTime.UtcNow.AddHours(ExpirationInHours),
                SigningCredentials = credentials,
                Issuer = Environment.GetEnvironmentVariable("JWT_SETTINGS_ISSUER"),
                Audience = Environment.GetEnvironmentVariable("JWT_SETTINGS_AUDIENCE")
            };

            var handler = new JsonWebTokenHandler();
            string token = handler.CreateToken(tokenDescriptor);
            return token;
        }
    }
}