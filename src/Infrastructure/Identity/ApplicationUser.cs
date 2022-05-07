using Microsoft.AspNetCore.Identity;

namespace RewardsPlus.Infrastructure.Identity;

public class ApplicationUser : IdentityUser
{
    public string? ImageUrl { get; set; }
    public bool IsActive { get; set; }
    public string? RefreshToken { get; set; }
    public DateTime RefreshTokenExpiryTime { get; set; }
    public string? ObjectId { get; set; }
}