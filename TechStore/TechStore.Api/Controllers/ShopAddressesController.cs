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
    public class ShopAddressesController(IShopAddressService shopAddressService, IValidator<ShopAddressDTO> shopAddressValidator, IMapper mapper) : ControllerBase
    {
        // GET: api/<ShopAddressesController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ShopAddress>>> Get()
        {
            var snopAddresses = await shopAddressService.GetAllAsync();
            var shopAddressesDTO = mapper.Map<ShopAddressDTO>(snopAddresses);
            return Ok(shopAddressesDTO);
        }

        // GET api/<ShopAddressesController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult> Get(int id)
        {
            var shopAddress = await shopAddressService.GetAsync(id);
            var shopAddressDTO = mapper.Map<ShopAddressDTO>(shopAddress);
            return shopAddress == null ? NotFound() : Ok(shopAddressDTO);
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
            var uri = new Uri($"api/shopaddresses/{created.Id}");
            return Created(uri, shopAddressDTO);
        }

        // PUT api/<ShopAddressesController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] ShopAddressDTO shopAddress)
        {
            var oldShopAddress = await shopAddressService.GetAsync(id);
            var validationResult = shopAddressValidator.Validate(shopAddress);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors[0].ToString());
            }
            if (oldShopAddress != null)
            {
                ShopAddress edited = mapper.Map<ShopAddress>(shopAddress);
                edited.Id = id;
                var updated = await shopAddressService.UpdateAsync(edited);
                var shopAddressDTO = mapper.Map<CategoryDTO>(updated);
                return Ok(shopAddressDTO);
            }
            return NotFound();
        }

        // DELETE api/<ShopAddressesController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var deleted = await shopAddressService.DeleteAsync(id);
            return deleted == false ? BadRequest("Shop address is not deleted") : Ok(deleted);
        }
    }
}
