using CSharpFunctionalExtensions;
using DomainValidation.Core.Entities;
using DomainValidation.Core.ValueObjects;

namespace DomainValidation.Core.Tests.Entities
{
    [TestClass]
    public class CustomerTests
    {
        [TestMethod]
        public void GivenCustomer_WhenCreateValid_ThenCreate()
        {
            var customer = CreateCustomer();
            customer.IsSuccess.Should().BeTrue();
            customer.Value.Name.FirstName.Should().Be("Joe");
            customer.Value.HasLeft.Should().BeFalse();
        }

        [TestMethod]
        public void GivenCustomer_WhenLeave_ThenHasLeft()
        {
            var customer = CreateCustomer().Value;
            customer.Leave();
            customer.HasLeft.Should().BeTrue();
        }

        [TestMethod]
        public void GivenCustomer_WhenUpdateNewAddress_ThenCreateAddress()
        {
            var customer = CreateCustomer().Value;
            customer.Address.Should().BeNull();
            customer.UpdateAddress("1 Feather Lane", "Manhattan", "New York", "12345");
            customer.Address.State.Should().Be("New York");
        }

        [TestMethod]
        public void GivenCustomer_WhenUpdateExistingAddress_ThenUpdateAddress()
        {
            var customer = CreateCustomer().Value;
            customer.UpdateAddress("1 Feather Lane", "Manhattan", "New York", "12345");
            customer.UpdateAddress("2 Feather Lane", "Manhattan", "New York", "12345");
            customer.Address.Street.Should().Be("2 Feather Lane");
        }

        private static Result<Customer, Error> CreateCustomer()
        {
            return Customer.Create(CustomerName.Create("Joe", "Bloggs").Value, Email.Create("joebloggs@test.com").Value);
        }
    }
}
