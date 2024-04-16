using DocumentTrackingSystem.Web.Database;
using DocumentTrackingSystem.Web.Models.User;
using DocumentTrackingSystem.Web.Services.Config;
using DocumentTrackingSystem.Web.Services.Jwt;
using Microsoft.EntityFrameworkCore;

namespace DocumentTrackingSystem.Web.Services.UserManager
{
    public class UserManager(AppDbContext context, IJwtService jwtService, IRouteProtector routeProtector) : IUserManager
    {
        private readonly AppDbContext _context = context;
        private readonly IJwtService _jwtService = jwtService;
        private readonly IRouteProtector _routeProtector = routeProtector;

        public bool IsUsernameOrEmailExist(string usernameoremail)
        {
            return _context.Users.Any(e => e.Email == usernameoremail || e.Username == usernameoremail);
        }

        public async Task<bool> SignInAsync(HttpContext httpcontext, string usernameoremail, string password)
        {
            var user = await _context.Users.Include(e => e.Role).Include(e => e.People).Select(e => new { e.Id, e.Username, e.Password, e.Email, e.People.LastName, e.People.MiddleName, e.People.FirstName, e.Role.RoleName }).FirstOrDefaultAsync(e => (e.Username == usernameoremail || e.Email == usernameoremail) && e.Password == password);

            if (user == null)
            {
                return false;
            }

            //generate token
            var jwtToken = _jwtService.GenerateToken(new ReadUserVM
            {
                EncryptedId = _routeProtector.Encode(user.Id),
                Username = user.Username,
                People = new ReadPeopleVM()
                {
                    FullName = user.LastName + user.FirstName + user.MiddleName
                },
                Role = new ReadRoleVM()
                {
                    RoleName = user.RoleName
                }
            });

            httpcontext.Response.Cookies.Append("token", jwtToken, new CookieOptions
            {
                Expires = DateTime.Now.AddHours(10),
                HttpOnly = true,
                Secure = true,
                IsEssential = true,
                SameSite = SameSiteMode.None
            });

            return true;
        }

        public string GetUserRole(HttpContext httpContext)
        {
            return _jwtService.GetUserClaims(httpContext.Request.Cookies["token"]).First(c => c.Type == "Role").Value;
        }
    }
}
