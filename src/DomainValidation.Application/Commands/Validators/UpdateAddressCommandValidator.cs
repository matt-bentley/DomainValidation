using DomainValidation.Core.Entities;
using FluentValidation;

namespace DomainValidation.Application.Commands.Validators
{
    public class UpdateAddressCommandValidator : AbstractValidator<UpdateAddressCommand>
    {
        public UpdateAddressCommandValidator()
        {
            RuleFor(e => e)
                .MustBeEntity(e => Address.Create(e.Street, e.City, e.State, e.ZipCode));
        }
    }
}
