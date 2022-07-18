using CSharpFunctionalExtensions;
using DomainValidation.Core.Entities.Abstract;

namespace DomainValidation.Core.Entities
{
    public class Address : EntityBase
    {
        private Address(string street, string city, string state, string zipCode)
        {
            Street = street;
            City = city;
            State = state;
            ZipCode = zipCode;
        }

        public static Result<Address, Error> Create(string street, string city, string state, string zipCode)
        {
            street = street.TrimNullable();
            state = state.TrimNullable();
            city = city.TrimNullable();
            zipCode = zipCode.TrimNullable();

            var validationResult = Validate(street, city, state, zipCode);
            if (validationResult.IsFailure)         
                return Result.Failure<Address, Error>(validationResult.Error);          

            return new Address(street, city, state, zipCode);
        }

        public UnitResult<Error> Update(string street, string city, string state, string zipCode)
        {
            street = street.TrimNullable();
            state = state.TrimNullable();
            city = city.TrimNullable();
            zipCode = zipCode.TrimNullable();

            var validationResult = Validate(street, city, state, zipCode);
            if (validationResult.IsFailure)
                return validationResult;

            Street = street;
            City = city;
            State= state;
            ZipCode= zipCode;
            return validationResult;
        }

        private static UnitResult<Error> Validate(string street, string city, string state, string zipCode)
        {
            if (street.Length < 1 || street.Length > 100)
                return Errors.General.InvalidLength("Street");

            if (city.Length < 1 || city.Length > 40)
                return Errors.General.InvalidLength("City");

            if (state.Length < 1 || state.Length > 40)
                return Errors.General.InvalidLength("State");

            if (zipCode.Length < 1 || zipCode.Length > 5)
                return Errors.General.InvalidLength("Zip Code");
            return UnitResult.Success<Error>();
        }

        public string Street { get; private set; }
        public string City { get; private set; }
        public string State { get; private set; }
        public string ZipCode { get; private set; }
    }
}
