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
public class CartsController(ICartService cartService, IValidator<CartDTO> CartValidator, IMapper mapper, ICartItemService cartItemService, IValidator<CartItemDTO> cartItemValidator) : ControllerBase
{
    // GET: api/<CartsController>

    [HttpGet]
    public async Task<IActionResult> GetCarts()
    {
        var carts = await cartService.GetAllAsync();
        var cartDTO = mapper.Map<List<CartDTO>>(carts);
        return Ok(cartDTO);
    }

    // GET api/<CartsController>/5
    [HttpGet("{id}")]
    public async Task<ActionResult> GetCart(int id)
    {
        var cart = await cartService.GetAsync(id);
        if (cart == null)
        {
            return NotFound();
        }
        var cartDTO = mapper.Map<CartDTO>(cart);
        return Ok(cartDTO);
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
        return CreatedAtAction(nameof(GetCarts), cartDTO);
    }

    // PUT api/<CartsController>/5
    [HttpPut("{id}")]
    public async Task<ActionResult> PutCart(int id, [FromBody] CartDTO cart)
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
    public async Task<ActionResult> DeleteCart(int id)
    {
        var deleted = await cartService.DeleteAsync(id);
        return deleted == false ? BadRequest("Cart is not deleted") : Ok(deleted);
    }

    [Route("/cartitems")]
    [HttpGet]
    public async Task<IActionResult> GetAllCartItems()
    {
        var cartItems = await cartItemService.GetAllAsync();
        var cartItemsDTO = mapper.Map<List<CartItemDTO>>(cartItems);
        return Ok(cartItemsDTO);
    }

    [HttpGet("{id}/cartitems")]
    public async Task<IActionResult> GetItemsByCartId(int id)
    {
        var cartItems = await cartItemService.GetItemsByCartIdAsync(id);
        if (cartItems == null)
        {
            return NotFound();
        }
        var cartItemsDTO = mapper.Map<List<CartItemDTO>>(cartItems);
        return Ok(cartItemsDTO);
    }
    [Route("/cartitems")]
    [HttpPost]
    public async Task<ActionResult> PostCartItems([FromBody] CartItemDTO cartItem)
    {
        var validationResult = cartItemValidator.Validate(cartItem);
        if (!validationResult.IsValid)
        {
            return BadRequest(validationResult.Errors[0].ToString());
        }
        var newCartItem = mapper.Map<CartItem>(cartItem);
        var created = await cartItemService.CreateAsync(newCartItem);
        var cartItemDTO = mapper.Map<CartItemDTO>(created);
        return CreatedAtAction(nameof(GetAllCartItems), cartItemDTO);
    }

    [HttpPut("cartitems/{id}")]
    public async Task<ActionResult> PutCartItems(int id, [FromBody] CartItemDTO cartItem)
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

    [HttpDelete("cartitems/{id}")]
    public async Task<ActionResult> DeleteCartItems(int id)
    {
        var deleted = await cartItemService.DeleteAsync(id);
        return deleted == false ? BadRequest("Cart with items is not deleted") : Ok(deleted);
    }
}
