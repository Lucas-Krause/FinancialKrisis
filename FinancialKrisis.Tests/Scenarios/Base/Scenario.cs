namespace FinancialKrisis.Tests.Scenarios;

public abstract class Scenario<TScenario, TInput>(TestContext pContext)
    where TScenario : Scenario<TScenario, TInput>
    where TInput : new()
{
    public abstract Type EntityType { get; }
    public TestContext Context { get; } = pContext;
    public TInput Input { get; } = new TInput();
    public Exception? LastException { get; private set; }

    public TScenario With(Action<TInput> pConfigure)
    {
        pConfigure(Input);
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