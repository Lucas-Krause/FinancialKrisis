using FinancialKrisis.Application.Services;
using FinancialKrisis.Tests.Scenarios.Entities;
using FinancialKrisis.Tests.TestInfrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.DependencyInjection;

namespace FinancialKrisis.Tests.Scenarios;

public sealed class TestContext : IDisposable
{
    private readonly Dictionary<Type, object> _entities = [];
    public IServiceScope Scope { get; }
    public ServiceProvider Provider { get; }

    public CreateAccountService CreateAccountService { get; }
    public DeactivateAccountService DeactivateAccountService { get; }

    public CreateCategoryService CreateCategoryService { get; }
    public DeactivateCategoryService DeactivateCategoryService { get; }

    public CreateSubcategoryService CreateSubcategoryService { get; }
    public DeactivateSubcategoryService DeactivateSubcategoryService { get; }

    public CreatePayeeService CreatePayeeService { get; }
    public DeactivatePayeeService DeactivatePayeeService { get; }

    public CreateTransactionService CreateTransactionService { get; }
    public DeleteTransactionService DeleteTransactionService { get; }

    public TestContext()
    {
        Provider = TestServiceProviderFactory.Create();
        Scope = Provider.CreateScope();

        CreateAccountService = Scope.ServiceProvider.GetRequiredService<CreateAccountService>();
        DeactivateAccountService = Scope.ServiceProvider.GetRequiredService<DeactivateAccountService>();

        CreateCategoryService = Scope.ServiceProvider.GetRequiredService<CreateCategoryService>();
        DeactivateCategoryService = Scope.ServiceProvider.GetRequiredService<DeactivateCategoryService>();

        CreateSubcategoryService = Scope.ServiceProvider.GetRequiredService<CreateSubcategoryService>();
        DeactivateSubcategoryService = Scope.ServiceProvider.GetRequiredService<DeactivateSubcategoryService>();

        CreatePayeeService = Scope.ServiceProvider.GetRequiredService<CreatePayeeService>();
        DeactivatePayeeService = Scope.ServiceProvider.GetRequiredService<DeactivatePayeeService>();

        CreateTransactionService = Scope.ServiceProvider.GetRequiredService<CreateTransactionService>();
        DeleteTransactionService = Scope.ServiceProvider.GetRequiredService<DeleteTransactionService>();
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

    public bool TryGetCurrent(Type pEntityType, out object? pEntity)
    {
        return _entities.TryGetValue(pEntityType, out pEntity);
    }

    public TEntity GetCurrentOrThrow<TEntity>()
    {
        if (!TryGetCurrent(out TEntity? entity))
            ThrowEntityNotFoundOnTestContext(typeof(TEntity));

        return entity!;
    }

    public object GetCurrentOrThrow(Type pEntityType)
    {
        if (!TryGetCurrent(pEntityType, out object? entity))
            ThrowEntityNotFoundOnTestContext(pEntityType);

        return entity!;
    }

    private static void ThrowEntityNotFoundOnTestContext(Type pEntityType)
    {
        throw new InvalidOperationException(
            $"Nenhuma entidade do tipo '{pEntityType.Name}' foi encontrada no TestContext. " +
            $"Esqueceu de chamar Create() ou AsCurrent{pEntityType.Name}() ou WithCurrent{pEntityType.Name}()?");
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

    public void Dispose()
    {
        Scope.Dispose();
        Provider.Dispose();
    }
}
