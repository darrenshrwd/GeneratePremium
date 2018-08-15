namespace GeneratePremium.ServiceModel
{
    using System;

    public class PremiumCalculations
    {
        public static decimal CalculatePremium(int age, string gender)
        {
            if (age < 18 || age > 65)
                throw new ArgumentException("Can only receive a Premium if between the age of 18 and 65");
            
            const decimal maleGenderFactor = 1.2M;
            const decimal femaleGenderFactor = 1.1M;
            decimal genderFactor;
            switch (gender.ToLower())
            {
                case "male":
                    genderFactor = maleGenderFactor;
                    break;
                case "female":
                    genderFactor = femaleGenderFactor;
                    break;
                default:
                    throw new Exception("We have not accounted for your unusual gender");
            }

            var premium = age * genderFactor * 100;
            return premium;
        }
    }
}