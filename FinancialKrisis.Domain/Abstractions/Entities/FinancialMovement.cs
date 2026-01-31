using FinancialKrisis.Domain.Entities;
using FinancialKrisis.Domain.Enums;

namespace FinancialKrisis.Domain.Abstractions;

public abstract partial class FinancialMovement : IEntity, IDeletable
{
    protected FinancialMovement() { }

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
        ValidateAmount(pAmount);
        ValidateDirection(pDirection);

        Id = Guid.NewGuid();
        AccountId = pAccount.Id;
        Account = pAccount;

        Amount = pAmount;
        Direction = pDirection;
        Memo = pMemo;
        Identifier = pIdentifier;

        PayeeId = pPayee?.Id;
        Payee = pPayee;

        CategoryId = pCategory?.Id;
        Category = pCategory;

        SubcategoryId = pSubcategory?.Id;
        Subcategory = pSubcategory;
    }
}
