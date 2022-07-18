using DomainValidation.Core.ValueObjects;

namespace DomainValidation.Core.Tests.ValueObjects
{
    [TestClass]
    public class EmailTests
    {
        [TestMethod]
        public void GivenEmail_WhenCreateValid_ThenCreate()
        {
            var email = Email.Create("tester@test.com");

            email.IsSuccess.Should().BeTrue();
            email.Value.Value.Should().Be("tester@test.com");
        }

        [TestMethod]
        public void GivenEmail_WhenCreateSameEmail_ThenEquals()
        {
            var email1 = Email.Create("tester@test.com");
            var email2 = Email.Create("tester@test.com");

            email1.Should().Be(email2);
        }

        [TestMethod]
        public void GivenEmail_WhenCreateWithExtraWhitespace_ThenCreateTrimmed()
        {
            var email = Email.Create(" tester@test.com ");

            email.IsSuccess.Should().BeTrue();
            email.Value.Value.Should().Be("tester@test.com");
        }

        [TestMethod]
        public void GivenEmail_WhenCreateNull_ThenFail()
        {
            var email = Email.Create(null);

            email.IsSuccess.Should().BeFalse();
            email.Error.Should().Be(Errors.General.ValueIsRequired());
        }

        [TestMethod]
        public void GivenEmail_WhenCreateInvalidEmail_ThenFail()
        {
            var email = Email.Create("test");

            email.IsSuccess.Should().BeFalse();
            email.Error.Should().Be(Errors.Customer.EmailIsInvalid());
        }
    }
}