using DocumentTrackingSystem.Web.Services.Document;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DocumentTrackingSystem.Web.Controllers
{
    [Authorize]
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
