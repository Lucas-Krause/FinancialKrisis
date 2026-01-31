using FinancialKrisis.Domain.Entities;
using FinancialKrisis.Domain.Enums;

namespace FinancialKrisis.Domain.Abstractions;

public abstract partial class FinancialMovement
{
    public void ChangeAmount(decimal pAmount)
    {
        ValidateAmount(pAmount);
        Amount = pAmount;
    }

    public void ChangeMemo(string? pMemo)
    {
        Memo = pMemo;
    }

    public void ChangeIdentifier(string? pIdentifier)
    {
        Identifier = pIdentifier;
    }

    public void ChangeDirection(FinancialMovementDirection pDirection)
    {
        ValidateDirection(pDirection);
        Direction = pDirection;
    }

    public void ChangePayee(Payee pPayee)
    {
        PayeeId = pPayee.Id;
        Payee = pPayee;
    }

    public void RemovePayee()
    {
        PayeeId = null;
        Payee = null;
    }

    public void ChangeCategory(Category pCategory)
    {
        CategoryId = pCategory.Id;
        Category = pCategory;
    }

    public void RemoveCategory()
    {
        CategoryId = null;
        Category = null;
    }

    public void ChangeSubcategory(Subcategory pSubcategory)
    {
        SubcategoryId = pSubcategory.Id;
        Subcategory = pSubcategory;
    }

    public void RemoveSubcategory()
    {
        SubcategoryId = null;
        Subcategory = null;
    }
}
