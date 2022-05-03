﻿using System.Globalization;
using RewardsPlus.Infrastructure.Middleware;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;

namespace RewardsPlus.Infrastructure.Localization;

internal static class Startup
{
    internal static IServiceCollection AddLocalization(this IServiceCollection services, IConfiguration config)
    {
        services.AddLocalization();

        services.AddSingleton<IStringLocalizerFactory, JsonStringLocalizerFactory>();

        var middlewareSettings = config.GetSection(nameof(MiddlewareSettings)).Get<MiddlewareSettings>();
        if (middlewareSettings.EnableLocalization)
        {
            services.AddSingleton<LocalizationMiddleware>();
        }

        return services;
    }

    internal static IApplicationBuilder UseLocalization(this IApplicationBuilder app, IConfiguration config)
    {
        app.UseRequestLocalization(new RequestLocalizationOptions
        {
            DefaultRequestCulture = new RequestCulture(new CultureInfo("en-US"))
        });

        var middlewareSettings = config.GetSection(nameof(MiddlewareSettings)).Get<MiddlewareSettings>();
        if (middlewareSettings.EnableLocalization)
        {
            app.UseMiddleware<LocalizationMiddleware>();
        }

        return app;
    }
}