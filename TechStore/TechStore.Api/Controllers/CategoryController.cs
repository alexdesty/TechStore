using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using TechStore.Api.DTO;
using TechStore.Api.DTOValidators;
using TechStore.Domain.Entities;
using TechStore.Domain.Interfaces.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TechStore.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController(ICategoryService categoryService, IValidator<CategoryDTO> categoryValidator, IMapper mapper) : ControllerBase
    {
        // GET: api/<CategoryController>
        [HttpGet]
        public async Task<IEnumerable<Category>> Get()
        {
            return await categoryService.GetAllAsync();
        }

        // GET api/<CategoryController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult> Get(int id) 
        {
            var category = await categoryService.GetAsync(id);
            return category == null ? NotFound() : Ok(category);
        }

        // POST api/<CategoryController>
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] CategoryDTO category)
        {
            var validationResult = categoryValidator.Validate(category);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors[0].ToString());
            }
            var newCategory = mapper.Map<Category>(category);
            var created = await categoryService.CreateAsync(newCategory);
            return Ok(created);
        }

        // PUT api/<CategoryController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] CategoryDTO category) 
        {
            var oldCategory = await categoryService.GetAsync(id);
            if (oldCategory != null)
            {
                Category edited = mapper.Map<Category>(category);
                edited.Id = id;
                var updated = await categoryService.UpdateAsync(edited);
                return Ok(updated);
            }
            return BadRequest();
        }

        // DELETE api/<CategoryController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
        var deleted = await categoryService.DeleteAsync(id);    
            return Ok(deleted);
        }
    }
}
