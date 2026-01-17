using FinancialKrisis.Application.DTOs;
using FinancialKrisis.Application.Enums;
using FinancialKrisis.Application.Exceptions;
using FinancialKrisis.Application.Services;
using FinancialKrisis.Domain.Entities;
using FinancialKrisis.Domain.Enums;
using FinancialKrisis.Domain.Exceptions;
using FinancialKrisis.Tests.TestInfrastructure;
using Microsoft.Extensions.DependencyInjection;

namespace FinancialKrisis.Tests.Transactions;

public class CreateTransactionServiceTests
{
    [Fact]
    public async Task ValidData_ShouldCreateSuccessfully()
    {
        ServiceProvider provider = TestServiceProviderFactory.Create();
        using IServiceScope scope = provider.CreateScope();

        CreateAccountService createAccountService = scope.ServiceProvider.GetRequiredService<CreateAccountService>();
        CreateTransactionService createTransactionService = scope.ServiceProvider.GetRequiredService<CreateTransactionService>();

        Account account = await createAccountService.ExecuteAsync(new CreateAccountDTO { Name = "Test Account", AccountNumber = "123", InitialBalance = 100 });
        DateTime now = DateTime.UtcNow;
        Transaction transaction = await createTransactionService.ExecuteAsync(new CreateTransactionDTO
        {
            Identifier = "T1",
            Description = "Test Transaction",
            DateTime = now,
            AccountId = account.Id,
            Amount = 50
        });

        Assert.Equal("T1", transaction.Identifier);
        Assert.Equal("Test Transaction", transaction.Description);
        Assert.Equal(now, transaction.DateTime);
        Assert.Equal(account.Id, transaction.AccountId);
        Assert.Equal(50, transaction.Amount);
    }

    [Fact]
    public async Task NonExistentAccount_ShouldThrowException()
    {
        ServiceProvider provider = TestServiceProviderFactory.Create();
        using IServiceScope scope = provider.CreateScope();

        CreateTransactionService createTransactionService = scope.ServiceProvider.GetRequiredService<CreateTransactionService>();
        var nonExistentAccountId = Guid.NewGuid();

        DomainRuleException ex = await Assert.ThrowsAsync<DomainRuleException>(async () =>
        {
            await createTransactionService.ExecuteAsync(new CreateTransactionDTO
            {
                Identifier = "T2",
                Description = "Should Fail",
                DateTime = DateTime.UtcNow,
                AccountId = nonExistentAccountId,
                Amount = 10
            });
        });

        Assert.Equal(DomainRuleErrorCode.EntityNotFound, ex.ErrorCode);
    }

    [Fact]
    public async Task SubcategoryDoesntBelongToCategory_ShouldThrowException()
    {
        ServiceProvider provider = TestServiceProviderFactory.Create();
        using IServiceScope scope = provider.CreateScope();

        CreateAccountService createAccountService = scope.ServiceProvider.GetRequiredService<CreateAccountService>();
        CreateTransactionService createTransactionService = scope.ServiceProvider.GetRequiredService<CreateTransactionService>();
        CreateCategoryService createCategoryService = scope.ServiceProvider.GetRequiredService<CreateCategoryService>();
        CreateSubcategoryService createSubcategoryService = scope.ServiceProvider.GetRequiredService<CreateSubcategoryService>();

        Account account = await createAccountService.ExecuteAsync(new CreateAccountDTO { Name = "Test Account", AccountNumber = "123", InitialBalance = 100 });
        Category category = await createCategoryService.ExecuteAsync(new CreateCategoryDTO { Name = "Subcategory Category" });
        Subcategory subcategory = await createSubcategoryService.ExecuteAsync(new CreateSubcategoryDTO { Name = "Transaction Subcategory", CategoryId = category.Id });
        Category transactionCategory = await createCategoryService.ExecuteAsync(new CreateCategoryDTO { Name = "Transaction Category" });

        ApplicationRuleException ex = await Assert.ThrowsAsync<ApplicationRuleException>(async () =>
        {
            await createTransactionService.ExecuteAsync(new CreateTransactionDTO
            {
                Identifier = "T2",
                Description = "Should Fail",
                DateTime = DateTime.UtcNow,
                AccountId = account.Id,
                CategoryId = transactionCategory.Id,
                SubcategoryId = subcategory.Id,
                Amount = 10
            });
        });

        Assert.Equal(ApplicationRuleErrorCode.SubcategoryDoesNotBelongToCategory, ex.ErrorCode);
    }
}
