using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using TechStore.Api.DTO;
using TechStore.Domain.Entities;
using TechStore.Domain.Interfaces.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TechStore.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PropertiesController(IPropertyService propertyService, IValidator<PropertyDTO> propertyValidator, IMapper mapper) : ControllerBase
{
    // GET: api/<PropertiesController>
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var properties = await propertyService.GetAllAsync();
        var propertyDTO = mapper.Map<List<PropertyDTO>>(properties);
        return Ok(propertyDTO);
    }

    // GET api/<PropertiesController>/5
    [HttpGet("{id}")]
    public async Task<ActionResult> Get(int id)
    {
        var property = await propertyService.GetAsync(id);
        var propertyDTO = mapper.Map<PropertyDTO>(property);
        return property == null ? NotFound() : Ok(propertyDTO);
    }

    // POST api/<PropertiesController>
    [HttpPost]
    public async Task<ActionResult> Post([FromBody] PropertyDTO property)
    {
        var validationResult = propertyValidator.Validate(property);
        if (!validationResult.IsValid)
        {
            return BadRequest(validationResult.Errors[0].ToString());
        }
        var newProperty = mapper.Map<Property>(property);
        var created = await propertyService.CreateAsync(newProperty);
        var propertyDTO = mapper.Map<PropertyDTO>(created);
        return CreatedAtAction(nameof(Get), propertyDTO);
    }

    // PUT api/<PropertiesController>/5
    [HttpPut("{id}")]
    public async Task<ActionResult> Put(int id, [FromBody] PropertyDTO property)
    {

        var validationResult = propertyValidator.Validate(property);
        if (!validationResult.IsValid)
        {
            return BadRequest(validationResult.Errors[0].ToString());
        }
        var oldProperty = await propertyService.GetAsync(id);
        if (oldProperty != null)
        {
            mapper.Map<PropertyDTO, Property>(property, oldProperty);
            oldProperty.Id = id;
            var updated = await propertyService.UpdateAsync(oldProperty);
            var propertyDTO = mapper.Map<PropertyDTO>(updated);
            return Ok(propertyDTO);
        }
        return NotFound();
    }

    // DELETE api/<PropertiesController>/5
    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        var deleted = await propertyService.DeleteAsync(id);
        return deleted == false ? BadRequest("Property is not deleted") : Ok(deleted);
    }
}
