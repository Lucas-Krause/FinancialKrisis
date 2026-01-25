using FinancialKrisis.Application.DTOs;
using FinancialKrisis.Application.Enums;
using FinancialKrisis.Domain.Entities;
using FinancialKrisis.Domain.Enums;
using FinancialKrisis.Tests.Scenarios;
using FinancialKrisis.Tests.Scenarios.Assertions;

namespace FinancialKrisis.Tests.ServiceTests.Accounts;

public class UpdateAccountServiceTests
{
    [Fact]
    public async Task ValidInput_ShouldUpdateSuccessfully()
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
    public async Task InactiveAccount_ShouldFailWithDomainRuleException()
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
    public async Task InvalidName_ShouldFailWithDomainRuleException()
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
    public async Task InvalidAccountNumber_ShouldFailWithDomainRuleException()
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
    public async Task NegativeInitialBalance_ShouldFailWithDomainRuleException()
    {
        new TestContext()
            .Account()
            .Create()
            .AsCurrentAccount()
            .UpdatingWith(UpdateInput => UpdateInput.InitialBalance = -100)
            .Update()
            .ShouldFailWithDomainRuleException(DomainRuleErrorCode.NegativeValue, typeof(Account), Account.Fields.InitialBalance);
    }
}
