using FinancialKrisis.Application.DTOs;
using FinancialKrisis.Domain.Entities;
using Xunit.Sdk;

namespace FinancialKrisis.Tests.Scenarios;

public abstract class Scenario<TScenario, TCreateInput, TUpdateInput, TEntity>(TestContext pContext)
    where TScenario : Scenario<TScenario, TCreateInput, TUpdateInput, TEntity>
    where TEntity : IEntity
    where TUpdateInput : IUpdateDTO
{
    public TestContext Context { get; } = pContext;
    public TCreateInput CreateInput { get; } = Activator.CreateInstance<TCreateInput>();
    public TUpdateInput UpdateInput { get; } = Activator.CreateInstance<TUpdateInput>();

    public TEntity? Entity
    {
        get
        {
            if (LastException is not null)
                throw new XunitException($"Uma exceção ocorreu e o cenário ficou inválido:{Environment.NewLine}{LastException}");

            if (field is not null)
                return field;

            if (Context.TryGetCurrent(out TEntity? current))
                return current;

            return default;
        }
        protected set;
    }

    public Exception? LastException { get; private set; }

    protected Func<TCreateInput, Task<TEntity>>? CreateFunc { get; init; }
    protected Func<TUpdateInput, Task<TEntity>>? UpdateFunc { get; init; }
    protected Func<Guid, Task>? DeactivateFunc { get; init; }

    public TScenario CreatingWith(Action<TCreateInput> pConfigure)
    {
        pConfigure(CreateInput);
        return (TScenario)this;
    }

    public TScenario UpdatingWith(Action<TUpdateInput> pConfigure)
    {
        pConfigure(UpdateInput);
        return (TScenario)this;
    }

    public TScenario Create()
    {
        ExecuteScenarioResultSync(async () => Entity = await CreateFunc!(CreateInput));
        return (TScenario)this;
    }

    public TScenario Update()
    {
        UpdateInput.Id = Entity?.Id ?? Guid.NewGuid();

        ExecuteScenarioResultSync(async () =>
        {
            Entity = await UpdateFunc!(UpdateInput);
            Context.SetCurrent(Entity);
        });

        return (TScenario)this;
    }

    public TScenario Deactivate()
    {
        ExecuteScenarioResultSync(async () => await DeactivateFunc!(Entity?.Id ?? Guid.NewGuid()));
        return (TScenario)this;
    }

    protected TScenario AsCurrent()
    {
        Context.SetCurrent(Entity);
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