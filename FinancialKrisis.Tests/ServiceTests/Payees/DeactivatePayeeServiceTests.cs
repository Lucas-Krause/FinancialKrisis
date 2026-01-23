using FinancialKrisis.Application.DTOs;
using FinancialKrisis.Application.Services;
using FinancialKrisis.Domain.Entities;
using FinancialKrisis.Tests.TestInfrastructure;
using Microsoft.Extensions.DependencyInjection;

namespace FinancialKrisis.Tests.ServiceTests.Payees;

public class DeactivatePayeeServiceTests
{
    [Fact]
    public async Task NormalSituation_ShouldDeactivatePayeeSuccessfully()
    {
        ServiceProvider provider = TestServiceProviderFactory.Create();
        using IServiceScope scope = provider.CreateScope();

        CreatePayeeService createPayeeService = scope.ServiceProvider.GetRequiredService<CreatePayeeService>();
        GetPayeeByIdService getPayeeByIdService = scope.ServiceProvider.GetRequiredService<GetPayeeByIdService>();
        DeactivatePayeeService deactivatePayeeService = scope.ServiceProvider.GetRequiredService<DeactivatePayeeService>();

        Payee createdPayee = await createPayeeService.ExecuteAsync(new CreatePayeeDTO { Name = "Test Payee" });

        await deactivatePayeeService.ExecuteAsync(createdPayee.Id);

        Payee? payeeAfterDeactivation = await getPayeeByIdService.ExecuteAsync(createdPayee.Id);
        Assert.NotNull(payeeAfterDeactivation);
        Assert.False(payeeAfterDeactivation.IsActive);
    }
}
