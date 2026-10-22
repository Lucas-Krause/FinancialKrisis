using FinancialKrisis.Domain.Entities;
using FinancialKrisis.Domain.Enums;
using FinancialKrisis.Tests.Scenarios;
using FinancialKrisis.Tests.Scenarios.Assertions;

namespace FinancialKrisis.Tests.ServiceTests.Payees;

public class ReactivatePayeeServiceTests
{
    [Fact]
    public void ValidInput_ShouldReactivateSuccessfully()
    {
        new TestContext()
            .Payee()
            .Create()
            .AsCurrentPayee()
            .Deactivate()
            .Reactivate()
            .ShouldReactivateSuccessfully();
    }

    [Fact]
    public void ActivePayee_ShouldFailWithDomainRuleException()
    {
        new TestContext()
            .Payee()
            .Create()
            .AsCurrentPayee()
            .Reactivate()
            .ShouldFailWithDomainRuleException(DomainRuleErrorCode.EntityAlreadyActive, typeof(Payee));
    }

    [Fact]
    public void NonExistentPayee_ShouldFailWithDomainRuleException()
    {
        new TestContext()
            .Payee()
            .Reactivate()
            .ShouldFailWithDomainRuleException(DomainRuleErrorCode.EntityNotFound, typeof(Payee));
    }
}
