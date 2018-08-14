using ServiceStack;

namespace GeneratePremium.ServiceModel
{
    using System;

    [Route("/generate-premium")]
    public class GenPremium : IReturn<GenPremiumResponse>
    {
        public string Name { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Gender { get; set; }
    }

    public class GenPremiumResponse
    {
        public string Result { get; set; }
    }
}
