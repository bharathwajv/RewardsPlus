namespace RewardsPlus.Application.Token;

public class TokenDto : IDto
{
    public double Balance { get; set; }
    public string? UserId { get; set; }
    public string? UserEmail { get; set; }
}