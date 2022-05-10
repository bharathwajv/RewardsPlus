using RewardsPlus.Application.Cash;
using RewardsPlus.Application.Catalog.Products;

namespace RewardsPlus.Application.Order;

public class BuyProductRequestValidator : CustomValidator<BuyProductRequest>
{
    public BuyProductRequestValidator(ICashierService cashierService, ISender mediator, IStringLocalizer<BuyProductRequestValidator> localizer)
    {
        RuleFor(p => p.ProductId).NotEqual(new Guid()).WithMessage(localizer["Product ID Required"])
            .MustAsync(async (id, ct) => await cashierService.GetBalanceAsync() > (await mediator.Send(new GetProductRequest(id), ct)).Rate)
            .WithMessage(localizer["Insufficient Balance"]);

        RuleFor(p => p.ProductId).MustAsync(async (id, ct) => await mediator.Send(new GetProductRequest(id), ct) != null)
            .WithMessage(localizer["Product Not Found"]);
    }
}