using FinancialKrisis.Tests.TestInfrastructure;
using Microsoft.Extensions.DependencyInjection;

namespace FinancialKrisis.Tests.Scenarios;

public abstract class BaseScenarioBuilder<TScenario>
{
    protected readonly IServiceScope Scope;
    protected readonly ServiceProvider Provider;

    protected BaseScenarioBuilder()
    {
        Provider = TestServiceProviderFactory.Create();
        Scope = Provider.CreateScope();
    }

    protected TService GetService<TService>() 
        where TService : notnull
    {
        return Scope.ServiceProvider.GetRequiredService<TService>();
    }

    protected abstract Task<TScenario> BuildInternalAsync();

    public Task<TScenario> BuildAsync()
    {
        return BuildInternalAsync();
    }
}
