namespace RewardsPlus.Application.Cash;

public class CashDto : IDto
{
    public double Balance { get; set; }
    public string? UserId { get; set; }
    public string? UserEmail { get; set; }
}