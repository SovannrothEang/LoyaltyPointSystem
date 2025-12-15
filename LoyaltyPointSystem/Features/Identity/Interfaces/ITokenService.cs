using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace LoyaltyPointSystem.Features.Identity.Interfaces;

public interface ITokenService
{
    Task<(string token, DateTime expiresAt)> GenerateTokenAsync(User user);
    Task<(string token, DateTime expiresAt)> GenerateTokenAsync(User user, DateTime expiresAt);
}

public class TokenService(
    IConfiguration configuration,
    UserManager<User> userManager) : ITokenService
{
    private readonly IConfiguration _configuration = configuration;
    private readonly UserManager<User> _userManager = userManager;

    public async Task<(string token, DateTime expiresAt)> GenerateTokenAsync(User user)
    {
        return await GenerateTokenAsync(user, DateTime.Now.AddHours(1));
    }

    public async Task<(string token, DateTime expiresAt)> GenerateTokenAsync(User user, DateTime expiresAt)
    {
        List<Claim> claims = [
            new (JwtRegisteredClaimNames.Sub, user.Id),
            new (JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new (ClaimTypes.NameIdentifier, user.Id),
            new (ClaimTypes.Email, user.Email ?? string.Empty)
        ];

        var roles = await _userManager.GetRolesAsync(user);
        if (roles.Any())
        {
            claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));
        }
        
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Key"]!));
        var credential = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var token = new JwtSecurityToken(
            issuer: _configuration["Jwt:Issuer"],
            audience: _configuration["Jwt:Audience"],
            claims: claims,
            expires: expiresAt,
            signingCredentials: credential);

        return (new JwtSecurityTokenHandler().WriteToken(token), token.ValidTo);
    }
}