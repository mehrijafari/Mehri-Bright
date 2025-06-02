using AutoMapper;
using Mehri_Bright.Entities;
using Mehri_Bright.Models;

namespace Mehri_Bright.Profiles
{
    public class CategoryProfile : Profile
    {
        public CategoryProfile()
        {
            CreateMap<Category, CategoryDto>();
        }
    }
}
