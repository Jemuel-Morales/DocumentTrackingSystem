namespace DocumentTrackingSystem.Web.Models.User
{
    public class ReadUserVM : BaseVM
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }

        public ReadPeopleVM People { get; set; }
        public ReadRoleVM Role { get; set; }
    }
}
