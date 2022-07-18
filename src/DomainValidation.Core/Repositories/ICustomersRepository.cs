using DomainValidation.Core.Entities;

namespace DomainValidation.Core.Repositories
{
    public interface ICustomersRepository
    {
        Task<Customer> GetByIdAsync(Guid id);
        IQueryable<Customer> GetAll();
        Task InsertAsync(Customer customer);
        Task DeleteAsync(Guid id);
    }
}
