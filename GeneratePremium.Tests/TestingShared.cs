namespace GeneratePremium.Tests
{
    using System;
    using ServiceModel;

    public class TestingShared
    {
        public static PremiumInput GetPremiumInput(int targetAgeToday, string name, string gender)
        {
            var genPremium = new PremiumInput
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