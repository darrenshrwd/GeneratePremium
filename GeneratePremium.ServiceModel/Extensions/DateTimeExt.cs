using System;

namespace GeneratePremium.ServiceModel.Extensions
{
    public static class DateTimeExt
    {
        public static int CalculateAge(this DateTime dateOfBirth)
        {
            var age = DateTime.Now.Year - dateOfBirth.Year;
            if (DateTime.Now.DayOfYear < dateOfBirth.DayOfYear)
                age = age - 1;

            return age;
        }
    }
}
