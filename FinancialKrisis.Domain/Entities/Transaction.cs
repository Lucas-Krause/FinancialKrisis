using FinancialKrisis.Domain.Abstractions;
using FinancialKrisis.Domain.Enums;

namespace FinancialKrisis.Domain.Entities;

public partial class Transaction : FinancialMovement
{
    private Transaction() : base() { }

    public Transaction(
        Account pAccount,
        decimal pAmount,
        DateTime pDateTime,
        FinancialMovementDirection pDirection,
        string? pMemo = null,
        string? pIdentifier = null,
        Payee? pPayee = null,
        Category? pCategory = null,
        Subcategory? pSubcategory = null)
        : base(
            pAccount,
            pAmount,
            pDirection,
            pMemo,
            pIdentifier,
            pPayee,
            pCategory,
            pSubcategory)
    {
        ValidateDateTime(pDateTime);

        DateTime = pDateTime;
    }
}
