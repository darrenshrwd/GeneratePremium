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

        //TODO 2 Adjust test.
        [Test]
        public void Can_call_Premium_Service()
        {
            var client = CreateClient();
            var input = new GenPremium
            {
                Name = "Joe Bloggs",
                DateOfBirth = new DateTime(2000, 01, 20),
                Gender = "Male"
            };

            var age = input.DateOfBirth.CalculateAge();
            Assert.AreEqual(18,age);

            var response = (GenPremiumResponse)client.Post(input);

            Assert.That(response.Result, Is.EqualTo("TODO Generate a premium for Joe Bloggs"));
        }
    }
}