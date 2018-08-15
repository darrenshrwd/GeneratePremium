using System;
using Funq;
using ServiceStack;
using NUnit.Framework;
using GeneratePremium.ServiceInterface;
using GeneratePremium.ServiceModel;
using GeneratePremium.ServiceModel.Extensions;

namespace GeneratePremium.Tests
{
    public class IntegrationTest
    {
        const string BaseUri = "http://localhost:2000/";
        private readonly ServiceStackHost appHost;

        class AppHost : AppSelfHostBase
        {
            public AppHost() : base(nameof(IntegrationTest), typeof(MyServices).Assembly) { }

            public override void Configure(Container container)
            {
            }
        }

        public IntegrationTest()
        {
            appHost = new AppHost()
                .Init()
                .Start(BaseUri);
        }

        [OneTimeTearDown]
        public void OneTimeTearDown() => appHost.Dispose();

        public IServiceClient CreateClient() => new JsonServiceClient(BaseUri);

        [Test]
        public void Can_call_Hello_Service()
        {
            var client = CreateClient();

            var response = client.Get(new Hello { Name = "World" });

            Assert.That(response.Result, Is.EqualTo("Hello, World!"));
        }

        [Test]
        public void Can_call_Premium_Service()
        {
            var client = CreateClient();

            const int targetAgeToday = 18;
            var input = TestingShared.GetPremiumInput(targetAgeToday, "A Rose", "male");

            var age = input.DateOfBirth.CalculateAge();
            Assert.AreEqual(targetAgeToday, age);

            var premium = PremiumCalculations.CalculatePremium(age, input.Gender);

            var response = client.Post(input);
            Assert.That(response.Result, Is.EqualTo($"Your premium is ${premium}"));;
        }
    }
}