using LoyaltyPointSystem.Features.Identity.DTOs;
using LoyaltyPointSystem.Features.Identity.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LoyaltyPointSystem.Features.Identity.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Tags("Roles")]
    public class RolesController(IRoleService roleService) : ControllerBase
    {
        private readonly IRoleService _roleService = roleService;

        [HttpPost("assign-user")]
        public async Task<ActionResult> AssginRoleUser([FromBody] AssignRoleToUserCommand command)
        {
            await _roleService.AssignRoleToUserAsync(command);
            return Ok(new { message = $"Role '{command.RoleName}' was assigned to User '{command.UserName}'" });
        }
    }
}
