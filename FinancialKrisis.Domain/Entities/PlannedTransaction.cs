using FinancialKrisis.Domain.Enums;
using FinancialKrisis.Domain.ValueObjects;

namespace FinancialKrisis.Domain.Entities;

public partial class PlannedTransaction : FinancialMovement
{
    public PlannedTransaction(
        Account pAccount,
        decimal pAmount,
        FinancialMovementDirection pDirection,
        PlannedSchedule pSchedule,
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
        Schedule = pSchedule;
        Status = PlannedTransactionStatus.Planned;
    }
}
