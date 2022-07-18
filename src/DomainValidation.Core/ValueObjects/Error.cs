using CSharpFunctionalExtensions;

namespace DomainValidation.Core
{
    public sealed class Error : ValueObject
    {
        private const string Separator = "||";

        public string Code { get; }
        public string Message { get; }

        internal Error(string code, string message)
        {
            Code = code;
            Message = message;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Code;
        }

        public string Serialize()
        {
            return $"{Code}{Separator}{Message}";
        }

        public static Error Deserialize(string serialized)
        {
            if (serialized == "A non-empty request body is required.")
                return Errors.General.ValueIsRequired();

            string[] data = serialized.Split(new[] { Separator }, StringSplitOptions.RemoveEmptyEntries);

            if (data.Length < 2)
                throw new Exception($"Invalid error serialization: '{serialized}'");

            return new Error(data[0], data[1]);
        }
    }

    public static class Errors
    {
        public static class Customer
        {
            public static Error EmailIsInvalid() =>
                new Error("customer.email.is.invalid", "Email is invalid");

            public static Error EmailAlreadyExists(string email = null)
            {
                var forEmail = email == null ? "" : $" for '{email}'";
                return new Error("customer.email.already.exists", $"Email already exists{forEmail}");
            }
                
        }

        public static class General
        {
            public static Error NotFound(Guid? id = null)
            {
                string forId = id == null ? "" : $" for Id '{id}'";
                return new Error("record.not.found", $"Record not found{forId}");
            }

            public static Error ValueIsInvalid(string name = "Value") =>
                new Error("value.is.invalid", $"{name} is invalid");

            public static Error ValueIsRequired(string name = "Value") =>
                new Error("value.is.required", $"{name} is required");

            public static Error ValueIsTooLong(string name = "Value") =>
                new Error("value.is.too.long", $"{name} is too long");


            public static Error InvalidLength(string name = null)
            {
                string label = name == null ? " " : " " + name + " ";
                return new Error("invalid.string.length", $"Invalid{label}length");
            }

            public static Error InternalServerError(string message)
            {
                return new Error("internal.server.error", message);
            }
        }
    }
}
