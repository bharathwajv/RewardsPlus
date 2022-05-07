using Microsoft.Extensions.Logging;
using RewardsPlus.Application.Common.Interfaces;
using RewardsPlus.Application.Payment;  //AskExperts

namespace RewardsPlus.Infrastructure.Multitenancy;

public class DemoGateway : IPaymentGateway
{
    private readonly ILogger<DemoGateway> _logger;
    public DemoGateway(ILogger<DemoGateway> logger) => _logger = logger;

    public async Task<bool> Sale(PayRequest request)
    {
        bool result = true;
        _logger.LogInformation("Pay request approved for user '{request.UserName}' with amount '{request.Amount}'.", request.UserName, request.Amount);
        return result;
    }
}