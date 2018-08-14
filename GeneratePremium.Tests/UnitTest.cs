using NUnit.Framework;
using ServiceStack;
using ServiceStack.Testing;
using GeneratePremium.ServiceInterface;
using GeneratePremium.ServiceModel;

namespace GeneratePremium.Tests
{
    public class UnitTest
    {
        private readonly ServiceStackHost appHost;

        public UnitTest()
        {
            appHost = new BasicAppHost().Init();
            appHost.Container.AddTransient<MyServices>();
            appHost.Container.AddTransient<PremiumService>();
        }

        [OneTimeTearDown]
        public void OneTimeTearDown() => appHost.Dispose();

        [Test]
        public void Can_call_MyServices()
        {
            var service = appHost.Container.Resolve<MyServices>();

            var response = (HelloResponse)service.Any(new Hello { Name = "World" });

            Assert.That(response.Result, Is.EqualTo("Hello, World!"));
        }

        //TODO 1 Adjust test.
        /* TAL Digital Technical Test

            Create a Simple Web Application (Technology of your own choice) that demonstrates full-stack development.
            The application will capture some input from the user, and generate a Premium.

            Input fields:
            Name
            Date of Birth
            Gender

            Based on the input, use the following to calculate a result:
            Premium = Age * GenderFactor * 100
            GenderFactor is based on the Gender the user supplied. 1.2 for a Male and 1.1 for a Female
            The person can only receive a Premium if they are between the age of 18 and 65

            In the project READ.ME, list out the areas of improvement and refinement if you had a full 2 days to build this application.

            Share the details of your repository for access by the TAL Digital team, and a URL if you have a running application.
        */
        [Test]
        public void Can_call_GeneratePremiumService()
        {
            var service = appHost.Container.Resolve<PremiumService>();

            var response = (GenPremiumResponse)service.Post(new GenPremium { Name = "Joe Bloggs" });

            Assert.That(response.Result, Is.EqualTo("TODO Generate a premium for Joe Bloggs"));
        }
    }
}
