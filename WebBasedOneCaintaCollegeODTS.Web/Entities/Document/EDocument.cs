using DocumentTrackingSystem.Web.Entities.TrackingStatus;

namespace DocumentTrackingSystem.Web.Entities.Document
{
    public class EDocument : BaseEntity
    {
        public int StudentId { get; set; }
        public string TrackingNumber { get; private set; } = GenerateTrackingNumber();
        public string DocumentNumber { get; private set; } = GenerateDocumentNumber();
        public string Subject { get; set; }
        public string Description { get; set; }
        public DateTime DateCreated { get; private set; } = DateTime.Now;
        public DateTime DateUpdated { get; set; } = DateTime.Now;
        public int TypeId { get; set; }

        public EStudent Student { get; set; }
        public EType Type { get; set; }

        public ICollection<ETrackingStatus> TrackingStatus { get; set; }

        private static string GenerateTrackingNumber()
        {
            DateTime now = DateTime.Now;
            string year = now.ToString("yy");
            string month = now.ToString("MM");
            string day = now.ToString("dd");
            string militaryTime = now.ToString("HH") + now.ToString("mm") + now.ToString("ss");
            string milliseconds = now.ToString("ffff");
            //9912312400609999
            return $"{year}{month}{day}{militaryTime}{milliseconds}";
        }

        private static string GenerateDocumentNumber()
        {
            DateTime now = DateTime.Now;
            string year = now.ToString("yyyy");
            string month = now.ToString("MM");
            string day = now.ToString("dd");
            string militaryTime = now.ToString("HH") + now.ToString("mm");
            string milliseconds = now.ToString("ffff");
            //9999-1231-2400-9999
            return $"{year}-{month}{day}-{militaryTime}-{milliseconds}";
        }
    }
}
