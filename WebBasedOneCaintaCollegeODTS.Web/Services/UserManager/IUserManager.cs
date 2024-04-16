namespace DocumentTrackingSystem.Web.Services.UserManager
{
    public interface IUserManager
    {
        bool IsUsernameOrEmailExist(string usernameOrEmail);
        Task<bool> SignInAsync(HttpContext httpcontext, string usernameOrEmail, string password);
        string GetUserRole(HttpContext httpContext);

    }
}
