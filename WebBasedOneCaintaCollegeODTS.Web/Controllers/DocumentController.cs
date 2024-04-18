using DocumentTrackingSystem.Web.Models;
using DocumentTrackingSystem.Web.Models.Document;
using DocumentTrackingSystem.Web.Models.TrackingStatus;
using DocumentTrackingSystem.Web.Services.Document;
using DocumentTrackingSystem.Web.Services.Student;
using DocumentTrackingSystem.Web.Services.TrackingStatus;
using DocumentTrackingSystem.Web.Services.UserManager;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.IdentityModel.Tokens.Jwt;

namespace DocumentTrackingSystem.Web.Controllers
{
    [Authorize]
    public class DocumentController(IDocumentService _documentService, IStudentService studentService, ITrackingStatus trackingStatus, IUserManager userManager) : Controller
    {
        private readonly IDocumentService _documentService = _documentService;
        private readonly IStudentService _studentService = studentService;
        private readonly ITrackingStatus _trackingStatus = trackingStatus;
        private readonly IUserManager _userManager = userManager;

        [AllowAnonymous]
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
                var name = await _userManager.GetUserFullName(HttpContext);
                if (result)
                {
                    await _documentService.CreateAsync(new WriteDocumentVM
                    {
                        StudentNumber = model.StudentNumber,
                        Subject = model.Subject,
                        Description = model.Description,
                        TypeId = model.TypeId,
                        TrackingStatus = [new WriteTrackingStatusVM {
                            CreatedBy = name
                        }]
                    });
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
            ViewBag.NameOfModifier = await _userManager.GetUserFullName(HttpContext);

            if (!string.IsNullOrEmpty(trckNum))
            {
                var exist = await _documentService.IsTrackingNumberExist(trckNum);

                if (!exist) return Redirect("/Dashboard");

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

                var trckNum = await _documentService.GetDocumentTrackingNumberById(model.WriteTrackingStatus.DocumentEncryptId);
                var name = await _userManager.GetUserFullName(HttpContext);
                if (trckNum != null)
                {
                    await _trackingStatus.CreateAsync(new WriteTrackingStatusVM
                    {
                        DocumentEncryptId = model.WriteTrackingStatus.DocumentEncryptId,
                        StatusId = model.WriteTrackingStatus.StatusId,
                        CreatedBy = name,
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
