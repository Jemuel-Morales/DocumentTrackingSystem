namespace DocumentTrackingSystem.Web.Entities.User
{
    public class EUser : BaseEntity
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }

        public EPeople People { get; set; }

        public int RoleId { get; set; }
        public ERole Role { get; set; }
    }
}
