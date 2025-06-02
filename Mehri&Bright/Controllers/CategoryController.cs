using AutoMapper;
using Mehri_Bright.Models;
using Mehri_Bright.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace Mehri_Bright.Controllers
{
    [ApiController]
    [Route("api/categories")]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public CategoryController(ICategoryRepository categoryRepository, IMapper mapper)
        {
            this._categoryRepository = categoryRepository ?? throw new ArgumentNullException(nameof(categoryRepository));
            this._mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet]
        public async Task<ActionResult<CategoryDto>> GetAllCategories()
        {
            var categoryEntities = await _categoryRepository.GetAllCategoriesAsync();
            return Ok(_mapper.Map<IEnumerable<CategoryDto>>(categoryEntities));
        }
    }
}
