using Microsoft.Extensions.DependencyInjection;

namespace FinancialKrisis.Tests.Scenarios;

public abstract class BaseScenario(IServiceScope pScope)
{
    protected IServiceScope Scope { get; } = pScope;
}
