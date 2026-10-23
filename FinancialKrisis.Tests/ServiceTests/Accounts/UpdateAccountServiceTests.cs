using FinancialKrisis.Application.DTOs;
using FinancialKrisis.Application.Enums;
using FinancialKrisis.Domain.Entities;
using FinancialKrisis.Domain.Enums;
using FinancialKrisis.Domain.ValueObjects;
using FinancialKrisis.Tests.Scenarios;
using FinancialKrisis.Tests.Scenarios.Assertions;

namespace FinancialKrisis.Tests.ServiceTests.Accounts;

public class UpdateAccountServiceTests
{
    [Fact]
    public void ValidInput_ShouldUpdateSuccessfully()
    {
        new TestContext()
            .Account()
            .Create()
            .AsCurrentAccount()
            .UpdatingWith(UpdateInput =>
            {
                UpdateInput.Name = "New Name";
                UpdateInput.AccountNumber = "123123123";
                UpdateInput.InitialBalance = 1000;
            })
            .Update()
            .ShouldUpdateSuccessfully();
    }

    [Fact]
    public void SameNameWithDifferentCase_ShouldUpdateSuccessfully()
    {
        new TestContext()
            .Account()
            .CreatingWith(CreateInput => CreateInput.Name = "Conta Corrente")
            .Create()
            .AsCurrentAccount()
            .UpdatingWith(UpdateInput => UpdateInput.Name = "CONTA CORRENTE")
            .Update()
            .ShouldUpdateSuccessfully();
    }

    [Fact]
    public void DuplicateName_ShouldFailWithApplicationRuleException()
    {
        new TestContext()
            .Account().CreatingWith(CreateInput => CreateInput.Name = "Conta Corrente").Create().AsCurrentAccount().ShouldCreateSuccessfully()
            .Account().Create().AsCurrentAccount().ShouldCreateSuccessfully()
            .Account()
                .UpdatingWith(UpdateInput => UpdateInput.Name = "conta corrente")
                .Update()
                .ShouldFailWithApplicationRuleException(ApplicationRuleErrorCode.ValueAlreadyInUse, typeof(Account), Account.Fields.Name);
    }

    [Fact]
    public void InactiveAccount_ShouldFailWithApplicationRuleException()
    {
        new TestContext()
            .Account()
            .Create()
            .AsCurrentAccount()
            .Deactivate()
            .Update()
            .ShouldFailWithApplicationRuleException(ApplicationRuleErrorCode.EntityInactive, typeof(Account));
    }

    [Fact]
    public void InvalidName_ShouldFailWithDomainRuleException()
    {
        new TestContext()
            .Account()
            .Create()
            .AsCurrentAccount()
            .UpdatingWith(UpdateInput => UpdateInput.Name = Optional<string>.Remove())
            .Update()
            .ShouldFailWithDomainRuleException(DomainRuleErrorCode.RequiredField, typeof(Account), Account.Fields.Name);
    }

    [Fact]
    public void InvalidAccountNumber_ShouldFailWithDomainRuleException()
    {
        new TestContext()
            .Account()
            .Create()
            .AsCurrentAccount()
            .UpdatingWith(UpdateInput => UpdateInput.AccountNumber = Optional<string>.Remove())
            .Update()
            .ShouldFailWithDomainRuleException(DomainRuleErrorCode.RequiredField, typeof(Account), Account.Fields.AccountNumber);
    }

    [Fact]
    public void NegativeInitialBalance_ShouldFailWithDomainRuleException()
    {
        new TestContext()
            .Account()
            .Create()
            .AsCurrentAccount()
            .UpdatingWith(UpdateInput => UpdateInput.InitialBalance = -100)
            .Update()
            .ShouldFailWithDomainRuleException(DomainRuleErrorCode.NegativeValue, typeof(Account), Account.Fields.InitialBalance);
    }

    [Fact]
    public void NonExistantAccount_ShouldFailWithDomainRuleException()
    {
        new TestContext()
            .Account()
            .Update()
            .ShouldFailWithDomainRuleException(DomainRuleErrorCode.EntityNotFound, typeof(Account));
    }

    [Fact]
    public void NameExceedingMaxLength_ShouldFailWithDomainRuleException()
    {
        new TestContext()
            .Account()
            .Create()
            .AsCurrentAccount()
            .UpdatingWith(UpdateInput => UpdateInput.Name = new string('a', EntityName.MaxLength + 1))
            .Update()
            .ShouldFailWithDomainRuleException(DomainRuleErrorCode.MaxLengthExceeded, typeof(Account), Account.Fields.Name);
    }
}
