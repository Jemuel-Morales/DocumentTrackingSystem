using DocumentTrackingSystem.Web.Entities.Document;

namespace DocumentTrackingSystem.Web.Entities
{
    public class EStudent : BaseEntity
    {
        public string StudentNumber { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public string FirstName { get; set; }
        public string Gender { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Course { get; set; }
        public int YearLevel { get; set; }
        public int Semester { get; set; }

        public ICollection<EDocument> Documents { get; set; }
    }
}
