using DocumentTrackingSystem.Web.Models.TrackingStatus;

namespace DocumentTrackingSystem.Web.Models.Document
{
    public class ReadDocumentVM : BaseVM
    {
        public string TrackingNumber { get; set; }
        public string DocumentNumber { get; set; }
        public string Subject { get; set; }
        public string Description { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateUpdated { get; set; }

        public ReadStudentVM Student { get; set; }
        public string TypeName { get; set; }

        public ICollection<ReadTrackingStatus> TrackingStatus { get; set; }

        public string FormatTimeInterval(DateTime oldDate)
        {
            // Get current date and time
            DateTime currentDate = DateTime.Now;

            // Calculate the time interval
            TimeSpan timeDifference = currentDate - oldDate;

            // Calculate total days, weeks, months, and years
            int totalDays = (int)timeDifference.TotalDays;
            int totalWeeks = totalDays / 7;
            int totalMonths = (int)(totalDays / 30.44); // Average number of days in a month
            int totalYears = (int)(totalDays / 365.25); // Average number of days in a year

            // Check for each interval
            if (totalYears > 0)
                return $"{totalYears} year{(totalYears > 1 ? "s" : "")} ago";
            else if (totalMonths > 0)
                return $"{totalMonths} month{(totalMonths > 1 ? "s" : "")} ago";
            else if (totalWeeks > 0)
                return $"{totalWeeks} week{(totalWeeks > 1 ? "s" : "")} ago";
            else if (totalDays > 0)
                return $"{totalDays} day{(totalDays > 1 ? "s" : "")} ago";
            else if (timeDifference.Hours > 0)
                return $"{timeDifference.Hours} hour{(timeDifference.Hours > 1 ? "s" : "")} ago";
            else if (timeDifference.Minutes > 0)
                return $"{timeDifference.Minutes} minute{(timeDifference.Minutes > 1 ? "s" : "")} ago";
            else
                return "Just now";
        }
    }
}
