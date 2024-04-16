namespace DocumentTrackingSystem.Web.Services.Config
{
    public interface IRouteProtector
    {
        string Encode(int routeId);
        int Decode(string encryptId);
    }
}
