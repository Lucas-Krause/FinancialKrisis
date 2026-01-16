using FinancialKrisis.Application.DTOs;
using FinancialKrisis.Application.Services;
using FinancialKrisis.Domain.Entities;
using FinancialKrisis.Tests.TestInfrastructure;
using Microsoft.Extensions.DependencyInjection;

namespace FinancialKrisis.Tests.Transactions;

public class UpdateTransactionServiceTests
{
    [Fact]
    public async Task ValidData_ShouldUpdateSuccessfully()
    {
        ServiceProvider provider = TestServiceProviderFactory.Create();
        using IServiceScope scope = provider.CreateScope();

        CreateAccountService createAccountService = scope.ServiceProvider.GetRequiredService<CreateAccountService>();
        CreateTransactionService createTransactionService = scope.ServiceProvider.GetRequiredService<CreateTransactionService>();
        UpdateTransactionService updateTransactionService = scope.ServiceProvider.GetRequiredService<UpdateTransactionService>();

        Account account = await createAccountService.ExecuteAsync(new CreateAccountDTO { Name = "Test Account", AccountNumber = "123", InitialBalance = 100 });
        Transaction transaction = await createTransactionService.ExecuteAsync(new CreateTransactionDTO
        {
            Identifier = "T1",
            Description = "Test Transaction",
            DateTime = DateTime.UtcNow,
            AccountId = account.Id,
            Amount = 50
        });

        DateTime originalDate = transaction.DateTime;

        Transaction updated = await updateTransactionService.ExecuteAsync(new UpdateTransactionDTO
        {
            Id = transaction.Id,
            Description = "Updated Description",
            DateTime = originalDate.AddDays(1),
            AccountId = account.Id,
            Amount = 99
        });

        Assert.Equal("Updated Description", updated.Description);
        Assert.Equal(originalDate.AddDays(1), updated.DateTime);
        Assert.Equal(99, updated.Amount);
    }

    [Fact]
    public async Task NonExistentTransaction_ShouldThrowException()
    {
        ServiceProvider provider = TestServiceProviderFactory.Create();
        using IServiceScope scope = provider.CreateScope();

        UpdateTransactionService updateTransactionService = scope.ServiceProvider.GetRequiredService<UpdateTransactionService>();
        var nonExistentId = Guid.NewGuid();

        await Assert.ThrowsAsync<InvalidOperationException>(async () =>
        {
            await updateTransactionService.ExecuteAsync(new UpdateTransactionDTO
            {
                Id = nonExistentId,
                Description = "Any",
                DateTime = DateTime.UtcNow,
                AccountId = Guid.NewGuid(),
                Amount = 1
            });
        });
    }
}
