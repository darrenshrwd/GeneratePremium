using ServiceStack;

namespace GeneratePremium.ServiceModel
{
    using System;

    [Route("/generate-premium")]
    public class PremiumInput : IReturn<PremiumInputResponse>
    {
        public string Name { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Gender { get; set; }
    }

    public class PremiumInputResponse
    {
        public string Result { get; set; }
    }
}
