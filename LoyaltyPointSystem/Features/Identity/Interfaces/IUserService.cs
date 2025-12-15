using LoyaltyPointSystem.Features.Identity.DTOs;

namespace LoyaltyPointSystem.Features.Identity.Interfaces;

public interface IUserService
{
    Task<TokenResponseDto?> LoginAsync(LoginCommand loginCommand);
    Task RegisterAsync(RegisterCommand registerCommand);
}