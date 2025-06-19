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
public class CartsController(ICartService cartService, IValidator<CartDTO> CartValidator, IMapper mapper) : ControllerBase
{
    // GET: api/<CartsController>
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var carts = await cartService.GetAllAsync();
        var cartDTO = mapper.Map<List<CartDTO>>(carts);
        return Ok(cartDTO);
    }

    // GET api/<CartsController>/5
    [HttpGet("{id}")]
    public async Task<ActionResult> Get(int id)
    {
        var cart = await cartService.GetAsync(id);
        var cartDTO = mapper.Map<CartDTO>(cart);
        return cart == null ? NotFound() : Ok(cartDTO);
    }

    // POST api/<CartsController>
    [HttpPost]
    public async Task<ActionResult> Post([FromBody] CartDTO cart)
    {
        var validationResult = CartValidator.Validate(cart);
        if (!validationResult.IsValid)
        {
            return BadRequest(validationResult.Errors[0].ToString());
        }
        var newCart = mapper.Map<Cart>(cart);
        var created = await cartService.CreateAsync(newCart);
        var cartDTO = mapper.Map<CartDTO>(created);
        return CreatedAtAction(nameof(Get), cartDTO);
    }

    // PUT api/<CartsController>/5
    [HttpPut("{id}")]
    public async Task<ActionResult> Put(int id, [FromBody] CartDTO cart)
    {

        var validationResult = CartValidator.Validate(cart);
        if (!validationResult.IsValid)
        {
            return BadRequest(validationResult.Errors[0].ToString());
        }
        var oldCart = await cartService.GetAsync(id);
        if (oldCart != null)
        {
            mapper.Map<CartDTO, Cart>(cart, oldCart);
            oldCart.Id = id;
            var updated = await cartService.UpdateAsync(oldCart);
            var cartDTO = mapper.Map<CartDTO>(updated);
            return Ok(cartDTO);
        }
        return NotFound();
    }

    // DELETE api/<CartsController>/5
    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        var deleted = await cartService.DeleteAsync(id);
        return deleted == false ? BadRequest("Cart is not deleted") : Ok(deleted);
    }
}
