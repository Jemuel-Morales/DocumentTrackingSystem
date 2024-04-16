using DocumentTrackingSystem.Web.Models.Document;

namespace DocumentTrackingSystem.Web.Services.Document
{
    public interface IDocumentService
    {
        Task<bool> CreateAsync(WriteDocumentVM model);
        Task<IEnumerable<ReadDocumentVM>> GetAllDocuments();
        Task<ReadDocumentVM> GetByQRCode(string trackingNumber);
        Task<bool> IsTrackingNumberExist(string trackingNumber);
        string GetDocumentTrackingNumberById(string encryptedId);
        Task<IEnumerable<ReadStatusVM>> GetAllStatus();
        Task<IEnumerable<ReadTypeVM>> GetAllTypes();

        Task<bool> IsValidDocumentId(string encryptedId);
    }
}
