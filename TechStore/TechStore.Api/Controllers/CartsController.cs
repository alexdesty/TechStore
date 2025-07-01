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
public class CartsController(ICartService cartService, ICartItemService cartItemService, IValidator<CartDTO> cartValidator, IValidator<CartItemDTO> cartItemValidator, IValidator<CartItemToCreateDTO> cartItemToCreateDTOValidator, IMapper mapper) : ControllerBase
{
    // GET: api/<CartsController>
    [HttpGet]
    public async Task<ActionResult<PaginatedList<CartDTO>>> Get(int pageIndex = 1, int pageSize = 5)
    {
        var carts = await cartService.GetAllAsync(pageIndex, pageSize);
        var cartDTO = mapper.Map<List<CartDTO>>(carts);
        var pagedDTO = new PaginatedList<CartDTO>(cartDTO, pageIndex, pageSize);
        return Ok(pagedDTO);
    }

    // GET api/<CartsController>/5
    [HttpGet("{id}")]
    public async Task<ActionResult> Get(int id)
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
        var validationResult = cartValidator.Validate(cart);
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
        var validationResult = cartValidator.Validate(cart);
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
        return !deleted ? BadRequest("Cart is not deleted") : Ok(deleted);
    }

    [HttpGet("{cartId}/cartitems")]
    public async Task<IActionResult> GetItemsByCartId(int cartId)
    {
        var cartItems = await cartItemService.GetItemsByCartIdAsync(cartId);
        if (cartItems == null)
        {
            return NotFound();
        }
        var cartItemsDTO = mapper.Map<List<CartItemDTO>>(cartItems);
        return Ok(cartItemsDTO);
    }

    [Route("{cartId}/cartitems")]
    [HttpPost]
    public async Task<ActionResult> PostCartItems([FromBody] CartItemToCreateDTO cartItem, int cartId)
    {
        var newCartItem = mapper.Map<CartItem>(cartItem);
        newCartItem.CartId = cartId;
        var created = await cartItemService.CreateAsync(newCartItem);
        var cartItemDTO = mapper.Map<CartItemDTO>(created);
        return CreatedAtAction(nameof(Get), cartItemDTO);
    }

    [HttpPut("{cartId}/cartitems/{cartItemId}")]
    public async Task<ActionResult> PutCartItems(int cartId, int cartItemId, [FromBody] CartItemToCreateDTO cartItem)
    {
        var validationResult = cartItemToCreateDTOValidator.Validate(cartItem);
        if (!validationResult.IsValid)
        {
            return BadRequest(validationResult.Errors[0].ToString());
        }
        var oldCartItem = await cartItemService.GetAsync(cartItemId);
        if (oldCartItem != null)
        {
            mapper.Map<CartItemToCreateDTO, CartItem>(cartItem, oldCartItem);
            oldCartItem.Id = cartItemId;
            oldCartItem.CartId = cartId;
            var updated = await cartItemService.UpdateAsync(oldCartItem);
            var cartItemDTO = mapper.Map<CartItemDTO>(updated);
            return Ok(cartItemDTO);
        }
        return NotFound();
    }

    [HttpDelete("{cartId}/cartitems")]
    public async Task<ActionResult> DeleteCartItemsByCardId(int cartId)
    {
        var deleted = await cartItemService.DeleteCartItemsByCartId(cartId);
        return !deleted ? BadRequest("Cart with items is not deleted") : Ok(deleted);
    }
}
