using System;
using System.Collections.Generic;
using System.Linq;
using ServiceStack;
using ServiceStack.Templates;
using ServiceStack.DataAnnotations;
using GeneratePremium.ServiceModel;
using GeneratePremium.ServiceModel.Extensions;

namespace GeneratePremium.ServiceInterface
{

    public class PremiumService : Service
    {
        public object Post(GenPremium request)
        {
            /* TODO 3
            Input fields:
            Name
            Date of Birth
            Gender

            Based on the input, use the following to calculate a result:
            Premium = Age * GenderFactor * 100
            GenderFactor is based on the Gender the user supplied. 1.2 for a Male and 1.1 for a Female
            The person can only receive a Premium if they are between the age of 18 and 65

             */

            var age1 = request.DateOfBirth.CalculateAge();

            return new GenPremiumResponse { Result = $"TODO Generate a premium for {request.Name}" };
        }


    }
}
