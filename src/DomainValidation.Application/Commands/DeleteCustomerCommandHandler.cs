using CSharpFunctionalExtensions;
using DomainValidation.Core;
using DomainValidation.Core.Repositories;

namespace DomainValidation.Application.Commands
{
    public class DeleteCustomerCommandHandler : IRequestHandler<DeleteCustomerCommand, UnitResult<Error>>
    {
        private readonly ICustomersRepository _customersRepository;

        public DeleteCustomerCommandHandler(ICustomersRepository customersRepository)
        {
            _customersRepository = customersRepository;
        }

        public async Task<UnitResult<Error>> Handle(DeleteCustomerCommand request, CancellationToken cancellationToken)
        {
            var customer = await _customersRepository.GetByIdAsync(request.Id);
            if(customer == null)
            {
                return Errors.General.NotFound();
            }
            await _customersRepository.DeleteAsync(request.Id);
            return UnitResult.Success<Error>();
        }
    }
}
