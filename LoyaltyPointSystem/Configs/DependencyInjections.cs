using System.Text;
using LoyaltyPointSystem.Features.Identity.Interfaces;
using Microsoft.IdentityModel.Tokens;

namespace LoyaltyPointSystem.Configs;

public static class DependencyInjections
{
    public static IServiceCollection AddDependencyInjections(this IServiceCollection services)
    {
        services.AddScoped<ITokenService, TokenService>();

        return services;
    }
    
    extension(WebApplicationBuilder builder)
    {
        public IServiceCollection AddAuthentication()
        {
            var jwtIssuer =  builder.Configuration["Jwt:Issuer"];
            var jwtAudience = builder.Configuration["Jwt:Audience"];
            var jwtKey = builder.Configuration["Jwt:Key"]
                         ?? throw new Exception("Missing JWT Key!");

            builder.Services.AddAuthentication()
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = !string.IsNullOrWhiteSpace(jwtIssuer),
                        ValidIssuer = jwtIssuer,
                        ValidateAudience = !string.IsNullOrWhiteSpace(jwtAudience),
                        ValidAudience = jwtAudience,
                        ValidateIssuerSigningKey = true,
                        RequireExpirationTime = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey))
                    };
                });
        
            return builder.Services;
        }

        public IServiceCollection AddAuthorization()
        {
            builder.Services.AddAuthorization();
        
            return builder.Services;
        }
    }
}