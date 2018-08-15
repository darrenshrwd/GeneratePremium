using System;
using ServiceStack;
using GeneratePremium.ServiceModel;
using GeneratePremium.ServiceModel.Extensions;

namespace GeneratePremium.ServiceInterface
{

    public class PremiumService : Service
    {
        public object Post(PremiumInput request)
        {
            if (string.IsNullOrEmpty(request.Name))
                throw new ArgumentException("We require your name");

            if (string.IsNullOrEmpty(request.Gender))
                throw new ArgumentException("We require your gender");

            var age = request.DateOfBirth.CalculateAge();

            if (age < 18 || age > 65)
                throw new ArgumentException("Can only receive a Premium if between the age of 18 and 65");

            var premium = PremiumCalculations.CalculatePremium(age, request.Gender);
            return new PremiumInputResponse { Result = $"Your premium is ${premium}" };
        }
    }
}
