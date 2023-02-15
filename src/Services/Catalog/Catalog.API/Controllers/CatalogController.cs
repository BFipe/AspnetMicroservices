using AutoMapper;
using Catalog.API.Data;
using Catalog.API.Entities;
using Catalog.API.Repositories;
using Catalog.API.Repositories.DataTransferObjects.Product;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Catalog.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class CatalogController : ControllerBase
    {
        private readonly IProductRepository _productRepository;
        private readonly ILogger<CatalogController> _logger;
        private readonly IMapper _mapper;

        public CatalogController(IProductRepository productRepository, ILogger<CatalogController> logger, IMapper mapper)
        {
            _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet("All")]
        public async Task<ActionResult<IEnumerable<GetProductDto>>> GetAllProducts()
        {
            var products = await _productRepository.GetAllAsync();
            if (products.Any() == false)
            {
                _logger.LogCritical($"Database does not contains any data");
                return NotFound();
            }
            var responce = _mapper.Map<IEnumerable<GetProductDto>>(products);
            return Ok(responce);
        }

        [HttpGet("{id:length(24)}")]
        public async Task<ActionResult<GetProductDto>> GetProductById(string id)
        {
            var product = await _productRepository.GetByIdAsync(id);
            if (product is null)
            {
                _logger.LogInformation($"Product with id {id} not found");
                return NotFound();
            }
            var responce = _mapper.Map<GetProductDto>(product);
            return Ok(responce);
        }

        [HttpGet("Names/{name}")]
        public async Task<ActionResult<IEnumerable<GetProductDto>>> GetProductsByName(string name)
        {
            var products = await _productRepository.GetByNameAsync(name);
            if (products.Any() == false)
            {
                _logger.LogInformation($"No products are containing name '{name}'");
                return NotFound();
            }
            var responce = _mapper.Map<IEnumerable<GetProductDto>>(products);
            return Ok(responce);
        }

        [HttpPost]
        public async Task<ActionResult<GetProductDto>> AddProduct([FromBody]PostProductDto postProduct)
        {
            var product = _mapper.Map<Product>(postProduct);
            var responce = await _productRepository.CreateAsync(product);
            return Ok(responce);
        }

        [HttpPut]
        public async Task<ActionResult<GetProductDto>> UpdateProduct([FromBody]UpdateProductDto updateProduct)
        {
            var product = _mapper.Map<Product>(updateProduct);
            var responce = await _productRepository.UpdateAsync(product);
            return Ok(responce);
        }

        [HttpDelete("{id:length(24)}")]
        public async Task<ActionResult<GetProductDto>> DeleteProduct(string id)
        {
            var responce = await _productRepository.DeleteAsync(id);
            return Ok(responce);
        }
    }
}
