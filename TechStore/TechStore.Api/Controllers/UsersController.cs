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
public class UsersController(IUserService userService, IValidator<UserDTO> userValidator, IMapper mapper) : ControllerBase
{
    // GET: api/<UsersController>
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var users = await userService.GetAllAsync();
        var userDTO = mapper.Map<List<UserDTO>>(users);
        return Ok(userDTO);
    }

    // GET api/<UsersController>/5
    [HttpGet("{id}")]
    public async Task<ActionResult> Get(int id)
    {
        var user = await userService.GetAsync(id);
        var userDTO = mapper.Map<UserDTO>(user);
        return user == null ? NotFound() : Ok(userDTO);
    }

    // POST api/<UsersController>
    [HttpPost]
    public async Task<ActionResult> Post([FromBody] UserDTO user)
    {
        var validationResult = userValidator.Validate(user);
        if (!validationResult.IsValid)
        {
            return BadRequest(validationResult.Errors[0].ToString());
        }
        var newUser = mapper.Map<User>(user);
        var created = await userService.CreateAsync(newUser);
        var userDTO = mapper.Map<UserDTO>(created);
        return CreatedAtAction(nameof(Get), userDTO);
    }

    // PUT api/<UsersController>/5
    [HttpPut("{id}")]
    public async Task<ActionResult> Put(int id, [FromBody] UserDTO user)
    {

        var validationResult = userValidator.Validate(user);
        if (!validationResult.IsValid)
        {
            return BadRequest(validationResult.Errors[0].ToString());
        }
        var oldUser = await userService.GetAsync(id);
        if (oldUser != null)
        {
            mapper.Map<UserDTO, User>(user, oldUser);
            oldUser.Id = id;
            var updated = await userService.UpdateAsync(oldUser);
            var userDTO = mapper.Map<UserDTO>(updated);
            return Ok(userDTO);
        }
        return NotFound();
    }

    // DELETE api/<UsersController>/5
    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        var deleted = await userService.DeleteAsync(id);
        return deleted == false ? BadRequest("User is not deleted") : Ok(deleted);
    }
}
