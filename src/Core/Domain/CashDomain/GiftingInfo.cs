namespace RewardsPlus.Domain.CashDomain;

public class GiftingInfo : AuditableEntity, IAggregateRoot
{
    public string FromUserEmail { get; private set; }
    public string ToUserEmail { get; private set; }
    public string GiftMessage { get; private set; }
    public string? GiftImage { get; private set; }
    public bool IsViewed { get; private set; }
    public decimal Amount { get; set; }

    //for mapster shit to work
    public GiftingInfo()
    {
    }
    public GiftingInfo(string fromUserEmail, string toUserEmail, string giftMessage, string giftImage = null, bool isViewed = false, decimal amount = 0)
    {
        FromUserEmail = fromUserEmail;
        ToUserEmail = toUserEmail;
        GiftMessage = giftMessage;
        GiftImage = giftImage;
        IsViewed = isViewed;
        Amount = amount;
    }

    public GiftingInfo UpdateGiftInfo(string fromUserEmail, string toUserEmail, string giftMessage, string giftImage = null, bool isViewed = false, decimal amount = 0)
    {

        if (fromUserEmail is not null && FromUserEmail?.Equals(fromUserEmail) is not true) FromUserEmail = fromUserEmail;
        if (toUserEmail is not null && ToUserEmail?.Equals(toUserEmail) is not true) ToUserEmail = toUserEmail;
        if (giftMessage is not null && GiftMessage?.Equals(giftMessage) is not true) GiftMessage = giftMessage;
        if (giftImage is not null && GiftImage?.Equals(giftImage) is not true) GiftImage = giftImage;
        if (IsViewed.Equals(isViewed) is not true) IsViewed = isViewed;
        Amount = amount;
        return this;
    }
}
