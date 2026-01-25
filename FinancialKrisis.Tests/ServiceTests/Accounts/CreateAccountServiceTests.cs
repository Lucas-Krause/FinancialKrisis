using FinancialKrisis.Domain.Entities;
using FinancialKrisis.Domain.Enums;
using FinancialKrisis.Tests.Scenarios;
using FinancialKrisis.Tests.Scenarios.Assertions;

namespace FinancialKrisis.Tests.ServiceTests.Accounts;

public class CreateAccountServiceTests
{
    [Fact]
    public void ValidInput_ShouldCreateSuccessfully()
    {
        new TestContext()
            .Account()
            .Create()
            .AsCurrentAccount()
            .ShouldCreateSuccessfully();
    }

    [Fact]
    public void InvalidName_ShouldFailWithDomainRuleException()
    {
        new TestContext()
            .Account()
            .CreatingWith(CreateInput => CreateInput.Name = string.Empty)
            .Create()
            .ShouldFailWithDomainRuleException(DomainRuleErrorCode.RequiredField, typeof(Account), Account.Fields.Name);
    }

    [Fact]
    public void InvalidAccountNumber_ShouldFailWithDomainRuleException()
    {
        new TestContext()
            .Account()
            .CreatingWith(CreateInput => CreateInput.AccountNumber = string.Empty)
            .Create()
            .ShouldFailWithDomainRuleException(DomainRuleErrorCode.RequiredField, typeof(Account), Account.Fields.AccountNumber);
    }

    [Fact]
    public void NegativeInitialBalance_ShouldFailWithDomainRuleException()
    {
        new TestContext()
            .Account()
            .CreatingWith(CreateInput => CreateInput.InitialBalance = -100)
            .Create()
            .ShouldFailWithDomainRuleException(DomainRuleErrorCode.NegativeValue, typeof(Account), Account.Fields.InitialBalance);
    }
}
