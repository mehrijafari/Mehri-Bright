using AutoMapper;
using Mehri_Bright.Entities;
using Mehri_Bright.Models;

namespace Mehri_Bright.Profiles
{
    public class ProductProfile : Profile
    {
        public ProductProfile() //ctor
        {

            CreateMap<Product, ProductDto>()
                .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.CategoryName));

            CreateMap<ProductDto, Product>();
            CreateMap<ProductForCreationDto, Product>();
            CreateMap<ProductToUpdateDto, Product>();


        }

    }
}