using FinancialKrisis.Common.Records;
using FinancialKrisis.Domain.Abstractions;
using FinancialKrisis.Domain.Enums;

namespace FinancialKrisis.Domain.Entities;

public partial class PlannedTransaction
{
    public static new class Fields
    {
        public static FieldKey Amount => FinancialMovement.Fields.Amount;
        public static FieldKey Memo => FinancialMovement.Fields.Memo;
        public static FieldKey Identifier => FinancialMovement.Fields.Identifier;
        public static FieldKey Direction => FinancialMovement.Fields.Direction;
        public static FieldKey Account => FinancialMovement.Fields.Account;
        public static FieldKey Payee => FinancialMovement.Fields.Payee;
        public static FieldKey Category => FinancialMovement.Fields.Category;
        public static FieldKey Subcategory => FinancialMovement.Fields.Subcategory;

        public static readonly FieldKey PlannedDateTime = new("PlannedDateTime");
        public static readonly FieldKey Status = new("Status");
    }

    public DateTime PlannedDateTime { get; private set; }
    public PlannedTransactionStatus Status { get; private set; }
}
