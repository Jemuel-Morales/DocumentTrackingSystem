using DocumentTrackingSystem.Web.Models;
using DocumentTrackingSystem.Web.Services.UserManager;
using Microsoft.AspNetCore.Mvc;

namespace DocumentTrackingSystem.Web.Controllers
{
    public class AccountController(IUserManager userManager) : Controller
    {
        private readonly IUserManager _userManager = userManager;

        public IActionResult Index()
        {
            if (HttpContext.Request.Cookies.Keys.Contains("token")) return Redirect("/Dashboard");

            return Redirect("/Account/Login");
        }

        public IActionResult Login()
        {
            if (HttpContext.Request.Cookies.Keys.Contains("token")) return Redirect("/Dashboard");
            return View();
        }

        public IActionResult Logout()
        {
            HttpContext.Response.Cookies.Delete("token");
            return Redirect("/");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(SignInVM response)
        {
            if (!_userManager.IsUsernameOrEmailExist(response.UsernameOrEmail))
            {
                ModelState.AddModelError("UsernameOrEmail", "Username or email doesn't exist");
                return View();
            }

            if (await _userManager.SignInAsync(HttpContext, response.UsernameOrEmail, response.Password))
            {
                return Redirect("/Dashboard");
            }
            return View();
        }
    }
}
