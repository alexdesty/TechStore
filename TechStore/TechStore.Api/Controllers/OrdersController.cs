using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TechStore.Api.DTO;
using TechStore.Domain.Entities;
using TechStore.Domain.Interfaces.Services;

namespace TechStore.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class OrdersController(IOrderService orderService, IValidator<OrderDTO> orderValidator, IMapper mapper) : ControllerBase
{
    // GET: api/<OrdersController>
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var orders = await orderService.GetAllAsync();
        var orderDTO = mapper.Map<List<OrderDTO>>(orders);
        return Ok(orderDTO);
    }

    // GET api/<OrdersController>/5
    [HttpGet("{id}")]
    public async Task<ActionResult> Get(int id)
    {
        var order = await orderService.GetAsync(id);
        var orderDTO = mapper.Map<OrderDTO>(order);
        return order == null ? NotFound() : Ok(orderDTO);
    }

    // POST api/<OrdersController>
    [HttpPost]
    public async Task<ActionResult> Post([FromBody] OrderDTO order)
    {
        var validationResult = orderValidator.Validate(order);
        if (!validationResult.IsValid)
        {
            return BadRequest(validationResult.Errors[0].ToString());
        }
        var newOrder = mapper.Map<Order>(order);
        var created = await orderService.CreateAsync(newOrder);
        var orderDTO = mapper.Map<OrderDTO>(created);
        return CreatedAtAction(nameof(Get), orderDTO);
    }

    // PUT api/<OrdersController>/5
    [HttpPut("{id}")]
    public async Task<ActionResult> Put(int id, [FromBody] OrderDTO order)
    {

        var validationResult = orderValidator.Validate(order);
        if (!validationResult.IsValid)
        {
            return BadRequest(validationResult.Errors[0].ToString());
        }
        var oldOrder = await orderService.GetAsync(id);
        if (oldOrder != null)
        {
            mapper.Map<OrderDTO, Order>(order, oldOrder);
            oldOrder.Id = id;
            var updated = await orderService.UpdateAsync(oldOrder);
            var orderDTO = mapper.Map<OrderDTO>(updated);
            return Ok(orderDTO);
        }
        return NotFound();
    }

    // DELETE api/<OrdersController>/5
    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        var deleted = await orderService.DeleteAsync(id);
        return deleted == false ? BadRequest("Order is not deleted") : Ok(deleted);
    }
}
