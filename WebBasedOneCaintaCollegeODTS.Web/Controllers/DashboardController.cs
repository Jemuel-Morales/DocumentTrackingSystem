using DocumentTrackingSystem.Web.Services.Document;
using Microsoft.AspNetCore.Mvc;

namespace DocumentTrackingSystem.Web.Controllers
{
    public class DashboardController(IDocumentService documentService) : Controller
    {
        private readonly IDocumentService _documentService = documentService;

        public async Task<IActionResult> Index()
        {
            var result = await _documentService.GetAllDocuments();
            return View(result);
        }
    }
}
