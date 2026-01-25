using FinancialKrisis.Tests.Scenarios;
using FinancialKrisis.Tests.Scenarios.Assertions;

namespace FinancialKrisis.Tests.ServiceTests.Payees;

public class DeactivatePayeeServiceTests
{
    [Fact]
    public void ValidInput_ShouldDeactivateSuccessfully()
    {
        new TestContext()
            .Payee()
            .Create()
            .AsCurrentPayee()
            .Deactivate()
            .ShouldDeactivateSuccessfully();
    }
}
