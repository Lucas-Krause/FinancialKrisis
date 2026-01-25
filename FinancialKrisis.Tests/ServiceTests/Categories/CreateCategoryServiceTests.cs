using FinancialKrisis.Domain.Entities;
using FinancialKrisis.Domain.Enums;
using FinancialKrisis.Tests.Scenarios;
using FinancialKrisis.Tests.Scenarios.Assertions;

namespace FinancialKrisis.Tests.ServiceTests.Categories;

public class CreateCategoryServiceTests
{
    [Fact]
    public void ValidInput_ShouldCreateSuccessfully()
    {
        new TestContext()
            .Category()
            .Create()
            .AsCurrentCategory()
            .ShouldCreateSuccessfully();
    }

    [Fact]
    public void InvalidName_ShouldFailWithDomainRuleException()
    {
        new TestContext()
            .Category()
            .CreatingWith(CreateInput => CreateInput.Name = string.Empty)
            .Create()
            .ShouldFailWithDomainRuleException(DomainRuleErrorCode.RequiredField, typeof(Category), Category.Fields.Name);
    }
}
