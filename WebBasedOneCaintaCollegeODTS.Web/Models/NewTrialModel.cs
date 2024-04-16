using DocumentTrackingSystem.Web.Models.Document;
using DocumentTrackingSystem.Web.Models.TrackingStatus;

namespace DocumentTrackingSystem.Web.Models
{
    public class NewTrialModel
    {
        public WriteTrackingStatusVM WriteTrackingStatus { get; set; }
        public ReadDocumentVM ReadDocument { get; set; }
    }
}
