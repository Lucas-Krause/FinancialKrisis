using FinancialKrisis.Application.DTOs;
using FinancialKrisis.Application.Services;
using FinancialKrisis.Domain.Entities;
using FinancialKrisis.Tests.TestInfrastructure;
using Microsoft.Extensions.DependencyInjection;

namespace FinancialKrisis.Tests.ServiceTests.Payees;

public class UpdatePayeeServiceTests
{
    [Fact]
    public async Task NormalSituation_ShouldUpdateNameSuccessfully()
    {
        ServiceProvider provider = TestServiceProviderFactory.Create();
        using IServiceScope scope = provider.CreateScope();

        CreatePayeeService createPayeeService = scope.ServiceProvider.GetRequiredService<CreatePayeeService>();
        UpdatePayeeService updatePayeeService = scope.ServiceProvider.GetRequiredService<UpdatePayeeService>();

        Payee createdPayee = await createPayeeService.ExecuteAsync(new CreatePayeeDTO { Name = "Old Name" });
        Payee updatedPayee = await updatePayeeService.ExecuteAsync(new UpdatePayeeDTO { Id = createdPayee.Id, Name = "New Name" });

        Assert.Equal("New Name", updatedPayee.Name);
    }
}
