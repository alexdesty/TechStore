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
public class OrdersController(IOrderService orderService, IValidator<OrderDTO> orderValidator, IMapper mapper) : ControllerBase
{
    // GET: api/<OrdersController>
    [HttpGet]
    public async Task<ActionResult<PaginatedList<OrderDTO>>> Get(int pageIndex = 1, int pageSize = 1)
    {
        var orders = await orderService.GetAllAsync(pageIndex, pageSize);
        var orderDTO = mapper.Map<List<OrderDTO>>(orders);
        var pagedDTO = new PaginatedList<OrderDTO>(orderDTO, pageIndex, pageSize);
        return Ok(pagedDTO);
    }

    // GET api/<OrdersController>/5
    [HttpGet("{id}")]
    public async Task<ActionResult> Get(int id)
    {
        var order = await orderService.GetAsync(id);
        if (order == null)
        {
            return NotFound();
        }
        var orderDTO = mapper.Map<OrderDTO>(order);
        return Ok(orderDTO);
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
        return !deleted ? BadRequest("Order is not deleted") : Ok(deleted);
    }
}