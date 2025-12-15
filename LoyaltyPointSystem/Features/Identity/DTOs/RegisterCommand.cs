namespace LoyaltyPointSystem.Features.Identity.DTOs;

public record RegisterCommand(string UserName, string Email, string Password);