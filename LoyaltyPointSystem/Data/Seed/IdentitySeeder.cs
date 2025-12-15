using LoyaltyPointSystem.Features.Identity;
using Microsoft.AspNetCore.Identity;

namespace LoyaltyPointSystem.Data.Seed;

public static class IdentitySeeder
{
    public static async Task<WebApplication> IdentitySeedAsync(this WebApplication app)
    {
        var serviceProvider = app.Services.CreateScope().ServiceProvider;
        var roleManager = serviceProvider.GetRequiredService<RoleManager<Role>>();
        var userManager = serviceProvider.GetRequiredService<UserManager<User>>();

        (string Id, string Name)[] roles = {
            ("ADMINB6F-3063-484E-8D3B-3600F209B391","Admin"),
            ("USER7598-C50F-439D-AAF4-AE5EC63D1B93","User")
        };

        foreach (var (id, name) in roles)
        {
            if (!await roleManager.RoleExistsAsync(name))
                await roleManager.CreateAsync(new Role { Id = id, Name = name });
        }

        const string adminId = "ADMINC8F-D914-483D-BF41-7DA09ABAA4DC";
        const string adminUser = "admin";
        const string adminPass = "Admin123!"; // change in production
        const string adminEmail = "admin@example.com";

        var admin = await userManager.FindByNameAsync(adminUser);

        if (admin is null)
        {
            admin = new User { Id = adminId, UserName = adminUser, Email = adminEmail };
            var isExist = await userManager.FindByEmailAsync(adminEmail);
            if (isExist == null)
            {
                var created = await userManager.CreateAsync(admin, adminPass);
                if (created.Succeeded)
                    await userManager.AddToRoleAsync(admin, roles[0].Name);
            }
        }

        return app;
    }
}

