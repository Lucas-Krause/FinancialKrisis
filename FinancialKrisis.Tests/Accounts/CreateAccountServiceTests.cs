using FinancialKrisis.Application.DTOs;
using FinancialKrisis.Application.Services;
using FinancialKrisis.Domain.Entities;
using FinancialKrisis.Tests.TestInfrastructure;
using Microsoft.Extensions.DependencyInjection;

namespace FinancialKrisis.Tests.Accounts;

public class CreateAccountServiceTests
{
    [Fact]
    public async Task NormalSituation_ShouldCreateSuccessfully()
    {
        ServiceProvider provider = TestServiceProviderFactory.Create();
        using IServiceScope scope = provider.CreateScope();

        CreateAccountService createAccountService = scope.ServiceProvider.GetRequiredService<CreateAccountService>();

        Account createdAccount = await createAccountService.ExecuteAsync(new CreateAccountDTO { Name = "Test Account", InitialBalance = 500 });

        Assert.Equal("Test Account", createdAccount.Name);
        Assert.Equal(500, createdAccount.InitialBalance);
        Assert.True(createdAccount.IsActive);
    }
}
