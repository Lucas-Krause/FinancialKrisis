using FinancialKrisis.Application.DTOs;
using FinancialKrisis.Application.Enums;
using FinancialKrisis.Domain.Entities;
using FinancialKrisis.Domain.Enums;
using FinancialKrisis.Tests.Scenarios;
using FinancialKrisis.Tests.Scenarios.Assertions;

namespace FinancialKrisis.Tests.ServiceTests.Categories;

public class UpdateCategoryServiceTests
{
    [Fact]
    public void ValidInput_ShouldUpdateSuccessfully()
    {
        new TestContext()
            .Category()
            .Create()
            .AsCurrentCategory()
            .Update()
            .ShouldUpdateSuccessfully();
    }

    [Fact]
    public void NonExistentCategory_ShouldFailWithDomainRuleException()
    {
        new TestContext()
            .Category()
            .Update()
            .ShouldFailWithDomainRuleException(DomainRuleErrorCode.EntityNotFound, typeof(Category));
    }

    [Fact]
    public void InactiveCategory_ShouldFailWithApplicationRuleException()
    {
        new TestContext()
            .Category()
            .Create()
            .AsCurrentCategory()
            .Deactivate()
            .Update()
            .ShouldFailWithApplicationRuleException(ApplicationRuleErrorCode.EntityInactive, typeof(Category));
    }

    [Fact]
    public void InvalidName_ShouldFailWithDomainRuleException()
    {
        new TestContext()
            .Category()
            .Create()
            .AsCurrentCategory()
            .UpdatingWith(UpdateInput => UpdateInput.Name = Optional<string>.Remove())
            .Update()
            .ShouldFailWithDomainRuleException(DomainRuleErrorCode.RequiredField, typeof(Category), Category.Fields.Name);
    }
}
