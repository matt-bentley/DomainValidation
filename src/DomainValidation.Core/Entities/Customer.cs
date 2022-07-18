using CSharpFunctionalExtensions;
using DomainValidation.Core.Entities.Abstract;
using DomainValidation.Core.ValueObjects;

namespace DomainValidation.Core.Entities
{
    public class Customer : AggregateRoot
    {
        private Customer(CustomerName customerName, Email email, bool hasLeft)
        {
            Name = customerName;
            Email = email;
            HasLeft = hasLeft;
        }

        public static Result<Customer, Error> Create(CustomerName customerName, Email email)
        {
            return new Customer(customerName, email, false);
        }

        public CustomerName Name { get; private set; }
        public Email Email { get; private set; }
        public Address Address { get; private set; }
        public bool HasLeft { get; private set; }

        public UnitResult<Error> UpdateAddress(string street, string city, string state, string zipCode)
        {
            if(Address == null)
            {
                var newAddress = Address.Create(street, city, state, zipCode);
                if (newAddress.IsFailure)
                    return UnitResult.Failure<Error>(newAddress.Error);
                Address = newAddress.Value;
            }
            else
            {
                var updatedAddress = Address.Update(street, city, state, zipCode);
                if (updatedAddress.IsFailure)
                    return updatedAddress;
            }
            return UnitResult.Success<Error>();
        }

        public void Leave()
        {
            HasLeft = true;
        }
    }
}
