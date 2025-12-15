namespace LoyaltyPointSystem.Features.Identity.DTOs;

public record UserProfileDto(string Id, string UserName, string Email, IList<string> Roles);