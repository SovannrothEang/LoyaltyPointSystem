using LoyaltyPointSystem.Features.Identity.DTOs;
using Microsoft.AspNetCore.Identity;

namespace LoyaltyPointSystem.Features.Identity.Interfaces;

public interface IRoleService
{
    Task<IEnumerable<string>> GetAllRolesAsync(CancellationToken cancellationToken = default);
    Task<IEnumerable<string>> GetRoleNamesAsync(User user);
    Task AssignRoleToUserAsync(AssignRoleToUserCommand command);
}

public class RoleService(
    UserManager<User> userManager,
    RoleManager<Role> roleManager
    ) : IRoleService
{
    private readonly UserManager<User> _userManager = userManager;
    private readonly RoleManager<Role> _roleManager = roleManager;

    public Task<IEnumerable<string>> GetAllRolesAsync(CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<string>> GetRoleNamesAsync(User user)
    {
        throw new NotImplementedException();
    }

    public async Task AssignRoleToUserAsync(AssignRoleToUserCommand command)
    {
        var user = await _userManager.FindByNameAsync(command.UserName)
                   ?? throw new KeyNotFoundException($"No User was found with UserName: {command.UserName}!");

        var role = await _roleManager.FindByNameAsync(command.RoleName)
                   ?? throw new KeyNotFoundException($"No Role was found with Name: {command.RoleName}!");

        if (await _userManager.IsInRoleAsync(user, command.RoleName))
            throw new BadHttpRequestException($"User is already assigned to role: '{command.RoleName}'!");

        await _userManager.AddToRoleAsync(user, role.Name!);
    }
}