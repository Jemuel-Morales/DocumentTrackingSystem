using Microsoft.AspNetCore.DataProtection;

namespace DocumentTrackingSystem.Web.Services.Config
{
    public class RouteProtector(IDataProtectionProvider protectionProvider) : IRouteProtector
    {
        private readonly IDataProtector _protect = protectionProvider.CreateProtector("strong key here");
        public int Decode(string encryptId)
        {
            return Convert.ToInt32(_protect.Unprotect(encryptId));
        }

        public string Encode(int routeId)
        {
            return _protect.Protect(routeId.ToString());
        }
    }
}
