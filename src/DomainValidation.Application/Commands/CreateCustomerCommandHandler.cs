using CSharpFunctionalExtensions;
using DomainValidation.Application.ReadModels;
using DomainValidation.Core;
using DomainValidation.Core.Entities;
using DomainValidation.Core.Repositories;
using DomainValidation.Core.ValueObjects;

namespace DomainValidation.Application.Commands
{
    public class CreateCustomerCommandHandler : IRequestHandler<CreateCustomerCommand, Result<CreatedReadModel, Error>>
    {
        private readonly ICustomersRepository _customersRepository;

        public CreateCustomerCommandHandler(ICustomersRepository customersRepository)
        {
            _customersRepository = customersRepository;
        }

        public async Task<Result<CreatedReadModel, Error>> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
        {
            if (Exists(request.Email))
            {
                return Errors.Customer.EmailAlreadyExists(request.Email);
            }
            var customer = Customer.Create(CustomerName.Create(request.FirstName, request.LastName).Value, Email.Create(request.Email).Value);
            if (customer.IsFailure)
                return customer.Error;
            await _customersRepository.InsertAsync(customer.Value);
            return new CreatedReadModel(customer.Value.Id);
        }

        private bool Exists(string email)
        {
            return _customersRepository.GetAll().Any(e => e.Email.Value.Equals(email.TrimNullable()));
        }
    }
}
