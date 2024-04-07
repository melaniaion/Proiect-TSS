using InventoryManagerBusiness.DTOs;
using InventoryManagerBusiness.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace InventoryManagerAPI.Controllers
{
    [ApiController]
    [Route("api/products")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public IActionResult Get()
        {
            List<ProductResponse> products = _productService.Get();
            return Ok(products);
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            ProductResponse product = _productService.Get(id);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }

        [HttpPost]
        public IActionResult Post([FromBody] ProductDto product)
        {
            if (product == null)
            {
                return BadRequest("Product data is required!");
            }

            int newProductId = _productService.Create(product);

            string actionName = nameof(Get);
            return CreatedAtAction(actionName, new { id = newProductId }, product);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] ProductRequest product)
        {
            if (product == null)
            {
                return BadRequest("Product data is required!");
            }

            _productService.Update(id, product);
            return Ok(product);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _productService.Delete(id);
            return NoContent();
        }
    }
}
