using FinancialKrisis.Domain.Enums;
using FinancialKrisis.Domain.Exceptions;

namespace FinancialKrisis.Domain.Entities;

public partial class Subcategory
{
    private static void ValidateName(string pName)
    {
        if (string.IsNullOrWhiteSpace(pName))
            throw new DomainRuleException(DomainRuleErrorCode.RequiredField, typeof(Subcategory), Fields.Name);
    }

    public bool BelongsToCategory(Category category)
    {
        return CategoryId == category.Id;
    }
}
