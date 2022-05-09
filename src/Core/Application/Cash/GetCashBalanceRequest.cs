namespace RewardsPlus.Application.Cash;

public class GetCashBalanceRequest : IRequest<double>
{
}

public class GetCashBalanceRequestHandler : IRequestHandler<GetCashBalanceRequest, double>
{
    private readonly ICashierService _cashierService;

    public GetCashBalanceRequestHandler(ICashierService cashierService) => _cashierService = cashierService;

    public async Task<double> Handle(GetCashBalanceRequest request, CancellationToken cancellationToken) =>
                                                        await _cashierService.GetBalanceAsync();

}