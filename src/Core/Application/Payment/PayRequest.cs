namespace RewardsPlus.Application.Payment;
public class PayRequest
{
    public string UserName { get; set; }
    public decimal Amount { get; set; }
    public PayRequest(string userName, decimal amount)
    {
        UserName = userName;
        Amount = amount;
    }
}
