namespace RewardsPlus.Application.Token;

public class GetAllCashRequest : IRequest<List<CashDto>>
{
}

public class GetAllCashRequestHandler : IRequestHandler<GetAllCashRequest, List<CashDto>>
{
    private readonly ICashierService _cashierService;

    public GetAllCashRequestHandler(ICashierService cashierService) => _cashierService = cashierService;

    public async Task<List<CashDto>> Handle(GetAllCashRequest request, CancellationToken cancellationToken) =>
                                                        await _cashierService.GetAllAsync();

}