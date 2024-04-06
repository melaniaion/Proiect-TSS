using InventoryManagerBusiness.DTOs;
using InventoryManagerBusiness.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace InventoryManagerAPI.Controllers
{
    [ApiController]
    [Route("api/categories")]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categorytService;
        public CategoryController(ICategoryService categoryService)
        {
            _categorytService = categoryService;
        }

        [HttpGet]
        public IActionResult Get()
        {
            List<CategoryResponse> categories = _categorytService.Get();
            return Ok(categories);
        }

        [HttpGet]
        [Route("/api/category/{id}")]
        public IActionResult Get(int id)
        {
            CategoryResponse category = _categorytService.Get(id);
            if (category == null)
            {
                return NotFound();
            }
            return Ok(category);
        }

        [HttpPost]
        public IActionResult Post([FromBody] CategoryRequest category)
        {
            if (category == null)
            {
                return BadRequest("Category data is required!");
            }

            int newCategoryId = _categorytService.Create(category);

            string actionName = nameof(Get);
            return CreatedAtAction(actionName, new { id = newCategoryId }, category);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] CategoryRequest category)
        {
            if (category == null)
            {
                return BadRequest("Category data is required!");
            }

            _categorytService.Update(id, category);
            return Ok(category);
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            _categorytService.Delete(id);
            return NoContent();
        }
    }
}
