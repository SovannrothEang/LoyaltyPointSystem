using System.Text;
using LoyaltyPointSystem.Data;
using LoyaltyPointSystem.Features.Identity;
using LoyaltyPointSystem.Features.Identity.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace LoyaltyPointSystem.Configs;

public static class DependencyInjections
{
    extension(WebApplicationBuilder builder)
    {
        public IServiceCollection AddDependencyInjections()
        {
            builder.Services.AddScoped<ITokenService, TokenService>();

            return builder.Services;
        }
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

        public IServiceCollection AddIdentity()
        {
            builder.Services
                .AddIdentity<User, Role>(options =>
                {
                    options.Password.RequiredLength = 8;
                    options.Password.RequireNonAlphanumeric = true;
                    options.Password.RequireUppercase = true;
                    options.Password.RequireLowercase = true;
                    options.Password.RequireDigit = true;
                })
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddSignInManager()
                .AddDefaultTokenProviders();
            
            return builder.Services;
        }
    }
}