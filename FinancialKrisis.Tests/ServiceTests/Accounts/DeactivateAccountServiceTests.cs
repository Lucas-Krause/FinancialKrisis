using FinancialKrisis.Domain.Entities;
using FinancialKrisis.Domain.Enums;
using FinancialKrisis.Tests.Scenarios;
using FinancialKrisis.Tests.Scenarios.Assertions;

namespace FinancialKrisis.Tests.ServiceTests.Accounts;

public class DeactivateAccountServiceTests
{
    [Fact]
    public void ValidInput_ShouldDeactivateSuccessfully()
    {
        new TestContext()
            .Account()
            .Create()
            .AsCurrentAccount()
            .Deactivate()
            .ShouldDeactivateSuccessfully();
    }

    [Fact]
    public void NonExistantAccount_ShouldFailWithDomainRuleException()
    {
        new TestContext()
            .Account()
            .Deactivate()
            .ShouldFailWithDomainRuleException(DomainRuleErrorCode.EntityNotFound, typeof(Account));
    }
}
