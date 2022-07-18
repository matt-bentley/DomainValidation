using DomainValidation.Application.ReadModels;

namespace DomainValidation.Application.Queries
{
    public record GetCustomerByIdQuery(Guid Id) : IRequest<CustomerReadModel>;
}
