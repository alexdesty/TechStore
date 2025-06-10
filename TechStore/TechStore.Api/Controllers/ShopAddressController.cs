using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using TechStore.Api.DTO;
using TechStore.Domain.Entities;
using TechStore.Domain.Interfaces.Services;
using TechStore.Domain.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TechStore.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShopAddressController(IShopAddressService shopAddressService, IValidator<ShopAddressDTO> shopAddressValidator, IMapper mapper) : ControllerBase
    {
        // GET: api/<ShopAddressController>
        [HttpGet]
        public async Task<IEnumerable<ShopAddress>> Get()
        {
            return await shopAddressService.GetAllAsync();
        }

        // GET api/<ShopAddressController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult> Get(int id)
        {
            var shopAddress = await shopAddressService.GetAsync(id);
            return shopAddress == null ? NotFound() : Ok(shopAddress);
        }

        // POST api/<ShopAddressController>
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
            return Ok(created);
        }

        // PUT api/<ShopAddressController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] ShopAddressDTO shopAddress)
        {
            var oldShopAddress = await shopAddressService.GetAsync(id);
            if (oldShopAddress != null)
            {
                ShopAddress edited = mapper.Map<ShopAddress>(shopAddress);
                edited.Id = id;
                var updated = await shopAddressService.UpdateAsync(edited);
                return Ok(updated);
            }
            return BadRequest();
        }

        // DELETE api/<ShopAddressController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var deleted = await shopAddressService.DeleteAsync(id);
            return Ok(deleted);
        }
    }
}
