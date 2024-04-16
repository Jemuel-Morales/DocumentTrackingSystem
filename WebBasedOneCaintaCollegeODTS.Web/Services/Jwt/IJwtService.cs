using DocumentTrackingSystem.Web.Models.User;
using System.Security.Claims;

namespace DocumentTrackingSystem.Web.Services.Jwt
{
    public interface IJwtService
    {
        string GenerateToken(ReadUserVM user);
        IEnumerable<Claim> GetUserClaims(string tokenString);
    }
}
