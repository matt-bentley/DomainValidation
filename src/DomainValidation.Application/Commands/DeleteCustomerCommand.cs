using CSharpFunctionalExtensions;
using DomainValidation.Core;

namespace DomainValidation.Application.Commands
{
    public record DeleteCustomerCommand(Guid Id) : IRequest<UnitResult<Error>>;
}
