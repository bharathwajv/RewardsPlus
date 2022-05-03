﻿using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace RewardsPlus.Infrastructure.Mailing;

internal static class Startup
{
    internal static IServiceCollection AddMailing(this IServiceCollection services, IConfiguration config) =>
        services.Configure<MailSettings>(config.GetSection(nameof(MailSettings)));
}