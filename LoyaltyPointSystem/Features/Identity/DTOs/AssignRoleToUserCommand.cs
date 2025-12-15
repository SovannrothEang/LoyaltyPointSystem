namespace LoyaltyPointSystem.Features.Identity.DTOs;

public record AssignRoleToUserCommand(string UserName, string RoleName);