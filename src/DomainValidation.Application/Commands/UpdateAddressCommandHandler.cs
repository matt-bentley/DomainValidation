using CSharpFunctionalExtensions;
using DomainValidation.Core;
using DomainValidation.Core.Repositories;

namespace DomainValidation.Application.Commands
{
    public class UpdateAddressCommandHandler : IRequestHandler<UpdateAddressCommand, UnitResult<Error>>
    {
        private readonly ICustomersRepository _customersRepository;

        public UpdateAddressCommandHandler(ICustomersRepository customersRepository)
        {
            _customersRepository = customersRepository;
        }

        public async Task<UnitResult<Error>> Handle(UpdateAddressCommand request, CancellationToken cancellationToken)
        {
            var customer = await _customersRepository.GetByIdAsync(request.CustomerId);
            if(customer == null)
            {
                return Errors.General.NotFound(request.CustomerId);
            }
            var result = customer.UpdateAddress(request.Street, request.City, request.State, request.ZipCode);
            if (result.IsSuccess)
            {
                // this is where the database transaction should be saved...
            }
            return result;
        }
    }
}
