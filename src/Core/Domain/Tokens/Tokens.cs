namespace RewardsPlus.Domain.Catalog;

public class Tokens : AuditableEntity, IAggregateRoot
{
    public double Value { get; private set; }
    public string? Description { get; private set; }

    public Tokens(string name, string? description)
    {
        Name = name;
        Description = description;
    }

    public Tokens Update(string? name, string? description)
    {
        if (name is not null && Name?.Equals(name) is not true) Name = name;
        if (description is not null && Description?.Equals(description) is not true) Description = description;
        return this;
    }
}