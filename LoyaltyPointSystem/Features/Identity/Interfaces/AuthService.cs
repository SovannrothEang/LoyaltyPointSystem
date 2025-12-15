using AutoMapper;
using LoyaltyPointSystem.Features.Identity.DTOs;
using Microsoft.AspNetCore.Identity;

namespace LoyaltyPointSystem.Features.Identity.Interfaces;

public class AuthService(
    UserManager<User> userManager,
    SignInManager<User> signInManager,
    ITokenService tokenService,
    IMapper mapper)
    : IUserService
{
    private readonly UserManager<User> _userManager = userManager;
    private readonly SignInManager<User> _signInManager = signInManager;
    private readonly ITokenService _tokenService = tokenService;
    private readonly IMapper _mapper = mapper;

    public async Task<TokenResponseDto?> LoginAsync(LoginCommand loginCommand)
    {
        var user = await _userManager.FindByNameAsync(loginCommand.UserName);
        if (user == null)
            throw new UnauthorizedAccessException();
        var result = await _signInManager.CheckPasswordSignInAsync(user, loginCommand.Password, false);
        if (!result.Succeeded)
            throw new UnauthorizedAccessException();
        var (token, expiresAt) = await _tokenService.GenerateTokenAsync(user);
        return new TokenResponseDto(token, expiresAt);
    }

    public async Task RegisterAsync(RegisterCommand registerCommand)
    {
        var exist = await _userManager.FindByEmailAsync(registerCommand.Email);
        if (exist != null)
            throw new BadHttpRequestException("Invalid credentials!");
        var username = await _userManager.FindByNameAsync(registerCommand.UserName);
        if (username != null)
            throw new BadHttpRequestException("Invalid credentials!");
        var user = _mapper.Map<User>(registerCommand);
        var result = await _userManager.CreateAsync(user, registerCommand.Password);
        if (!result.Succeeded)
            throw new BadHttpRequestException("Invalid credentials!");
    }
}