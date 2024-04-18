using DocumentTrackingSystem.Web.Database;
using DocumentTrackingSystem.Web.Entities.TrackingStatus;
using DocumentTrackingSystem.Web.Models.TrackingStatus;
using DocumentTrackingSystem.Web.Services.Config;

namespace DocumentTrackingSystem.Web.Services.TrackingStatus
{
    public class TrackingStatus(AppDbContext context, IRouteProtector routeProtector) : ITrackingStatus
    {
        private readonly AppDbContext _context = context;
        private readonly IRouteProtector _routeProtector = routeProtector;

        public async Task<bool> CreateAsync(WriteTrackingStatusVM model)
        {
            try
            {

                await _context.TrackingStatus.AddAsync(new ETrackingStatus
                {
                    DocumentId = _routeProtector.Decode(model.DocumentEncryptId),
                    StatusId = model.StatusId,
                    Comments = model.Comments,
                    CreatedBy = model.CreatedBy
                });

                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }
    }
}
