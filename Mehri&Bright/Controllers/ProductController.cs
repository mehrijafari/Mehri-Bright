using AutoMapper;
using Mehri_Bright.Entities;
using Mehri_Bright.Models;
using Mehri_Bright.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;


namespace Mehri_Bright.Controllers
{
    [ApiController]
    [Route("api/products")]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        private readonly ICategoryRepository _categoryRepository;

        public ProductController(IProductRepository productRepository, IMapper mapper, ICategoryRepository categoryRepository)
        {
            _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _categoryRepository = categoryRepository ?? throw new ArgumentNullException(nameof(categoryRepository));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetAllProducts()
        {
            var productEntities = await _productRepository.GetAllProductsAsync();
            return Ok(_mapper.Map<IEnumerable<ProductDto>>(productEntities));
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetProduct(int id)
        {
            var product = await _productRepository.GetProductAsync(id);
            if (product == null)
            {
                return NotFound($"Product with id {id} was not found");
            }

            return Ok(_mapper.Map<ProductDto>(product));
        }

        [HttpPost(Name = "CreateProduct")]
        public async Task<ActionResult<ProductDto>> CreateProduct(ProductForCreationDto product)
        {


            var productEntity = _mapper.Map<Product>(product);

            var categoryExists = _categoryRepository.CategoryExists(product.CategoryId);
            if (!categoryExists)
            {
                return NotFound($"Category with id {product.CategoryId} was not found.");
            }

            await _productRepository.AddProduct(productEntity);

            await _productRepository.SaveChangesAsync();

            var createdProductWithCategory = await _productRepository.GetProductAsync(productEntity.Id);


            var creadtedProductToReturn = _mapper.Map<ProductDto>(createdProductWithCategory);

            return CreatedAtRoute("CreateProduct",
                new
                {
                    id = productEntity.Id
                },
                creadtedProductToReturn);

        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteProduct(int id)
        {
            var productEntity = await _productRepository.GetProductAsync(id);
            if (productEntity == null)
            {
                return NotFound($"Product with id {id} was not found.");
            }

            await _productRepository.DeleteProduct(productEntity);
            await _productRepository.SaveChangesAsync();

            return Ok($"Product with id {id} was successfully deleted.");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(int id, ProductToUpdateDto product) 
        {
            var productToUpdate = await _productRepository.GetProductAsync(id);
            if (productToUpdate == null)
            {
                return NotFound($"Product with id {id} was not found");
            }

            var productEntity = _mapper.Map(product, productToUpdate);

            var categoryExists = _categoryRepository.CategoryExists(product.CategoryId);
            if (!categoryExists)
            {
                return NotFound($"Category with id {product.CategoryId} was not found.");
            }

            await _productRepository.UpdateProductAsync(productEntity);
            await _productRepository.SaveChangesAsync();

            return Ok($"Product with id {id} was updated successfully");
        }
       
    }
}
