using DomainValidation.Core.Entities;
using DomainValidation.Core.Repositories;

namespace DomainValidation.Infrastructure.Repositories
{
    public class CustomersRepository : ICustomersRepository
    {
        private readonly List<Customer> _customers = new List<Customer>();

        public Task<Customer> GetByIdAsync(Guid id)
        {
            return Task.FromResult(_customers.FirstOrDefault(c => c.Id == id));
        }

        public IQueryable<Customer> GetAll()
        {
            return _customers.AsQueryable();
        }

        public Task InsertAsync(Customer customer)
        {
            _customers.Add(customer);
            return Task.CompletedTask;
        }

        public Task DeleteAsync(Guid id)
        {
            _customers.RemoveAll(c => c.Id == id);
            return Task.CompletedTask;
        }
    }
}
