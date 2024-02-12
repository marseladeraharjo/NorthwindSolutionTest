using Mapster;
using Microsoft.AspNetCore.Mvc;
using Northwind.Contract.Dtos;
using Northwind.Domain.Entities.Master;
using Northwind.Domain.Repositories;
using Northwind.Service.Abstraction.Base;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Northwind.WebAPI.Controllers
{
    [Route("api/categorysrv")]
    [ApiController]
    public class CategoryServiceController : ControllerBase
    {
        private readonly IServiceManager _serviceManager;

        public CategoryServiceController(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
        }

        // GET: api/<CategoryController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoryDto>>> GetCategories()
        {
            var categoryDtos =await _serviceManager.CategoryService.GetAllAsync(false);
            return Ok(categoryDtos);
        }

        // GET api/<CategoryController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CategoryDto>> GetCategoryById(int id)
        {
            var categoryDto = await _serviceManager.CategoryService.GetByIdAsync(id, false);
            return Ok(categoryDto);
        }

        // POST api/<CategoryController>
        [HttpPost]
        public async Task<IActionResult> CreateCategory([FromBody] CategoryDto categoryDto)
        {
            var category = await _serviceManager.CategoryService.CreateAsync(categoryDto);
            return CreatedAtAction(nameof(GetCategoryById), new {id =  category.Id}, category);
        }

        // PUT api/<CategoryController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCategory(int id, [FromBody] CategoryDto categoryDto)
        {
            await _serviceManager.CategoryService.UpdateAsync(id, categoryDto);
            return NoContent();
        }

        // DELETE api/<CategoryController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            await _serviceManager.CategoryService.DeleteAsync(id);
            return NoContent();
        }
    }
}
