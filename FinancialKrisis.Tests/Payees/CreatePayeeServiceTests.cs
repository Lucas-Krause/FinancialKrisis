using FinancialKrisis.Application.DTOs;
using FinancialKrisis.Application.Services;
using FinancialKrisis.Domain.Entities;
using FinancialKrisis.Tests.TestInfrastructure;
using Microsoft.Extensions.DependencyInjection;

namespace FinancialKrisis.Tests.Payees;

public class CreatePayeeServiceTests
{
    [Fact]
    public async Task NormalSituation_ShouldCreateSuccessfully()
    {
        ServiceProvider provider = TestServiceProviderFactory.Create();
        using IServiceScope scope = provider.CreateScope();

        CreatePayeeService createPayeeService = scope.ServiceProvider.GetRequiredService<CreatePayeeService>();

        Payee createdPayee = await createPayeeService.ExecuteAsync(new CreatePayeeDTO { Name = "Test Payee" });

        Assert.Equal("Test Payee", createdPayee.Name);
        Assert.True(createdPayee.IsActive);
    }
}
