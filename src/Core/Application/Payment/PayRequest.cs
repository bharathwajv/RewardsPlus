namespace RewardsPlus.Application.Payment;
public class PayRequest
{
    public string UserName { get; set; }
    public double Amount { get; set; }
    public PayRequest(string userName, double amount)
    {
        UserName = userName;
        Amount = amount;
    }
}
