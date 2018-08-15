using System;
using NUnit.Framework;
using ServiceStack;
using ServiceStack.Testing;
using GeneratePremium.ServiceInterface;
using GeneratePremium.ServiceModel;

namespace GeneratePremium.Tests
{
    using NUnit.Framework.Constraints;
    using ServiceModel.Extensions;

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

        [Test]
        public void CalculateAgeWorksFor50YrsAgoToday()
        {
            //Arrange
            const int targetAgeToday = 50;
            var sameDateXYearsAgo = TestingShared.SameDateXYearsAgo(targetAgeToday);

            //Act
            var age = sameDateXYearsAgo.CalculateAge();

            //Assert
            Assert.AreEqual(targetAgeToday, age);
        }

        [Test]
        public void CalculatePremiumReturnsExpectedResultFor40YrOldFemale()
        {
            //Arrange
            const int targetAgeToday = 40;
            const string gender = "female";

            var input = TestingShared.GetPremiumInput(targetAgeToday, "A Rose", gender);
            var age = input.DateOfBirth.CalculateAge();

            //Act
            var premium = PremiumCalculations.CalculatePremium(age, input.Gender);

            //Assert
            Assert.AreEqual(4400, premium);
        }

        [Test]
        public void CalculatePremiumReturnsExpectedResultFor30YrOldMale()
        {
            const int targetAgeToday = 30;
            const string gender = "male";

            var input = TestingShared.GetPremiumInput(targetAgeToday, "A Rose", gender);
            var age = input.DateOfBirth.CalculateAge();
            var premium = PremiumCalculations.CalculatePremium(age, input.Gender);

            Assert.AreEqual(3600, premium);
        }

        [Test]
        public void CalculatePremiumThrowsIfUnder18()
        {
            void CheckFunction()
            {
                //Arrange
                const int targetAgeToday = 17;
                var input = TestingShared.GetPremiumInput(targetAgeToday, "A Rose", "male");
                var age = input.DateOfBirth.CalculateAge();

                //Act
                PremiumCalculations.CalculatePremium(age, input.Gender);
            }

            //Assert
            Assert.Throws(typeof(ArgumentException), CheckFunction);
        }

        [Test]
        public void CalculatePremiumThrowsIfOver65()
        {
            void CheckFunction()
            {
                //Arrange
                const int targetAgeToday = 66;
                var input = TestingShared.GetPremiumInput(targetAgeToday, "A Rose", "male");
                var age = input.DateOfBirth.CalculateAge();

                //Act
                PremiumCalculations.CalculatePremium(age, input.Gender);
            }

            //Assert
            Assert.Throws(typeof(ArgumentException), CheckFunction);
        }

        /* TAL Digital Technical Test

            Create a Simple Web Application (Technology of your own choice) that demonstrates full-stack development.
            The application will capture some input from the user, and generate a Premium.

            Input fields:
            Name
            Date of Birth
            Gender

            In the project READ.ME, list out the areas of improvement and refinement if you had a full 2 days to build this application.

            Share the details of your repository for access by the TAL Digital team, and a URL if you have a running application.
        */
        [Test]
        public void Can_call_GeneratePremiumService()
        {
            //Arrange
            var service = appHost.Container.Resolve<PremiumService>();
            const int targetAgeToday = 18;
            var input = TestingShared.GetPremiumInput(targetAgeToday, "A Rose", "male");
            var age = input.DateOfBirth.CalculateAge();
            var premium = PremiumCalculations.CalculatePremium(age, input.Gender);

            //Act
            var response = (GenPremiumResponse)service.Post(input);

            //Assert
            Assert.That(response.Result, Is.EqualTo($"Your premium is ${premium}"));
        }
    }
}
