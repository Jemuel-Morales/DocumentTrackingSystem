using System.ComponentModel.DataAnnotations;

namespace DocumentTrackingSystem.Web.Models
{
    public class SignInVM
    {
        [Required(ErrorMessage = "Username or Email is required")]
        public required string  UsernameOrEmail { get; set; }
        [Required(ErrorMessage = "Password is required")]
        public required string Password { get; set; }
    }
}
