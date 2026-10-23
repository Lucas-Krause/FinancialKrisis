using FinancialKrisis.Application.DTOs;
using FinancialKrisis.Application.Enums;
using FinancialKrisis.Domain.Entities;
using FinancialKrisis.Domain.Enums;
using FinancialKrisis.Domain.ValueObjects;
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
    public void SameNameWithDifferentCase_ShouldUpdateSuccessfully()
    {
        new TestContext()
            .Payee()
            .CreatingWith(CreateInput => CreateInput.Name = "Padaria")
            .Create()
            .AsCurrentPayee()
            .UpdatingWith(UpdateInput => UpdateInput.Name = "PADARIA")
            .Update()
            .ShouldUpdateSuccessfully();
    }

    [Fact]
    public void DuplicateName_ShouldFailWithApplicationRuleException()
    {
        new TestContext()
            .Payee().CreatingWith(CreateInput => CreateInput.Name = "Padaria").Create().AsCurrentPayee().ShouldCreateSuccessfully()
            .Payee().Create().AsCurrentPayee().ShouldCreateSuccessfully()
            .Payee()
                .UpdatingWith(UpdateInput => UpdateInput.Name = "padaria")
                .Update()
                .ShouldFailWithApplicationRuleException(ApplicationRuleErrorCode.ValueAlreadyInUse, typeof(Payee), Payee.Fields.Name);
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

    [Fact]
    public void NonExistantPayee_ShouldFailWithDomainRuleException()
    {
        new TestContext()
            .Payee()
            .Update()
            .ShouldFailWithDomainRuleException(DomainRuleErrorCode.EntityNotFound, typeof(Payee));
    }

    [Fact]
    public void NameExceedingMaxLength_ShouldFailWithDomainRuleException()
    {
        new TestContext()
            .Payee()
            .Create()
            .AsCurrentPayee()
            .UpdatingWith(UpdateInput => UpdateInput.Name = new string('a', EntityName.MaxLength + 1))
            .Update()
            .ShouldFailWithDomainRuleException(DomainRuleErrorCode.MaxLengthExceeded, typeof(Payee), Payee.Fields.Name);
    }
}
