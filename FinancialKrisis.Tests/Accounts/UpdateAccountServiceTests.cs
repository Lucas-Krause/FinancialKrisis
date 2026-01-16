using FinancialKrisis.Application.DTOs;
using FinancialKrisis.Application.Services;
using FinancialKrisis.Domain.Entities;
using FinancialKrisis.Tests.TestInfrastructure;
using Microsoft.Extensions.DependencyInjection;

namespace FinancialKrisis.Tests.Accounts;

public class UpdateAccountServiceTests
{
    [Fact]
    public async Task NormalSituation_ShouldUpdateNameSuccessfully()
    {
        ServiceProvider provider = TestServiceProviderFactory.Create();
        using IServiceScope scope = provider.CreateScope();

        CreateAccountService createAccountService = scope.ServiceProvider.GetRequiredService<CreateAccountService>();
        UpdateAccountService updateAccountService = scope.ServiceProvider.GetRequiredService<UpdateAccountService>();

        Account createdAccount = await createAccountService.ExecuteAsync(new CreateAccountDTO { Name = "Old Name", AccountNumber = "111", InitialBalance = 100 });
        Account updatedAccount = await updateAccountService.ExecuteAsync(new UpdateAccountDTO { Id = createdAccount.Id, Name = "New Name", AccountNumber = "222" });

        Assert.Equal("New Name", updatedAccount.Name);
        Assert.Equal("222", updatedAccount.AccountNumber);
    }
}
