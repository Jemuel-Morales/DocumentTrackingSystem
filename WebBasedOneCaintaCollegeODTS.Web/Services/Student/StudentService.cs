
using DocumentTrackingSystem.Web.Database;
using Microsoft.EntityFrameworkCore;

namespace DocumentTrackingSystem.Web.Services.Student
{
    public class StudentService(AppDbContext context) : IStudentService
    {
        private readonly AppDbContext _context = context;

        public async Task<int> GetIdByStudentNumber(string studentNumber)
        {
            var result = await _context.Students.FirstOrDefaultAsync(e => e.StudentNumber == studentNumber.Trim());

            if (result != null)
            {
                return result.Id;
            }

            return 0;
        }

        public async Task<bool> IsValidStudentNumber(string studentNumber)
        {
            try
            {
                var result = await _context.Students.FirstOrDefaultAsync(e => e.StudentNumber == studentNumber.Trim());
                if (result == null)
                {
                    return false;
                }
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }
    }
}
