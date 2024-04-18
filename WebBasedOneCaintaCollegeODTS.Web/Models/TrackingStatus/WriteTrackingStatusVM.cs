namespace DocumentTrackingSystem.Web.Models.TrackingStatus
{
    public class WriteTrackingStatusVM
    {
        public string DocumentEncryptId { get; set; }
        public string Comments { get; set; }
        public int StatusId { get; set; }
        public string CreatedBy { get; set; }
    }
}
