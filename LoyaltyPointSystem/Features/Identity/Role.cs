using Microsoft.AspNetCore.Identity;

namespace LoyaltyPointSystem.Features.Identity;

public sealed class Role : IdentityRole<string>
{
    public Role()
        => Id = Guid.NewGuid().ToString();
    
    public Role(string id)
        => Id = id;
}
