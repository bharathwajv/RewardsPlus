using RewardsPlus.Application.Cash;
using RewardsPlus.Application.Identity.Users;

namespace RewardsPlus.Application.Order;

public class GiftCashRequestValidator : CustomValidator<GiftCashRequest>
{
    public GiftCashRequestValidator(ICurrentUser _currentUser, IUserService userService, IStringLocalizer<GiftCashRequestValidator> T)
    {
        RuleFor(p => p.ToUserEmail)
            .Must((p, ct) => _currentUser.GetUserEmail() != p.ToUserEmail)
             .WithMessage((_, name) => T["You can't gift to yourself ", _.ToUserEmail]);

        RuleFor(p => p.ToUserEmail).MustAsync(async (email, _) => await userService.ExistsWithEmailAsync(email))
                .WithMessage((_, email) => T["Enter a valid user.", email]);
    }

}