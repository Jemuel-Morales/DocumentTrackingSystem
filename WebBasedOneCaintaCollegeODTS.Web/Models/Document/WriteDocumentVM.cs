using DocumentTrackingSystem.Web.Models.TrackingStatus;
using System.ComponentModel.DataAnnotations;

namespace DocumentTrackingSystem.Web.Models.Document
{
    public class WriteDocumentVM
    {
        [Display(Name = "Student Number")]
        public string StudentNumber { get; set; }
        public string Subject { get; set; }
        public string Description { get; set; }
        public int TypeId { get; set; }

        public ICollection<WriteTrackingStatusVM> TrackingStatus { get; set; }
    }
}
