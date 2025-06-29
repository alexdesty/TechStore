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
public class ShopAddressesController(IShopAddressService shopAddressService, IValidator<ShopAddressDTO> shopAddressValidator, IMapper mapper) : ControllerBase
{
    // GET: api/<ShopAddressesController>
    [HttpGet]
    public async Task<ActionResult<PaginatedList<ShopAddressDTO>>> Get(int pageIndex = 1, int pageSize = 1)
    {
        var shopAddresses = await shopAddressService.GetAllAsync(pageIndex, pageSize);
        var shopAddressDTO = mapper.Map<List<ShopAddressDTO>>(shopAddresses);
        var pagedDTO = new PaginatedList<ShopAddressDTO>(shopAddressDTO, pageIndex, pageSize);
        return Ok(pagedDTO);
    }

    // GET api/<ShopAddressesController>/5
    [HttpGet("{id}")]
    public async Task<ActionResult> Get(int id)
    {
        var shopAddress = await shopAddressService.GetAsync(id);
        if (shopAddress == null)
        {
            return NotFound();
        }
        var shopAddressDTO = mapper.Map<ShopAddressDTO>(shopAddress);
        return Ok(shopAddressDTO);
    }

    // POST api/<ShopAddressesController>
    [HttpPost]
    public async Task<ActionResult> Post([FromBody] ShopAddressDTO shopAddress)
    {
        var validationResult = shopAddressValidator.Validate(shopAddress);
        if (!validationResult.IsValid)
        {
            return BadRequest(validationResult.Errors[0].ToString());
        }
        var newShopAddress = mapper.Map<ShopAddress>(shopAddress);
        var created = await shopAddressService.CreateAsync(newShopAddress);
        var shopAddressDTO = mapper.Map<ShopAddressDTO>(created);
        return CreatedAtAction(nameof(Get), shopAddressDTO);
    }

    // PUT api/<ShopAddressesController>/5
    [HttpPut("{id}")]
    public async Task<ActionResult> Put(int id, [FromBody] ShopAddressDTO shopAddress)
    {

        var validationResult = shopAddressValidator.Validate(shopAddress);
        if (!validationResult.IsValid)
        {
            return BadRequest(validationResult.Errors[0].ToString());
        }
        var oldShopAddress = await shopAddressService.GetAsync(id);
        if (oldShopAddress != null)
        {
            mapper.Map<ShopAddressDTO, ShopAddress>(shopAddress, oldShopAddress);
            oldShopAddress.Id = id;
            var updated = await shopAddressService.UpdateAsync(oldShopAddress);
            var shopAddressDTO = mapper.Map<ShopAddressDTO>(updated);
            return Ok(shopAddressDTO);
        }
        return NotFound();
    }

    // DELETE api/<ShopAddressesController>/5
    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        var deleted = await shopAddressService.DeleteAsync(id);
        return !deleted ? BadRequest("Shop address is not deleted") : Ok(deleted);
    }
}
