using FinancialKrisis.Domain.Entities;
using FinancialKrisis.Domain.Enums;
using FinancialKrisis.Tests.Scenarios;
using FinancialKrisis.Tests.Scenarios.Assertions;

namespace FinancialKrisis.Tests.ServiceTests.Payees;

public class CreatePayeeServiceTests
{
    [Fact]
    public void ValidInput_ShouldCreateSuccessfully()
    {
        new TestContext()
            .Payee()
            .Create()
            .AsCurrentPayee()
            .ShouldCreateSuccessfully();
    }

    [Fact]
    public void InvalidName_ShouldFailWithDomainRuleException()
    {
        new TestContext()
            .Payee()
            .CreatingWith(CreateInput => CreateInput.Name = string.Empty)
            .Create()
            .ShouldFailWithDomainRuleException(DomainRuleErrorCode.RequiredField, typeof(Payee), Payee.Fields.Name);
    }
}
