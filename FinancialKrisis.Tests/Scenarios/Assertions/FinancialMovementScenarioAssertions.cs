using FinancialKrisis.Domain.Entities;
using FinancialKrisis.Tests.Scenarios.Interfaces;
using Xunit.Sdk;

namespace FinancialKrisis.Tests.Scenarios.Assertions;

public static class FinancialMovementScenarioAssertions
{
    extension(IFinancialMovementScenario pScenario)
    {
        public TestContext ShouldHaveCurrentCategory()
        {
            if (pScenario.LastException is not null)
                throw pScenario.LastException;

            var movement = (FinancialMovement)pScenario.Entity!;
            Category category = pScenario.Context.GetCurrentOrThrow<Category>();

            if (movement.CategoryId != category.Id)
                throw new XunitException($"Esperava que o movimento '{movement.GetType().Name}' estivesse na categoria '{category.Id}', mas está em '{movement.CategoryId?.ToString() ?? "<null>"}'.");

            return pScenario.Context;
        }

        public TestContext ShouldHaveNoSubcategory()
        {
            if (pScenario.LastException is not null)
                throw pScenario.LastException;

            var movement = (FinancialMovement)pScenario.Entity!;

            if (movement.SubcategoryId is not null)
                throw new XunitException($"Esperava que o movimento '{movement.GetType().Name}' estivesse sem subcategoria, mas está com '{movement.SubcategoryId}'.");

            return pScenario.Context;
        }
    }
}
