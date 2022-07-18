using DomainValidation.Core.ValueObjects;
using FluentValidation;

namespace DomainValidation.Application.Commands.Validators
{
    public class CreateCustomerCommandValidator : AbstractValidator<CreateCustomerCommand>
    {
        public CreateCustomerCommandValidator()
        {
            RuleFor(e => e)
                .MustBeValueObject(v => CustomerName.Create(v.FirstName, v.LastName));

            RuleFor(e => e)
                .MustBeValueObject(v => Email.Create(v.Email));
        }
    }
}
