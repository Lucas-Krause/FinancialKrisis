using FinancialKrisis.Domain.Enums;
using FinancialKrisis.Domain.Interfaces;

namespace FinancialKrisis.Domain.Entities;

public abstract partial class FinancialMovement : Entity, IDeletable
{
    protected FinancialMovement(
        Account pAccount,
        decimal pAmount,
        FinancialMovementDirection pDirection,
        string? pMemo,
        string? pIdentifier,
        Payee? pPayee,
        Category? pCategory,
        Subcategory? pSubcategory)
    {
        Id = Guid.NewGuid();
        ChangeAmount(pAmount);
        ChangeDirection(pDirection);
        ChangeAccount(pAccount);
        ChangeMemo(pMemo);
        ChangeIdentifier(pIdentifier);

        if (pPayee is not null)
            ChangePayee(pPayee);

        if (pCategory is not null)
            ChangeCategory(pCategory);

        if (pSubcategory is not null)
            ChangeSubcategory(pSubcategory);
    }
}
