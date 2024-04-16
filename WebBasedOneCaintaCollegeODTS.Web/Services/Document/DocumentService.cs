using DocumentTrackingSystem.Web.Database;
using DocumentTrackingSystem.Web.Entities.Document;
using DocumentTrackingSystem.Web.Entities.TrackingStatus;
using DocumentTrackingSystem.Web.Models;
using DocumentTrackingSystem.Web.Models.Document;
using DocumentTrackingSystem.Web.Models.TrackingStatus;
using DocumentTrackingSystem.Web.Services.Config;
using DocumentTrackingSystem.Web.Services.Student;
using Microsoft.EntityFrameworkCore;

namespace DocumentTrackingSystem.Web.Services.Document
{
    public class DocumentService(AppDbContext context, IRouteProtector routeProtector, IStudentService studentService) : IDocumentService
    {
        private readonly AppDbContext _context = context;
        private readonly IRouteProtector _routeProtector = routeProtector;
        private readonly IStudentService _studentService = studentService;

        public async Task<bool> CreateAsync(WriteDocumentVM model)
        {
            try
            {
                int result = await _studentService.GetIdByStudentNumber(model.StudentNumber);
                await _context.Documents.AddAsync(new EDocument
                {
                    StudentId = result,
                    TypeId = model.TypeId,
                    Subject = model.Subject,
                    Description = model.Description,
                    TrackingStatus = [new ETrackingStatus {
                        Comments = "This is Auto-Generated",
                        ModifiedBy = "Computer",
                        StatusId = 1
                    }]
                });

                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        public async Task<IEnumerable<ReadDocumentVM>> GetAllDocuments()
        {
            var raw = await _context.Documents.Include(e => e.Student).Include(e => e.TrackingStatus).ThenInclude(e => e.Status).Include(e => e.Type).AsNoTracking().ToListAsync();

            var map = raw.Select(e => new ReadDocumentVM
            {
                EncryptedId = _routeProtector.Encode(e.Id),
                TrackingNumber = e.TrackingNumber,
                DocumentNumber = e.DocumentNumber,
                Subject = e.Subject,
                Description = e.Description,
                DateCreated = e.DateCreated,
                DateUpdated = e.DateUpdated,
                TypeName = e.Type.TypeName,
                Student = new ReadStudentVM
                {
                    StudentNumber = e.Student.StudentNumber,
                    FullName = e.Student.LastName + ", " + e.Student.FirstName + " " + e.Student.MiddleName.Substring(0, 1),
                    Gender = e.Student.Gender,
                    YearLevel = e.Student.YearLevel,
                    Semester = e.Student.Semester,
                    Course = e.Student.Course,
                    Age = new Age().Calculate(e.Student.DateOfBirth)
                },
                TrackingStatus = e.TrackingStatus.Select(e => new ReadTrackingStatus
                {
                    Id = e.Id,
                    Comments = e.Comments,
                    ModifiedBy = e.ModifiedBy,
                    StatusName = e.Status.StatusName,
                    DateCreated = e.DateCreated,
                    DateModified = e.DateModified
                }).ToList()
            });

            return map;
        }


        public async Task<ReadDocumentVM> GetByQRCode(string trackingNumber)
        {
            var raw = await _context.Documents.Include(e => e.Student).Include(e => e.TrackingStatus.OrderByDescending(e => e.DateCreated)).ThenInclude(e => e.Status).Include(e => e.Type).AsNoTracking().FirstOrDefaultAsync(e => e.TrackingNumber == trackingNumber.Trim());

            if (raw == null)
            {
                return null;
            }

            var map = new ReadDocumentVM
            {
                EncryptedId = _routeProtector.Encode(raw.Id),
                TrackingNumber = raw.TrackingNumber,
                DocumentNumber = raw.DocumentNumber,
                Subject = raw.Subject,
                Description = raw.Description,
                DateCreated = raw.DateCreated,
                DateUpdated = raw.DateUpdated,
                TypeName = raw.Type.TypeName,
                Student = new ReadStudentVM
                {
                    StudentNumber = raw.Student.StudentNumber,
                    FullName = raw.Student.LastName + ", " + raw.Student.FirstName + " " + raw.Student.MiddleName.Substring(0, 1),
                    Gender = raw.Student.Gender,
                    YearLevel = raw.Student.YearLevel,
                    Semester = raw.Student.Semester,
                    Course = raw.Student.Course,
                    Age = new Age().Calculate(raw.Student.DateOfBirth)
                },
                TrackingStatus = raw.TrackingStatus.Select(e => new ReadTrackingStatus
                {
                    Id = e.Id,
                    Comments = e.Comments,
                    ModifiedBy = e.ModifiedBy,
                    StatusName = e.Status.StatusName,
                    DateCreated = e.DateCreated,
                    DateModified = e.DateModified
                }).ToList()
            };

            return map;
        }
        public async Task<IEnumerable<ReadStatusVM>> GetAllStatus()
        {
            var raw = await _context.Status.AsNoTracking().ToListAsync();

            var map = raw.Select(e => new ReadStatusVM
            {
                Id = e.Id,
                StatusName = e.StatusName
            });

            return map;
        }

        public async Task<IEnumerable<ReadTypeVM>> GetAllTypes()
        {
            var raw = await _context.Types.AsNoTracking().ToListAsync();

            var map = raw.Select(e => new ReadTypeVM
            {
                Id = e.Id,
                TypeName = e.TypeName
            });

            return map;
        }




        public async Task<bool> IsValidDocumentId(string encryptedId)
        {
            try
            {
                var result = await _context.Documents.FirstOrDefaultAsync(e => e.Id == _routeProtector.Decode(encryptedId));

                if (result == null) return false;
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        public string GetDocumentTrackingNumberById(string encryptedId)
        {
            try
            {
                var result = _context.Documents.Where(e => e.Id == _routeProtector.Decode(encryptedId)).First().TrackingNumber;

                if (result != null)
                {
                    return result;
                }
            }
            catch (Exception)
            {

                return null;
            }

            return null;
        }
    }
}
