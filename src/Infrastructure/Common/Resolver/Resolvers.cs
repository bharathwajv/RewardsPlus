﻿using RewardsPlus.Application.Payment;  // AskExperts
using RewardsPlus.Domain.Cash;
using RewardsPlus.Domain.Catalog; // AskExperts

namespace RewardsPlus.Infrastructure.Common.Resolver;
public class Resolvers
{
    public delegate IPaymentGateway PaymentGatewayResolver(BuyMode paymentGatewayType);
}
