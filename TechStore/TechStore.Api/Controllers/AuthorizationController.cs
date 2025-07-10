using Microsoft.AspNetCore.Mvc;
using TechStore.Api.DTO;
using TechStore.Domain.Interfaces.Services;
using TechStore.Domain.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TechStore.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthorizationController(ITokensService tokensService) : ControllerBase
{
    [HttpPost("login")]
    public IActionResult Login([FromBody] LoginDTO loginDTO)
    {
        var (token, expiration) = tokensService.GenerateAccessToken();
        return Ok(new
        {
            token,
            expiration,
        });
    }
}