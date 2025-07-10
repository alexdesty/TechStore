using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using TechStore.Api.DTO;
using TechStore.Api.DTOValidators;
using TechStore.Domain.Entities;
using TechStore.Domain.Interfaces.Services;
using TechStore.Domain.Pagination;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TechStore.Api.Controllers;


[Route("api/[controller]")]
[ApiController]
public class CategoriesController(ICategoryService categoryService, IValidator<CategoryDTO> categoryValidator, IMapper mapper) : ControllerBase
{
    // GET: api/<CategoriesController>
    [Authorize]
    [HttpGet]
    public async Task<ActionResult<PaginatedList<CategoryDTO>>> Get(int pageIndex = 1, int pageSize = 1)
    {
        var categories = await categoryService.GetAllAsync(pageIndex, pageSize);
        var categoryDTO = mapper.Map<List<CategoryDTO>>(categories);
        var pagedDTO = new PaginatedList<CategoryDTO>(categoryDTO, pageIndex, pageSize);
        return Ok(pagedDTO);
    }

    // GET api/<CategoriesController>/5
    [HttpGet("{id}")]
    public async Task<ActionResult> Get(int id)
    {
        var category = await categoryService.GetAsync(id);
        if (category == null)
        {
            return NotFound();
        }
        var categoryDTO = mapper.Map<CategoryDTO>(category);
        return Ok(categoryDTO);
    }

    // POST api/<CategoriesController>
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
        var categoryDTO = mapper.Map<CategoryDTO>(created);
        return CreatedAtAction(nameof(Get), categoryDTO);
    }

    // PUT api/<CategoriesController>/5
    [HttpPut("{id}")]
    public async Task<ActionResult> Put(int id, [FromBody] CategoryDTO category)
    {

        var validationResult = categoryValidator.Validate(category);
        if (!validationResult.IsValid)
        {
            return BadRequest(validationResult.Errors[0].ToString());
        }
        var oldCategory = await categoryService.GetAsync(id);
        if (oldCategory != null)
        {
            mapper.Map<CategoryDTO, Category>(category, oldCategory);
            oldCategory.Id = id;
            var updated = await categoryService.UpdateAsync(oldCategory);
            var categoryDTO = mapper.Map<CategoryDTO>(updated);
            return Ok(categoryDTO);
        }
        return NotFound();
    }

    // DELETE api/<CategoriesController>/5
    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        var deleted = await categoryService.DeleteAsync(id);
        return !deleted ? BadRequest("Category is not deleted") : Ok(deleted);
    }
}
