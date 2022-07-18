using CSharpFunctionalExtensions;
using DomainValidation.Core;

namespace DomainValidation.Application.Commands
{
    public record UpdateAddressCommand(Guid CustomerId, string Street, string City, string State, string ZipCode) : IRequest<UnitResult<Error>>;
}
