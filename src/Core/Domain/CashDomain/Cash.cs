namespace RewardsPlus.Domain.CashDomain;

public class Cash : AuditableEntity, IAggregateRoot
{
    public decimal Balance { get; private set; }
    public string UserId { get; private set; }
    public string UserEmail { get; private set; }

    public Cash(decimal balance, string userId, string userEmail)
    {
        Balance = balance;
        UserId = userId;
        UserEmail = userEmail;
    }

    public Cash Update(decimal balance)
    {
        if (Balance != balance) Balance = balance;
        return this;
    }
}
