namespace RewardsPlus.Domain.Catalog;

public class Cash : AuditableEntity, IAggregateRoot
{
    public double Balance { get; private set; }
    public string UserId { get; private set; }
    public string UserEmail { get; private set; }

    public Cash(double balance, string userId, string userEmail)
    {
        Balance = balance;
        UserId = userId;
        UserEmail = userEmail;
    }

    public Cash Update(double balance)
    {
        if (Balance != balance) Balance = balance;
        return this;
    }
}
