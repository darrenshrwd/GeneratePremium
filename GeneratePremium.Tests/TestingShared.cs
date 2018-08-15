namespace GeneratePremium.Tests
{
    using System;
    using ServiceModel;

    public class TestingShared
    {
        public static GenPremium GetPremiumInput(int targetAgeToday, string name, string gender)
        {
            var genPremium = new GenPremium
            {
                Name = name,
                Gender = gender
            };
            genPremium.DateOfBirth = SameDateXYearsAgo(targetAgeToday);
            return genPremium;
        }

        public static DateTime SameDateXYearsAgo(int x)
        {
            return new DateTime(DateTime.Now.Year - x, DateTime.Now.Month, DateTime.Now.Day);
        }
    }
}