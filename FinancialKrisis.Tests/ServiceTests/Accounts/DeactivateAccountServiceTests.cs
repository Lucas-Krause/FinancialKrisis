using FinancialKrisis.Application.DTOs;
using FinancialKrisis.Application.Services;
using FinancialKrisis.Domain.Entities;
using FinancialKrisis.Tests.TestInfrastructure;
using Microsoft.Extensions.DependencyInjection;

namespace FinancialKrisis.Tests.ServiceTests.Accounts;

public class DeactivateAccountServiceTests
{
    [Fact]
    public async Task NormalSituation_ShouldDeactivateAccountSuccessfully()
    {
        ServiceProvider provider = TestServiceProviderFactory.Create();
        using IServiceScope scope = provider.CreateScope();

        CreateAccountService createAccountService = scope.ServiceProvider.GetRequiredService<CreateAccountService>();
        GetAccountByIdService getAccountByIdService = scope.ServiceProvider.GetRequiredService<GetAccountByIdService>();
        DeactivateAccountService deactivateAccountService = scope.ServiceProvider.GetRequiredService<DeactivateAccountService>();

        Account createdAccount = await createAccountService.ExecuteAsync(new CreateAccountDTO { Name = "Test Account", InitialBalance = 100, AccountNumber = "123" });

        await deactivateAccountService.ExecuteAsync(createdAccount.Id);

        Account? accountAfterDeactivation = await getAccountByIdService.ExecuteAsync(createdAccount.Id);
        Assert.NotNull(accountAfterDeactivation);
        Assert.False(accountAfterDeactivation.IsActive);
    }
}
