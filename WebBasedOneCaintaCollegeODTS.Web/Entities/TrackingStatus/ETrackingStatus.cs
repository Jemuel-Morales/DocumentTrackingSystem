using DocumentTrackingSystem.Web.Entities.Document;

namespace DocumentTrackingSystem.Web.Entities.TrackingStatus
{
    public class ETrackingStatus : BaseEntity
    {
        public int DocumentId { get; set; }
        public EDocument Document { get; set; }
        public string Comments { get; set; }
        public int StatusId { get; set; } = 0;
        public EStatus Status { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime DateCreated { get; private set; } = DateTime.Now;

    }
}
