using FinancialKrisis.Application.Services;
using FinancialKrisis.Domain.Entities;
using FinancialKrisis.Tests.Scenarios.Assertions;
using FinancialKrisis.Tests.Scenarios.Entities;
using FinancialKrisis.Tests.Scenarios.Interfaces;
using FinancialKrisis.Tests.TestInfrastructure;
using Microsoft.Extensions.DependencyInjection;

namespace FinancialKrisis.Tests.Scenarios;

public sealed class TestContext : IDisposable
{
    private readonly Dictionary<Type, object> _entities = [];
    public IServiceScope Scope { get; }
    public ServiceProvider Provider { get; }

    public CreateAccountService CreateAccountService { get; }
    public UpdateAccountService UpdateAccountService { get; }
    public DeactivateAccountService DeactivateAccountService { get; }

    public CreateCategoryService CreateCategoryService { get; }
    public UpdateCategoryService UpdateCategoryService { get; }
    public DeactivateCategoryService DeactivateCategoryService { get; }

    public CreateSubcategoryService CreateSubcategoryService { get; }
    public UpdateSubcategoryService UpdateSubcategoryService { get; }
    public DeactivateSubcategoryService DeactivateSubcategoryService { get; }

    public CreatePayeeService CreatePayeeService { get; }
    public UpdatePayeeService UpdatePayeeService { get; }
    public DeactivatePayeeService DeactivatePayeeService { get; }

    public CreateTransactionService CreateTransactionService { get; }
    public UpdateTransactionService UpdateTransactionService { get; }
    public DeleteTransactionService DeleteTransactionService { get; }

    public CreatePlannedTransactionService CreatePlannedTransactionService { get; }
    public UpdatePlannedTransactionService UpdatePlannedTransactionService { get; }
    public DeletePlannedTransactionService DeletePlannedTransactionService { get; }

    public TestContext()
    {
        Provider = TestServiceProviderFactory.Create();
        Scope = Provider.CreateScope();

        CreateAccountService = Scope.ServiceProvider.GetRequiredService<CreateAccountService>();
        UpdateAccountService = Scope.ServiceProvider.GetRequiredService<UpdateAccountService>();
        DeactivateAccountService = Scope.ServiceProvider.GetRequiredService<DeactivateAccountService>();

        CreateCategoryService = Scope.ServiceProvider.GetRequiredService<CreateCategoryService>();
        UpdateCategoryService = Scope.ServiceProvider.GetRequiredService<UpdateCategoryService>();
        DeactivateCategoryService = Scope.ServiceProvider.GetRequiredService<DeactivateCategoryService>();

        CreateSubcategoryService = Scope.ServiceProvider.GetRequiredService<CreateSubcategoryService>();
        UpdateSubcategoryService = Scope.ServiceProvider.GetRequiredService<UpdateSubcategoryService>();
        DeactivateSubcategoryService = Scope.ServiceProvider.GetRequiredService<DeactivateSubcategoryService>();

        CreatePayeeService = Scope.ServiceProvider.GetRequiredService<CreatePayeeService>();
        UpdatePayeeService = Scope.ServiceProvider.GetRequiredService<UpdatePayeeService>();
        DeactivatePayeeService = Scope.ServiceProvider.GetRequiredService<DeactivatePayeeService>();

        CreateTransactionService = Scope.ServiceProvider.GetRequiredService<CreateTransactionService>();
        UpdateTransactionService = Scope.ServiceProvider.GetRequiredService<UpdateTransactionService>();
        DeleteTransactionService = Scope.ServiceProvider.GetRequiredService<DeleteTransactionService>();

        CreatePlannedTransactionService = Scope.ServiceProvider.GetRequiredService<CreatePlannedTransactionService>();
        UpdatePlannedTransactionService = Scope.ServiceProvider.GetRequiredService<UpdatePlannedTransactionService>();
        DeletePlannedTransactionService = Scope.ServiceProvider.GetRequiredService<DeletePlannedTransactionService>();
    }

    public void SetCurrent<TEntity>(TEntity pEntity)
    {
        _entities[typeof(TEntity)] = pEntity!;
    }

    public bool TryGetCurrent<TEntity>(out TEntity? pEntity)
    {
        if (_entities.TryGetValue(typeof(TEntity), out object? value))
        {
            pEntity = (TEntity)value;
            return true;
        }

        pEntity = default;
        return false;
    }

    public TEntity GetCurrentOrThrow<TEntity>()
    {
        if (!TryGetCurrent(out TEntity? entity))
            throw new InvalidOperationException(
                $"Nenhuma entidade do tipo '{typeof(TEntity).Name}' foi encontrada no TestContext. " +
                $"Esqueceu de chamar Create() ou AsCurrent{typeof(TEntity).Name}() ou WithCurrent{typeof(TEntity).Name}()?");

        return entity!;
    }

    public AccountScenario Account()
    {
        return new AccountScenario(this);
    }

    public PayeeScenario Payee()
    {
        return new PayeeScenario(this);
    }

    public CategoryScenario Category()
    {
        return new CategoryScenario(this);
    }

    public SubcategoryScenario Subcategory()
    {
        return new SubcategoryScenario(this);
    }

    public TransactionScenario Transaction()
    {
        return new TransactionScenario(this);
    }

    public PlannedTransactionScenario PlannedTransaction()
    {
        return new PlannedTransactionScenario(this);
    }

    public IFinancialMovementScenario FinancialMovement(Type pMovementType)
    {
        if (pMovementType == typeof(Transaction))
            return Transaction();
        else if (pMovementType == typeof(PlannedTransaction))
            return PlannedTransaction();
        else 
            throw new ArgumentException($"Movimento financeiro não implementado: {pMovementType.Name}");
    }

    public void Dispose()
    {
        Scope.Dispose();
        Provider.Dispose();
    }
}
