namespace DocumentTrackingSystem.Web.Services.Student
{
    public interface IStudentService
    {
        Task<bool> IsValidStudentNumber(string studentNumber);
        Task<int> GetIdByStudentNumber(string studentNumber);
    }
}
