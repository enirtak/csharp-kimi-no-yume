using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace proj_csharp_kiminoyume.Services
{
    public class TokenService
    {
        private const int ExpirationMinutes = 30;
        private readonly IConfiguration _configuration;

        public TokenService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string CreateToken(IdentityUser user)
        {
            var expiration = DateTime.UtcNow.AddMinutes(ExpirationMinutes);
            var token = CreateJWTToken(
                CreateClaim(user),
                CreateSigningCredentials(),
                expiration
            );
            var tokenHandler = new JwtSecurityTokenHandler();
            return tokenHandler.WriteToken(token);
        }

        private JwtSecurityToken CreateJWTToken(List<Claim> claims, SigningCredentials credentials, DateTime expiration) =>
            new(
                _configuration["Jwt:ValidIssuer"],
                _configuration["Jwt:ValidAudience"],
                claims,
                expires: expiration,
                signingCredentials: credentials
                );

        private List<Claim> CreateClaim(IdentityUser user)
        {
            try
            {
                var claims = new List<Claim>
                {
                    // creates a claim w/ the subject of JWT
                    new Claim(JwtRegisteredClaimNames.Sub, _configuration["Jwt:Subject"]),
                   
                    // creates unique identifier claim (Jti) for JWT
                    // it assignes new GUID (Globally Unique Identifier) to each token
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    
                    // reates a claim with the issued at (IAT) timestamp of the JWT
                    // the date and time when the token was issued
                    new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString(CultureInfo.InvariantCulture)),
                    
                    // creates a claim with the user's unique identifier
                    new Claim(ClaimTypes.NameIdentifier, user.Id),

                    // creates a claim with the user's username or display name
                    new Claim(ClaimTypes.Name, user.UserName),

                    // creates a claim with the user's email address
                    new Claim(ClaimTypes.Email, user.Email)
                };

                return claims;
            }
            catch(Exception ex) {
                Console.WriteLine("Exception at CreateClaim | " + ex);
                throw;
            }
        }

        // creates object used to sign the JWT
        // and ensures its integrity during authentication & authorization process
        private SigningCredentials CreateSigningCredentials()
        {
            #pragma warning disable CS8604 // Possible null reference argument.

            // represents the credentials used to sign JWT 
            // included key & algo (HmacSha256)
            return new SigningCredentials(

                // represents a key used for cryptographic operations
                new SymmetricSecurityKey(

                    // retriebes JWT key and converts into a byte array
                    // key is used to sign the JWT and ensure its integrity
                    Encoding.UTF8.GetBytes(_configuration["Jwt:Key"])
                ),
                SecurityAlgorithms.HmacSha256
            );
        }
    }
}
