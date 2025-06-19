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
public class CartsItemsController(ICartItemService cartItemService, IValidator<CartItemDTO> cartItemValidator, IMapper mapper) : ControllerBase
{
    // GET: api/<CartsItemsController>
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var cartsItems = await cartItemService.GetAllAsync();
        var cartItemDTO = mapper.Map<List<CartItemDTO>>(cartsItems);
        return Ok(cartItemDTO);
    }

    // GET api/<CartsItemsController>/5
    [HttpGet("{id}")]
    public async Task<ActionResult> Get(int id)
    {
        var cartItem = await cartItemService.GetAsync(id);
        var cartItemDTO = mapper.Map<CartItemDTO>(cartItem);
        return cartItem == null ? NotFound() : Ok(cartItemDTO);
    }

    // POST api/<CartsItemsController>
    [HttpPost]
    public async Task<ActionResult> Post([FromBody] CartItemDTO cartItem)
    {
        var validationResult = cartItemValidator.Validate(cartItem);
        if (!validationResult.IsValid)
        {
            return BadRequest(validationResult.Errors[0].ToString());
        }
        var newCartItem = mapper.Map<CartItem>(cartItem);
        var created = await cartItemService.CreateAsync(newCartItem);
        var cartItemDTO = mapper.Map<CartItemDTO>(created);
        return CreatedAtAction(nameof(Get), cartItemDTO);
    }

    // PUT api/<CartsItemsController>/5
    [HttpPut("{id}")]
    public async Task<ActionResult> Put(int id, [FromBody] CartItemDTO cartItem)
    {

        var validationResult = cartItemValidator.Validate(cartItem);
        if (!validationResult.IsValid)
        {
            return BadRequest(validationResult.Errors[0].ToString());
        }
        var oldCartItem = await cartItemService.GetAsync(id);
        if (oldCartItem != null)
        {
            mapper.Map<CartItemDTO, CartItem>(cartItem, oldCartItem);
            oldCartItem.Id = id;
            var updated = await cartItemService.UpdateAsync(oldCartItem);
            var cartItemDTO = mapper.Map<CartItemDTO>(updated);
            return Ok(cartItemDTO);
        }
        return NotFound();
    }

    // DELETE api/<CartsItemsController>/5
    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        var deleted = await cartItemService.DeleteAsync(id);
        return deleted == false ? BadRequest("Cart with items is not deleted") : Ok(deleted);
    }
}
