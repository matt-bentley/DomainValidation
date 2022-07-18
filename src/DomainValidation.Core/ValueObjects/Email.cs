using CSharpFunctionalExtensions;
using System.Net.Mail;

namespace DomainValidation.Core.ValueObjects
{
    public class Email : ValueObject
    {
        private Email(string email)
        {
            Value = email;
        }

        public static Result<Email, Error> Create(string email)
        {
            email = email.TrimNullable();

            if (string.IsNullOrEmpty(email))
            {
                return Errors.General.ValueIsRequired("Email");
            }

            if (!ValidEmail(email))
            {
                return Errors.Customer.EmailIsInvalid();
            }
            return new Email(email);
        }

        private static bool ValidEmail(string email)
        {
            try
            {
                _ = new MailAddress(email);
                return true;
            }
            catch (FormatException)
            {
                return false;
            }
        }

        public string Value { get; }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
