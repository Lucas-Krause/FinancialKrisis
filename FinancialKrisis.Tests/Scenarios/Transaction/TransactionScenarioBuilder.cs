using FinancialKrisis.Application.DTOs;
using FinancialKrisis.Application.Services;
using FinancialKrisis.Domain.Entities;

namespace FinancialKrisis.Tests.Scenarios;

public sealed class TransactionScenarioBuilder : BaseScenarioBuilder<TransactionScenario>
{
    private bool _inactiveAccount;
    private bool _inactiveCategory;
    private bool _inactiveSubcategory;
    private bool _inactivePayee;
    private bool _withoutPayee;
    private bool _withoutCategory;
    private bool _withoutSubcategory;
    private bool _nonExistentAccount;
    private bool _subcategoryDoesNotBelongToCategory;
    private string _identifier = "T1";
    private string _description = "Test Transaction";
    private decimal _amount = 0;

    protected override async Task<TransactionScenario> BuildInternalAsync()
    {
        Guid transactionAccountId;
        Guid transactionCategoryId;

        CreateAccountService createAccount = GetService<CreateAccountService>();
        DeactivateAccountService deactivateAccount = GetService<DeactivateAccountService>();

        CreateCategoryService createCategory = GetService<CreateCategoryService>();
        DeactivateCategoryService deactivateCategory = GetService<DeactivateCategoryService>();

        CreateSubcategoryService createSubcategory = GetService<CreateSubcategoryService>();
        DeactivateSubcategoryService deactivateSubcategory = GetService<DeactivateSubcategoryService>();

        CreatePayeeService createPayee = GetService<CreatePayeeService>();
        DeactivatePayeeService deactivatePayee = GetService<DeactivatePayeeService>();

        if (_nonExistentAccount)
        {
            transactionAccountId = Guid.NewGuid();
        }
        else
        {
            Account account = await createAccount.ExecuteAsync(new CreateAccountDTO { Name = "Test Account", AccountNumber = "123", InitialBalance = 100 });

            if (_inactiveAccount)
                await deactivateAccount.ExecuteAsync(account.Id);

            transactionAccountId = account.Id;
        }

        Category category = await createCategory.ExecuteAsync(new CreateCategoryDTO { Name = "Test Category" });
        transactionCategoryId = category.Id;

        if (_subcategoryDoesNotBelongToCategory)
        {
            Category otherCategory = await createCategory.ExecuteAsync(new CreateCategoryDTO { Name = "Other Test Category" });
            transactionCategoryId = otherCategory.Id;
        }

        if (_inactiveCategory)
            await deactivateCategory.ExecuteAsync(transactionCategoryId);

        Subcategory subcategory = await createSubcategory.ExecuteAsync(new CreateSubcategoryDTO { Name = "Test Sub", CategoryId = category!.Id });

        if (_inactiveSubcategory)
            await deactivateSubcategory.ExecuteAsync(subcategory.Id);

        Payee payee = await createPayee.ExecuteAsync(new CreatePayeeDTO { Name = "Test Payee" });

        if (_inactivePayee)
            await deactivatePayee.ExecuteAsync(payee.Id);

        return new TransactionScenario(
            Scope,
            _amount,
            _identifier,
            _description,
            DateTime.Now,
            transactionAccountId,
            _withoutCategory ? null : transactionCategoryId,
            _withoutSubcategory ? null : subcategory.Id,
            _withoutPayee ? null : payee.Id);
    }

    public TransactionScenarioBuilder WithInactiveAccount()
    {
        _inactiveAccount = true;
        return this;
    }

    public TransactionScenarioBuilder WithInactiveCategory()
    {
        _inactiveCategory = true;
        return this;
    }

    public TransactionScenarioBuilder WithInactiveSubcategory()
    {
        _inactiveSubcategory = true;
        return this;
    }

    public TransactionScenarioBuilder WithInactivePayee()
    {
        _inactivePayee = true;
        return this;
    }

    public TransactionScenarioBuilder WithNonExistentAccount()
    {
        _nonExistentAccount = true;
        return this;
    }

    public TransactionScenarioBuilder WithSubcategoryNotBelongingToCategory()
    {
        _subcategoryDoesNotBelongToCategory = true;
        return this;
    }

    public TransactionScenarioBuilder WithAmount(decimal pAmount)
    {
        _amount = pAmount;
        return this;
    }

    public TransactionScenarioBuilder WithIdentifier(string pIdentifier)
    {
        _identifier = pIdentifier;
        return this;
    }

    public TransactionScenarioBuilder WithDescription(string pDescription)
    {
        _description = pDescription;
        return this;
    }

    public TransactionScenarioBuilder WithoutPayee()
    {
        _withoutPayee = true;
        return this;
    }

    public TransactionScenarioBuilder WithoutCategory()
    {
        _withoutCategory = true;
        return this;
    }

    public TransactionScenarioBuilder WithoutSubcategory()
    {
        _withoutSubcategory = true;
        return this;
    }
}
