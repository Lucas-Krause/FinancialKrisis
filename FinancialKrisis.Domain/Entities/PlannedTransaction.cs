using FinancialKrisis.Domain.Enums;

namespace FinancialKrisis.Domain.Entities;

public partial class PlannedTransaction : FinancialMovement
{
    public PlannedTransaction(
        Account pAccount,
        decimal pAmount,
        FinancialMovementDirection pDirection,
        DateTime pPlannedDateTime,
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
        ValidatePlannedDateTime(pPlannedDateTime);

        PlannedDateTime = pPlannedDateTime;
        Status = PlannedTransactionStatus.Planned;
    }
}
