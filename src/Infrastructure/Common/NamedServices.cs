using Microsoft.Extensions.DependencyInjection;
using RewardsPlus.Domain.CashDomain;//AskExperts
using RewardsPlus.Infrastructure.Multitenancy;
using static RewardsPlus.Infrastructure.Common.Resolver.Resolvers;

namespace RewardsPlus.Infrastructure.Common;

internal static class NamedServices
{
    internal static IServiceCollection AddNamedServices(this IServiceCollection services)
    {
        services.AddTransient<StripeGateway>();
        services.AddTransient<DemoGateway>();

        return services.AddTransient<PaymentGatewayResolver>(serviceProvider => key =>
        {
            switch (key)
            {
                case BuyMode.Demo:
                    return serviceProvider?.GetService<DemoGateway>();
                case BuyMode.Stripe:
                    return serviceProvider?.GetService<StripeGateway>();
                default:
                    throw new NotSupportedException($"PaymentGatewayResolver, key: {key}");
            }
        });
    }
}