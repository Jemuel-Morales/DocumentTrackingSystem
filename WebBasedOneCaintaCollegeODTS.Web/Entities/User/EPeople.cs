namespace DocumentTrackingSystem.Web.Entities.User
{
    public class EPeople : BaseEntity
    {
        public int UserId { get; set; }
        public EUser User { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public string FirstName { get; set; }
        public string Gender { get; set; }
        public DateTime DateOfBirth { get; set; }
    }
}
