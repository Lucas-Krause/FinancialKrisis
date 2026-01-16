using FinancialKrisis.Application.DTOs;
using FinancialKrisis.Application.Services;
using FinancialKrisis.Domain.Entities;
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

        CreateAccountService pCreateAccountService = scope.ServiceProvider.GetRequiredService<CreateAccountService>();
        CreateTransactionService pCreateTransactionService = scope.ServiceProvider.GetRequiredService<CreateTransactionService>();

        Account account = await pCreateAccountService.ExecuteAsync(new CreateAccountDTO { Name = "Test Account", AccountNumber = "123", InitialBalance = 100 });
        DateTime now = DateTime.UtcNow;
        Transaction transaction = await pCreateTransactionService.ExecuteAsync(new CreateTransactionDTO
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
        Assert.Null(transaction.Payee);
        Assert.Null(transaction.Category);
        Assert.Null(transaction.SubCategory);
    }

    [Fact]
    public async Task NonExistentAccount_ShouldThrowException()
    {
        ServiceProvider provider = TestServiceProviderFactory.Create();
        using IServiceScope scope = provider.CreateScope();

        CreateTransactionService pCreateTransactionService = scope.ServiceProvider.GetRequiredService<CreateTransactionService>();
        var nonExistentAccountId = Guid.NewGuid();

        await Assert.ThrowsAsync<InvalidOperationException>(async () =>
        {
            await pCreateTransactionService.ExecuteAsync(new CreateTransactionDTO
            {
                Identifier = "T2",
                Description = "Should Fail",
                DateTime = DateTime.UtcNow,
                AccountId = nonExistentAccountId,
                Amount = 10
            });
        });
    }
}
