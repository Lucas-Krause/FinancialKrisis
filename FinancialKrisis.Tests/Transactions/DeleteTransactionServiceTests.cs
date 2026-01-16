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

        CreateAccountService pCreateAccountService = scope.ServiceProvider.GetRequiredService<CreateAccountService>();
        CreateTransactionService pCreateTransactionService = scope.ServiceProvider.GetRequiredService<CreateTransactionService>();
        DeleteTransactionService pDeleteTransactionService = scope.ServiceProvider.GetRequiredService<DeleteTransactionService>();
        GetTransactionByIdService pGetTransactionByIdService = scope.ServiceProvider.GetRequiredService<GetTransactionByIdService>();

        Account account = await pCreateAccountService.ExecuteAsync(new CreateAccountDTO { Name = "Test Account", AccountNumber = "123", InitialBalance = 100 });
        Transaction transaction = await pCreateTransactionService.ExecuteAsync(new CreateTransactionDTO
        {
            Identifier = "T1",
            Description = "Test Transaction",
            DateTime = DateTime.UtcNow,
            AccountId = account.Id,
            Amount = 50
        });

        await pDeleteTransactionService.ExecuteAsync(transaction.Id);
        Transaction? deleted = await pGetTransactionByIdService.ExecuteAsync(transaction.Id);
        Assert.Null(deleted);
    }

    [Fact]
    public async Task NonExistentId_ShouldNotThrow()
    {
        ServiceProvider provider = TestServiceProviderFactory.Create();
        using IServiceScope scope = provider.CreateScope();

        DeleteTransactionService pDeleteTransactionService = scope.ServiceProvider.GetRequiredService<DeleteTransactionService>();
        var nonExistentId = Guid.NewGuid();

        // Não deve lançar exceção
        await pDeleteTransactionService.ExecuteAsync(nonExistentId);
    }
}
