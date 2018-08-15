using System;

namespace GeneratePremium.ServiceModel.Extensions
{
    public static class DateTimeExt
    {
        public static int CalculateAge(this DateTime dateOfBirth)
        {
            var today = DateTime.Today;
            var age = today.Year - dateOfBirth.Year;

            // Go back to the year the person was born in case of a leap year
            if (dateOfBirth > today.AddYears(-age)) age--;
            return age;
        }
    }
}
