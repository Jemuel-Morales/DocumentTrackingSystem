namespace DocumentTrackingSystem.Web.Services.Config
{
    public class Age
    {
        public int Calculate(DateTime dateOfBirth)
        {
            DateTime targetDate = new DateTime(2024, 12, 31);
            int age = targetDate.Year - dateOfBirth.Year;

            if (dateOfBirth.DayOfYear > targetDate.DayOfYear)
            {
                age--;
            }

            return age;
        }
    }
}
