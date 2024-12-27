using Microsoft.AspNetCore.Mvc;
using Odev.Core.Entitys;
using Odev.Core.Interfaces;
using OpenQA.Selenium;
using ILogger = Odev.Core.Interfaces.ILogger;
using Odev.Core.TransferObjects;
using AutoMapper;


namespace VeriNesneOdev2.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductRepository _productRepository;
        private readonly ILogger _logger;
        private readonly IMapper _mapper;

        public ProductsController(IProductRepository productRepository, ILogger logger, IMapper mapper)
        {
            _productRepository = productRepository;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetProducts()
        {
            try
            {
                var products = await _productRepository.GetAllAsync();
                var productDtos = _mapper.Map<List<ProductDto>>(products);
                return Ok(productDtos);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error retrieving products", ex);
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDto>> GetProduct(int id)
        {
            try
            {
                var product = await _productRepository.GetByIdAsync(id);
                if (product == null)
                {
                    _logger.LogInformation($"Product with id {id} not found");
                    return NotFound();
                }
                var productDto = _mapper.Map<ProductDto>(product);
                return Ok(productDto);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error retrieving product with id {id}", ex);
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost]
        public async Task<ActionResult> CreateProduct(CreateProductDto productDto)
        {
            try
            {
                var product = _mapper.Map<Product>(productDto);
                await _productRepository.AddAsync(product);
                return CreatedAtAction(nameof(GetProduct), new { id = product.Id }, product);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error creating product", ex);
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateProduct(int id, UpdateProductDto productDto)
        {
            try
            {
                var product = _mapper.Map<Product>(productDto);
                product.Id = id;
                await _productRepository.UpdateAsync(product);
                return NoContent();
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error updating product with id {id}", ex);
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteProduct(int id)
        {
            try
            {
                await _productRepository.DeleteAsync(id);
                return NoContent();
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error deleting product with id {id}", ex);
                return StatusCode(500, "Internal server error");
            }
        }

        
        
           
        
    }
}
