using LoyaltyPointSystem.Features.Identity.DTOs;
using LoyaltyPointSystem.Features.Identity.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LoyaltyPointSystem.Features.Identity.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController(IUserService userService) : ControllerBase
{
    private readonly IUserService _userService = userService;

    [HttpPost("login", Name = "LoginUser")]
    public async Task<ActionResult> LoginAsync(
        [FromBody] LoginCommand loginCommand)
    {
        var tokenResponse = await _userService.LoginAsync(loginCommand);
        return Ok(tokenResponse);
    }

    [HttpPost("register", Name = "RegisterUser")]
    public async Task<ActionResult> RegisterAsync(
        [FromBody] RegisterCommand registerCommand)
    {
       await _userService.RegisterAsync(registerCommand);
       return Ok(new { message =  "Register successfully!" }); 
    }
}