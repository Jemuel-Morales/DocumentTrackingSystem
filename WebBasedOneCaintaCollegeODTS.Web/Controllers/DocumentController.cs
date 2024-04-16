using DocumentTrackingSystem.Web.Models;
using DocumentTrackingSystem.Web.Models.Document;
using DocumentTrackingSystem.Web.Models.TrackingStatus;
using DocumentTrackingSystem.Web.Services.Document;
using DocumentTrackingSystem.Web.Services.Student;
using DocumentTrackingSystem.Web.Services.TrackingStatus;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DocumentTrackingSystem.Web.Controllers
{
    public class DocumentController(IDocumentService _documentService, IStudentService studentService, ITrackingStatus trackingStatus) : Controller
    {
        private readonly IDocumentService _documentService = _documentService;
        private readonly IStudentService _studentService = studentService;
        private readonly ITrackingStatus _trackingStatus = trackingStatus;

        public async Task<IActionResult> Index(string trckNum)
        {
            if (!string.IsNullOrEmpty(trckNum))
            {
                var result = await _documentService.GetByQRCode(trckNum);

                if (result != null)
                {
                    return View(result);
                }
            }
            //if login
            //redirect to dashboard
            return Redirect("/");
        }

        // GET: DocumentController/Create
        public async Task<IActionResult> Create()
        {
            IEnumerable<ReadTypeVM> listOfTypes = await _documentService.GetAllTypes();
            ViewBag.Types = listOfTypes.Select(e => new SelectListItem()
            {
                Text = e.TypeName,
                Value = e.Id.ToString()
            }).ToList();
            return View();
        }

        // POST: DocumentController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(WriteDocumentVM model)
        {
            try
            {
                bool result = await _studentService.IsValidStudentNumber(model.StudentNumber);

                if (result)
                {
                    await _documentService.CreateAsync(model);
                }

                return Redirect("/Document");
            }
            catch
            {
                return View();
            }
        }

        public async Task<IActionResult> NewTrail(string trckNum)
        {
            IEnumerable<ReadStatusVM> listOfStatus = await _documentService.GetAllStatus();

            ViewBag.Status = listOfStatus.Select(e => new SelectListItem()
            {
                Text = e.StatusName,
                Value = e.Id.ToString()
            }).ToList();

            if (!string.IsNullOrEmpty(trckNum))
            {
                var result = await _documentService.GetByQRCode(trckNum);



                if (result != null)
                {
                    var map = new NewTrialModel
                    {
                        ReadDocument = result
                    };
                    return View(map);
                }
            }

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> NewTrail(NewTrialModel model)
        {
            try
            {
                var trckNum = _documentService.GetDocumentTrackingNumberById(model.WriteTrackingStatus.DocumentEncryptId);

                if (trckNum != null)
                {
                    await _trackingStatus.CreateAsync(new WriteTrackingStatusVM
                    {
                        DocumentEncryptId = model.WriteTrackingStatus.DocumentEncryptId,
                        StatusId = model.WriteTrackingStatus.StatusId,
                        ModifiedBy = model.WriteTrackingStatus.ModifiedBy,
                        Comments = model.WriteTrackingStatus.Comments
                    });
                    return Redirect($"/Document/NewTrail?trckNum={trckNum}");
                }
            }
            catch (Exception)
            {
                return Redirect("/Dashboard");
            }

            return Redirect("/Dashboard");
        }
    }
}
