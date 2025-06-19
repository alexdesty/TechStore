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
public class ProductsToPropertiesController(IProductToPropertyService productToPropertyService, IValidator<ProductToPropertyDTO> productToPropertyValidator, IMapper mapper) : ControllerBase
{
    // GET: api/<ProductsToPropertiesController>
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var productsToProperties = await productToPropertyService.GetAllAsync();
        var productToPropertyDTO = mapper.Map<List<ProductToPropertyDTO>>(productsToProperties);
        return Ok(productToPropertyDTO);
    }

    // GET api/<ProductsToPropertiesController>/5
    [HttpGet("{id}")]
    public async Task<ActionResult> Get(int id)
    {
        var productToProperty = await productToPropertyService.GetAsync(id);
        var productToPropertyDTO = mapper.Map<ProductToPropertyDTO>(productToProperty);
        return productToProperty == null ? NotFound() : Ok(productToPropertyDTO);
    }

    // POST api/<ProductsToPropertiesController>
    [HttpPost]
    public async Task<ActionResult> Post([FromBody] ProductToPropertyDTO productToProperty)
    {
        var validationResult = productToPropertyValidator.Validate(productToProperty);
        if (!validationResult.IsValid)
        {
            return BadRequest(validationResult.Errors[0].ToString());
        }
        var newProductToProperty = mapper.Map<ProductToProperty>(productToProperty);
        var created = await productToPropertyService.CreateAsync(newProductToProperty);
        var productToPropertyDTO = mapper.Map<ProductToPropertyDTO>(created);
        return CreatedAtAction(nameof(Get), productToPropertyDTO);
    }

    // PUT api/<ProductsToPropertiesController>/5
    [HttpPut("{id}")]
    public async Task<ActionResult> Put(int id, [FromBody] ProductToPropertyDTO productToProperty)
    {

        var validationResult = productToPropertyValidator.Validate(productToProperty);
        if (!validationResult.IsValid)
        {
            return BadRequest(validationResult.Errors[0].ToString());
        }
        var oldProductToProperty = await productToPropertyService.GetAsync(id);
        if (oldProductToProperty != null)
        {
            mapper.Map<ProductToPropertyDTO, ProductToProperty>(productToProperty, oldProductToProperty);
            oldProductToProperty.Id = id;
            var updated = await productToPropertyService.UpdateAsync(oldProductToProperty);
            var productToPropertyDTO = mapper.Map<ProductToPropertyDTO>(updated);
            return Ok(productToPropertyDTO);
        }
        return NotFound();
    }

    // DELETE api/<ProductsToPropertiesController>/5
    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        var deleted = await productToPropertyService.DeleteAsync(id);
        return deleted == false ? BadRequest("ProductToProperty is not deleted") : Ok(deleted);
    }
}
