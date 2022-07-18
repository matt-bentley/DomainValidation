using DomainValidation.Application.ReadModels;

namespace DomainValidation.Application.Queries
{
    public record GetCustomersQuery() : IRequest<List<CustomerReadModel>>;
}
