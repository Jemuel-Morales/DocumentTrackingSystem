namespace DocumentTrackingSystem.Web.Models.TrackingStatus
{
    public class ReadTrackingStatus
    {
        public int Id { get; set; }
        public string Comments { get; set; }
        public string StatusName { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }
    }
}
