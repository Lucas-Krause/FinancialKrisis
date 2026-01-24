using FinancialKrisis.Domain.Common;

namespace FinancialKrisis.Tests.Scenarios;

public abstract class Scenario<TScenario, TInput, TEntity>(TestContext pContext)
    where TScenario : Scenario<TScenario, TInput, TEntity>
    where TEntity : IEntity
{
    public TestContext Context { get; } = pContext;
    public TInput Input { get; } = Activator.CreateInstance<TInput>();

    public TEntity? CreatedEntity { get; protected set; }
    public Exception? LastException { get; private set; }

    protected Func<TInput, Task<TEntity>>? CreateFunc { get; init; }
    protected Func<Guid, Task>? DeactivateFunc { get; init; }

    public TScenario With(Action<TInput> pConfigure)
    {
        pConfigure(Input);
        return (TScenario)this;
    }

    public TScenario Create()
    {
        ExecuteScenarioResultSync(async () => CreatedEntity = await CreateFunc!(Input));
        return (TScenario)this;
    }

    public TScenario Deactivate()
    {
        ExecuteScenarioResultSync(async () => await DeactivateFunc!(Context.GetCurrentOrThrow<TEntity>().Id));
        return (TScenario)this;
    }

    protected TScenario AsCurrent()
    {
        Context.SetCurrent(CreatedEntity);
        return (TScenario)this;
    }

    protected void ExecuteScenarioResultSync(Func<Task> pAction)
    {
        ExecuteScenarioResultAsync(pAction)
            .GetAwaiter()
            .GetResult();
    }

    protected async Task ExecuteScenarioResultAsync(Func<Task> pAction)
    {
        try
        {
            await pAction();
            LastException = null;
        }
        catch (Exception ex)
        {
            LastException = ex;
        }
    }
}