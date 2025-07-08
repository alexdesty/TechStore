using AutoMapper;
using FluentValidation;
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
public class ProductsController(IProductService productService, IValidator<ProductDTO> productValidator, IMapper mapper, IFileUploadService fileUploadService) : ControllerBase
{
    // GET: api/<ProductsController>
    [HttpGet]
    public async Task<ActionResult<PaginatedList<ProductDTO>>> Get(int pageIndex = 1, int pageSize = 1)
    {
        var products = await productService.GetAllAsync(pageIndex, pageSize);
        var productDTO = mapper.Map<List<ProductDTO>>(products);
        var pagedDTO = new PaginatedList<ProductDTO>(productDTO, pageIndex, pageSize);
        return Ok(pagedDTO);
    }

    // GET api/<ProductsController>/5
    [HttpGet("{id}")]
    public async Task<ActionResult> Get(int id)
    {
        var product = await productService.GetAsync(id);
        if (product == null)
        {
            return NotFound();
        }
        var productDTO = mapper.Map<ProductDTO>(product);
        return Ok(productDTO);
    }

    // POST api/<ProductsController>
    [HttpPost]
    public async Task<ActionResult> Post([FromBody] ProductDTO product)
    {
        var validationResult = productValidator.Validate(product);
        if (!validationResult.IsValid)
        {
            return BadRequest(validationResult.Errors[0].ToString());
        }
        var newProduct = mapper.Map<Product>(product);
        var created = await productService.CreateAsync(newProduct);
        var productDTO = mapper.Map<ProductDTO>(created);
        return CreatedAtAction(nameof(Get), productDTO);
    }

    // PUT api/<ProductsController>/5
    [HttpPut("{id}")]
    public async Task<ActionResult> Put(int id, [FromBody] ProductDTO product)
    {

        var validationResult = productValidator.Validate(product);
        if (!validationResult.IsValid)
        {
            return BadRequest(validationResult.Errors[0].ToString());
        }
        var oldProduct = await productService.GetAsync(id);
        if (oldProduct != null)
        {
            mapper.Map<ProductDTO, Product>(product, oldProduct);
            oldProduct.Id = id;
            var updated = await productService.UpdateAsync(oldProduct);
            var productDTO = mapper.Map<ProductDTO>(updated);
            return Ok(productDTO);
        }
        return NotFound();
    }

    // DELETE api/<ProductsController>/5
    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        var deleted = await productService.DeleteAsync(id);
        return !deleted ? BadRequest("Product is not deleted") : Ok(deleted);
    }

    [HttpPost("{id}/uploadImage")]
    public async Task<IActionResult> UploadImage(int id, IFormFile file)
    {
        if (file == null || file.Length == 0)
        {
            return BadRequest("No file uploaded.");
        }

        var filename = await productService.UploadImageAsync(id, file);

        return Ok(new { FileName = filename });
    }
}
