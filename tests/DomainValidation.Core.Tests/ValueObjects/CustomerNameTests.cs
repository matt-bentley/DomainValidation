using DomainValidation.Core.Tests.Helpers;
using DomainValidation.Core.ValueObjects;

namespace DomainValidation.Core.Tests.ValueObjects
{
    [TestClass]
    public class CustomerNameTests
    {
        [TestMethod]
        public void GivenCustomerName_WhenCreateValid_ThenCreate()
        {
            var name = CustomerName.Create("Joe", "Bloggs");
            name.Value.FirstName.Should().Be("Joe");
        }

        [TestMethod]
        public void GivenCustomerName_WhenToString_ThenCombine()
        {
            var name = CustomerName.Create("Joe", "Bloggs");
            name.Value.ToString().Should().Be("Joe Bloggs");
        }

        [TestMethod]
        public void GivenCustomerName_WhenFirstNameTooLong_ThenFail()
        {
            var name = CustomerName.Create(StringGenerator.WithLength(101), "Bloggs");
            name.IsFailure.Should().BeTrue();
            name.Error.Should().Be(Errors.General.ValueIsTooLong());
        }

        [TestMethod]
        public void GivenCustomerName_WhenLastNameTooLong_ThenFail()
        {
            var name = CustomerName.Create("Joe", StringGenerator.WithLength(101));
            name.IsFailure.Should().BeTrue();
            name.Error.Should().Be(Errors.General.ValueIsTooLong());
        }

        [TestMethod]
        public void GivenCustomerName_WhenCreateSame_ThenEquals()
        {
            var name1 = CustomerName.Create("Joe", "Bloggs");
            var name2 = CustomerName.Create("Joe", "Bloggs");

            name1.Should().Be(name2);
        }

        [TestMethod]
        public void GivenCustomerName_WhenCreateWhitespace_ThenFail()
        {
            var name = CustomerName.Create("Joe", " ");
            name.IsFailure.Should().BeTrue();
            name.Error.Should().Be(Errors.General.ValueIsRequired("Last Name"));
        }
    }
}
