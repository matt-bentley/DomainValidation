using AutoMapper;
using DomainValidation.Application.ReadModels;
using DomainValidation.Core.Entities;

namespace DomainValidation.Application.MappingProfiles
{
    public class CustomerMappingProfile : Profile
    {
        public CustomerMappingProfile()
        {
            CreateMap<Customer, CustomerReadModel>()
                .ForMember(e => e.FirstName, e => e.MapFrom(src => src.Name.FirstName))
                .ForMember(e => e.LastName, e => e.MapFrom(src => src.Name.LastName))
                .ForMember(e => e.Email, e => e.MapFrom(src => src.Email.Value));
        }
    }
}
