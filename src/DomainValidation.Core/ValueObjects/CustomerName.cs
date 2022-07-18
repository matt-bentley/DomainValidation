using CSharpFunctionalExtensions;

namespace DomainValidation.Core.ValueObjects
{
    public class CustomerName : ValueObject
    {
        private CustomerName(string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;
        }

        public static Result<CustomerName, Error> Create(string firstName, string lastName)
        {
            firstName = firstName.TrimNullable();
            lastName = lastName.TrimNullable();

            if (string.IsNullOrEmpty(firstName))
            {
                return Errors.General.ValueIsRequired("First Name");
            }
            if (string.IsNullOrEmpty(lastName))
            {
                return Errors.General.ValueIsRequired("Last Name");
            }

            if (firstName.Length > 100)
                return Errors.General.ValueIsTooLong("First Name");

            if (lastName.Length > 100)
                return Errors.General.ValueIsTooLong("Last Name");

            return new CustomerName(firstName, lastName);
        }

        public string FirstName { get; }
        public string LastName { get; }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return FirstName;
            yield return LastName;
        }

        public override string ToString()
        {
            return $"{FirstName} {LastName}";
        }
    }
}
