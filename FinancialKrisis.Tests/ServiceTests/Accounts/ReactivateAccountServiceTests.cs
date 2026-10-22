using FinancialKrisis.Domain.Entities;
using FinancialKrisis.Domain.Enums;
using FinancialKrisis.Tests.Scenarios;
using FinancialKrisis.Tests.Scenarios.Assertions;

namespace FinancialKrisis.Tests.ServiceTests.Accounts;

public class ReactivateAccountServiceTests
{
    [Fact]
    public void ValidInput_ShouldReactivateSuccessfully()
    {
        new TestContext()
            .Account()
            .Create()
            .AsCurrentAccount()
            .Deactivate()
            .Reactivate()
            .ShouldReactivateSuccessfully();
    }

    [Fact]
    public void ActiveAccount_ShouldFailWithDomainRuleException()
    {
        new TestContext()
            .Account()
            .Create()
            .AsCurrentAccount()
            .Reactivate()
            .ShouldFailWithDomainRuleException(DomainRuleErrorCode.EntityAlreadyActive, typeof(Account));
    }

    [Fact]
    public void NonExistentAccount_ShouldFailWithDomainRuleException()
    {
        new TestContext()
            .Account()
            .Reactivate()
            .ShouldFailWithDomainRuleException(DomainRuleErrorCode.EntityNotFound, typeof(Account));
    }
}
