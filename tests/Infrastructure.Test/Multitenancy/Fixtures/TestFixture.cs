using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RewardsPlus.Application.Common.Persistence;
using RewardsPlus.Infrastructure.Persistence.ConnectionString;
using Xunit.Microsoft.DependencyInjection.Abstracts;

namespace Infrastructure.Test.Multitenancy.Fixtures;

public class TestFixture : TestBedFixture
{
    protected override void AddServices(IServiceCollection services, IConfiguration? configuration)
        => services
            .AddTransient<IConnectionStringSecurer, ConnectionStringSecurer>();

    protected override ValueTask DisposeAsyncCore()
        => new();

    protected override IEnumerable<string> GetConfigurationFiles()
    {
        yield return "appsettings.json";
    }
}