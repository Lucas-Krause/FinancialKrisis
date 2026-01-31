using FinancialKrisis.Common.Records;
using FinancialKrisis.Domain.Enums;

namespace FinancialKrisis.Domain.Entities;

public abstract partial class FinancialMovement
{
    public FinancialMovement() { }

    public static class Fields
    {
        public static readonly FieldKey Amount = new("Amount");
        public static readonly FieldKey Memo = new("Memo");
        public static readonly FieldKey Identifier = new("Identifier");
        public static readonly FieldKey Direction = new("Direction");
        public static readonly FieldKey Account = new("Account");
        public static readonly FieldKey Payee = new("Payee");
        public static readonly FieldKey Category = new("Category");
        public static readonly FieldKey Subcategory = new("Subcategory");
    }

    public decimal Amount { get; protected set; }
    public string? Memo { get; protected set; }
    public string? Identifier { get; protected set; }
    public FinancialMovementDirection Direction { get; protected set; }

    public Guid AccountId { get; protected set; }
    public Account Account { get; protected set; } = null!;

    public Guid? CategoryId { get; protected set; }
    public Category? Category { get; protected set; }

    public Guid? SubcategoryId { get; protected set; }
    public Subcategory? Subcategory { get; protected set; }

    public Guid? PayeeId { get; protected set; }
    public Payee? Payee { get; protected set; }
}
