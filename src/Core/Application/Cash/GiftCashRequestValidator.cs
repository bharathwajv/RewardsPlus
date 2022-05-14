using RewardsPlus.Application.Cash;

namespace RewardsPlus.Application.Order;

public class GiftCashRequestValidator : CustomValidator<GiftCashRequest>
{
    public GiftCashRequestValidator(ICurrentUser _currentUser, IStringLocalizer<GiftCashRequestValidator> T)
    {
        RuleFor(p => p.ToUserEmail)
            .Must((p, ct) => _currentUser.GetUserEmail() != p.ToUserEmail)
             .WithMessage((_, name) => T["You can't gift to yourself ", _.ToUserEmail]);
    }

}