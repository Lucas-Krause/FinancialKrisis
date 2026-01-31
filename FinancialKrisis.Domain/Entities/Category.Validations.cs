using FinancialKrisis.Domain.Enums;
using FinancialKrisis.Domain.Exceptions;

namespace FinancialKrisis.Domain.Entities;

public partial class Category
{
    private static void ValidateName(string pName)
    {
        if (string.IsNullOrWhiteSpace(pName))
            throw new DomainRuleException(DomainRuleErrorCode.RequiredField, typeof(Category), Fields.Name);
    }
}
