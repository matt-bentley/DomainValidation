using CSharpFunctionalExtensions;
using DomainValidation.Application.ReadModels;
using DomainValidation.Core;

namespace DomainValidation.Application.Commands
{
    public record CreateCustomerCommand(string FirstName, string LastName, string Email) : IRequest<Result<CreatedReadModel, Error>>;
}
