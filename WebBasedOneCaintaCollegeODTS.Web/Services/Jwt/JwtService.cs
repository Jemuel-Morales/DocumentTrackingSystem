
using DocumentTrackingSystem.Web.Models.User;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace DocumentTrackingSystem.Web.Services.Jwt
{
    public class JwtService(IConfiguration configuration) : IJwtService
    {
        private readonly IConfiguration _config = configuration;
        public string GenerateToken(ReadUserVM user)
        {
            var claims = new ClaimsIdentity(new Claim[]
            {
                new Claim("ID", user.EncryptedId),
                new Claim("Username", user.Username == null ?user.Email! : user.Username!),
                new Claim("Name", user.People.FullName),
                new Claim("Role", user.Role.RoleName)
            });

            var secretKey = Encoding.ASCII.GetBytes(_config.GetSection("Jwt:Secret").Value!);

            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = claims,
                Expires = DateTime.Now.AddHours(10),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(secretKey), SecurityAlgorithms.HmacSha256Signature)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var jwt = tokenHandler.WriteToken(token);

            return jwt;
        }

        public IEnumerable<Claim> GetUserClaims(string tokenString)
        {
            var token = new JwtSecurityTokenHandler().ReadJwtToken(tokenString);
            return token.Claims;
        }
    }
}
