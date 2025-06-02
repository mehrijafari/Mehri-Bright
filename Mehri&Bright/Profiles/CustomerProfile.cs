using AutoMapper;
using Mehri_Bright.Entities;
using Mehri_Bright.Models;

namespace Mehri_Bright.Profiles
{
    public class CustomerProfile : Profile
    {
        public CustomerProfile() //ctor
        {
            CreateMap<Customer, CustomerDto>();
            CreateMap<Customer, CustomerForCreationDto>();
            CreateMap<CustomerForCreationDto, Customer>();
            CreateMap<CustomerToUpdateDto, Customer>();
        }
    }
}
