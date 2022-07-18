using AutoMapper;
using DomainValidation.Application.ReadModels;
using DomainValidation.Core.Repositories;

namespace DomainValidation.Application.Queries
{
    public class GetCustomerByIdQueryHandler : IRequestHandler<GetCustomerByIdQuery, CustomerReadModel>
    {
        private readonly IMapper _mapper;
        private readonly ICustomersRepository _customersRepository;

        public GetCustomerByIdQueryHandler(IMapper mapper,
            ICustomersRepository customersRepository)
        {
            _mapper = mapper;
            _customersRepository = customersRepository;
        }

        public async Task<CustomerReadModel> Handle(GetCustomerByIdQuery request, CancellationToken cancellationToken)
        {
            var customer = await _customersRepository.GetByIdAsync(request.Id);
            return _mapper.Map<CustomerReadModel>(customer);
        }
    }
}
