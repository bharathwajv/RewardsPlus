    namespace RewardsPlus.Application.Cash;

public class GetCashBalanceRequest : IRequest<decimal>
{
}

public class GetCashBalanceRequestHandler : IRequestHandler<GetCashBalanceRequest, decimal>
{
    private readonly ICashierService _cashierService;

    public GetCashBalanceRequestHandler(ICashierService cashierService) => _cashierService = cashierService;

    public async Task<decimal> Handle(GetCashBalanceRequest request, CancellationToken cancellationToken) =>
                                                        await _cashierService.GetBalanceAsync();

}