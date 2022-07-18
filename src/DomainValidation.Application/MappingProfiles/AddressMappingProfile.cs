using AutoMapper;
using DomainValidation.Application.ReadModels;
using DomainValidation.Core.Entities;

namespace DomainValidation.Application.MappingProfiles
{
    public class AddressMappingProfile : Profile
    {
        public AddressMappingProfile()
        {
            CreateMap<Address, AddressReadModel>();
        }
    }
}
