using DomainValidation.Core.Entities;
using DomainValidation.Core.Tests.Helpers;

namespace DomainValidation.Core.Tests.Entities
{
    [TestClass]
    public class AddressTests
    {
        [TestMethod]
        public void GivenAddress_WhenCreateValid_ThenCreate()
        {
            var address = Address.Create("1 Feather Lane", "Manhattan", "New York", "12345");

            address.IsSuccess.Should().BeTrue();
            address.Value.State.Should().Be("New York");
        }

        [TestMethod]
        public void GivenAddress_WhenUpdateValid_ThenUpdate()
        {
            var address = Address.Create("1 Feather Lane", "Manhattan", "New York", "12345");
            address.Value.Update("2 Feather Lane", "Manhattan", "New York", "12345");

            address.IsSuccess.Should().BeTrue();
            address.Value.Street.Should().Be("2 Feather Lane");
        }

        [DataTestMethod]
        [DataRow("", "Manhattan", "New York", "12345", "street")]
        [DataRow("1 Feather Lane", "", "New York", "12345", "city")]
        [DataRow("1 Feather Lane", "Manhattan", "", "12345", "state")]
        [DataRow("1 Feather Lane", "Manhattan", "New York", "", "zipCode")]
        public void GivenAddress_WhenCreateMissingField_ThenFail(string street, string city, string state, string zipCode, string errorField)
        {
            var address = Address.Create(street, city, state, zipCode);

            address.IsFailure.Should().BeTrue();
            address.Error.Should().Be(Errors.General.InvalidLength());
        }

        [DataTestMethod]
        [DataRow(101, 10, 10, 10, "street")]
        [DataRow(10, 41, 10, 10, "city")]
        [DataRow(10, 10, 41, 10, "state")]
        [DataRow(10, 10, 10, 6, "zipCode")]
        public void GivenAddress_WhenCreateTooLongField_ThenFail(int streetLength, int cityLength, int stateLength, int zipCodeLength, string errorField)
        {
            var address = Address.Create(StringGenerator.WithLength(streetLength), StringGenerator.WithLength(cityLength), StringGenerator.WithLength(stateLength), StringGenerator.WithLength(zipCodeLength));

            address.IsFailure.Should().BeTrue();
            address.Error.Should().Be(Errors.General.InvalidLength());
        }
    }
}
