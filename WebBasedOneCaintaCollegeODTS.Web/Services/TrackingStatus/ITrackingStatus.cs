using DocumentTrackingSystem.Web.Models.TrackingStatus;

namespace DocumentTrackingSystem.Web.Services.TrackingStatus
{
    public interface ITrackingStatus
    {
        Task<bool> CreateAsync(WriteTrackingStatusVM model);
    }
}
