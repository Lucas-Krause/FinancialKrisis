using FinancialKrisis.Application.Enums;
using FinancialKrisis.Domain.Entities;
using FinancialKrisis.Domain.Enums;
using FinancialKrisis.Domain.ValueObjects;
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
    public void DuplicateNameIgnoringCaseAndSpaces_ShouldFailWithApplicationRuleException()
    {
        new TestContext()
            .Account().CreatingWith(CreateInput => CreateInput.Name = "Conta Corrente").Create().AsCurrentAccount().ShouldCreateSuccessfully()
            .Account()
                .CreatingWith(CreateInput => CreateInput.Name = "  conta CORRENTE ")
                .Create()
                .ShouldFailWithApplicationRuleException(ApplicationRuleErrorCode.ValueAlreadyInUse, typeof(Account), Account.Fields.Name);
    }

    [Fact]
    public void DuplicateNameWithAccentIgnoringCase_ShouldFailWithApplicationRuleException()
    {
        new TestContext()
            .Account().CreatingWith(CreateInput => CreateInput.Name = "Poupança").Create().AsCurrentAccount().ShouldCreateSuccessfully()
            .Account()
                .CreatingWith(CreateInput => CreateInput.Name = "POUPANÇA")
                .Create()
                .ShouldFailWithApplicationRuleException(ApplicationRuleErrorCode.ValueAlreadyInUse, typeof(Account), Account.Fields.Name);
    }

    [Fact]
    public void DuplicateNameOfInactiveAccount_ShouldFailWithApplicationRuleException()
    {
        new TestContext()
            .Account().CreatingWith(CreateInput => CreateInput.Name = "Conta Corrente").Create().AsCurrentAccount().Deactivate().ShouldDeactivateSuccessfully()
            .Account()
                .CreatingWith(CreateInput => CreateInput.Name = "Conta Corrente")
                .Create()
                .ShouldFailWithApplicationRuleException(ApplicationRuleErrorCode.ValueAlreadyInUse, typeof(Account), Account.Fields.Name);
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

    [Fact]
    public void NameExceedingMaxLength_ShouldFailWithDomainRuleException()
    {
        new TestContext()
            .Account()
            .CreatingWith(CreateInput => CreateInput.Name = new string('a', EntityName.MaxLength + 1))
            .Create()
            .ShouldFailWithDomainRuleException(DomainRuleErrorCode.MaxLengthExceeded, typeof(Account), Account.Fields.Name);
    }

    [Fact]
    public void NameWithMaxLength_ShouldCreateSuccessfully()
    {
        new TestContext()
            .Account()
            .CreatingWith(CreateInput => CreateInput.Name = new string('a', EntityName.MaxLength))
            .Create()
            .ShouldCreateSuccessfully();
    }
}
