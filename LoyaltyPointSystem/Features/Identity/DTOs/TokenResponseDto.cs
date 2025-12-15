namespace LoyaltyPointSystem.Features.Identity.DTOs;

public record TokenResponseDto(string Token,  DateTime ExpiresAt);