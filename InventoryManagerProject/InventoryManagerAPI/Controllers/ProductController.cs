using InventoryManagerBusiness.DTOs;
using InventoryManagerBusiness.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

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

        [HttpGet("category/{categoryId}")]
        public IActionResult GetByCategory(int categoryId,int index)
        {
            List<ProductResponse> products = _productService.GetByCategory(categoryId,index);
            if (products == null)
            {
                return NotFound();
            }
            return Ok(products);
        }

        [HttpPost]
        public IActionResult Post([FromBody] ProductRequest product)
        {
            var validationContext = new ValidationContext(product, serviceProvider: null, items: null);
            var validationResults = new List<ValidationResult>();
            bool isValid = Validator.TryValidateObject(product, validationContext, validationResults, validateAllProperties: true);

            if (!isValid)
            {
                var errors = validationResults.Select(vr => vr.ErrorMessage);
                return BadRequest(errors);
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
