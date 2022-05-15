namespace RewardsPlus.Application.Cash;

public class GiftingInfoDto : IDto
{
    public string FromUserEmail { get; set; }
    public string GiftMessage { get; set; }
    public string? GiftImage { get; set; }
    public decimal Amount { get; set; }
    public DateTime CreatedOn { get; set; }
}