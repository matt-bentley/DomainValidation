using AutoMapper;
using DomainValidation.Application.ReadModels;
using DomainValidation.Core.Repositories;

namespace DomainValidation.Application.Queries
{
    public class GetCustomersQueryHandler : IRequestHandler<GetCustomersQuery, List<CustomerReadModel>>
    {
        private readonly IMapper _mapper;
        private readonly ICustomersRepository _customersRepository;

        public GetCustomersQueryHandler(IMapper mapper,
            ICustomersRepository customersRepository)
        {
            _mapper = mapper;
            _customersRepository = customersRepository;
        }

        public Task<List<CustomerReadModel>> Handle(GetCustomersQuery request, CancellationToken cancellationToken)
        {
            var customers = _customersRepository.GetAll();
            var mapped = _mapper.ProjectTo<CustomerReadModel>(customers).ToList();
            return Task.FromResult(mapped);
        }
    }
}
