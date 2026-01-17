using FinancialKrisis.Application.DTOs;
using FinancialKrisis.Application.Services;
using FinancialKrisis.Domain.Entities;
using FinancialKrisis.Tests.TestInfrastructure;
using Microsoft.Extensions.DependencyInjection;

namespace FinancialKrisis.Tests.Transactions;

public class DeleteTransactionServiceTests
{
    [Fact]
    public async Task ValidId_ShouldDeleteSuccessfully()
    {
        ServiceProvider provider = TestServiceProviderFactory.Create();
        using IServiceScope scope = provider.CreateScope();

        CreateAccountService createAccountService = scope.ServiceProvider.GetRequiredService<CreateAccountService>();
        CreateTransactionService createTransactionService = scope.ServiceProvider.GetRequiredService<CreateTransactionService>();
        DeleteTransactionService deleteTransactionService = scope.ServiceProvider.GetRequiredService<DeleteTransactionService>();
        GetTransactionByIdService getTransactionByIdService = scope.ServiceProvider.GetRequiredService<GetTransactionByIdService>();

        Account account = await createAccountService.ExecuteAsync(new CreateAccountDTO { Name = "Test Account", AccountNumber = "123", InitialBalance = 100 });
        Transaction transaction = await createTransactionService.ExecuteAsync(new CreateTransactionDTO
        {
            Identifier = "T1",
            Description = "Test Transaction",
            DateTime = DateTime.UtcNow,
            AccountId = account.Id,
            Amount = 50
        });

        await deleteTransactionService.ExecuteAsync(transaction.Id);
        Transaction? deleted = await getTransactionByIdService.ExecuteAsync(transaction.Id);
        Assert.Null(deleted);
    }

    [Fact]
    public async Task NonExistentId_ShouldNotThrow()
    {
        ServiceProvider provider = TestServiceProviderFactory.Create();
        using IServiceScope scope = provider.CreateScope();

        DeleteTransactionService deleteTransactionService = scope.ServiceProvider.GetRequiredService<DeleteTransactionService>();
        var nonExistentId = Guid.NewGuid();

        // Não deve lançar exceção
        await deleteTransactionService.ExecuteAsync(nonExistentId);
    }
}
