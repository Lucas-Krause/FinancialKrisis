using FinancialKrisis.Application.DTOs;
using FinancialKrisis.Application.Enums;
using FinancialKrisis.Domain.Entities;
using FinancialKrisis.Domain.Enums;
using FinancialKrisis.Tests.Scenarios;
using FinancialKrisis.Tests.Scenarios.Assertions;

namespace FinancialKrisis.Tests.ServiceTests.Payees;

public class UpdatePayeeServiceTests
{
    [Fact]
    public void ValidInput_ShouldUpdateSuccessfully()
    {
        new TestContext()
            .Payee()
            .Create()
            .AsCurrentPayee()
            .UpdatingWith(UpdateInput => UpdateInput.Name = "Updated Payee Name")
            .Update()
            .ShouldUpdateSuccessfully();
    }

    [Fact]
    public void InvalidName_ShouldFailWithDomainRuleException()
    {
        new TestContext()
            .Payee()
            .Create()
            .AsCurrentPayee()
            .UpdatingWith(UpdateInput => UpdateInput.Name = Optional<string>.Remove())
            .Update()
            .ShouldFailWithDomainRuleException(DomainRuleErrorCode.RequiredField, typeof(Payee), Payee.Fields.Name);
    }

    [Fact]
    public void InactivePayee_ShouldFailWithApplicationRuleException()
    {
        new TestContext()
            .Payee()
            .Create()
            .AsCurrentPayee()
            .Deactivate()
            .Update()
            .ShouldFailWithApplicationRuleException(ApplicationRuleErrorCode.EntityInactive, typeof(Payee));
    }
}
