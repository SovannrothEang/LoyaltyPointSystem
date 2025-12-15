using Microsoft.AspNetCore.Identity;

namespace LoyaltyPointSystem.Features.Identity;

public sealed class User : IdentityUser<string>
{
    public User()
    {
        Id = Guid.NewGuid().ToString();
        SecurityStamp = Guid.NewGuid().ToString();
    }

    public User(string id)
    {
        Id = id;
        SecurityStamp = Guid.NewGuid().ToString();
    }
}
