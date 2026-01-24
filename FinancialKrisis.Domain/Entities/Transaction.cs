using FinancialKrisis.Domain.Enums;
using FinancialKrisis.Domain.Exceptions;
using FinancialKrisis.Domain.Identity;

namespace FinancialKrisis.Domain.Entities;

public class Transaction
{
    public static class Fields
    {
        public static readonly FieldKey Amount = new("Amount");
        public static readonly FieldKey DateTime = new("DateTime");
        public static readonly FieldKey Category = new("Category");
        public static readonly FieldKey Subcategory = new("Subcategory");
        public static readonly FieldKey Payee = new("Payee");
        public static readonly FieldKey Identifier = new("Identifier");
        public static readonly FieldKey Description = new("Description");
        public static readonly FieldKey Direction = new("Direction");
    }

    public Guid Id { get; private set; }

    public Guid AccountId { get; private set; }
    public Guid? CategoryId { get; private set; }
    public Guid? SubcategoryId { get; private set; }
    public Guid? PayeeId { get; private set; }

    public decimal Amount { get; private set; }
    public DateTime DateTime { get; private set; }

    public string? Identifier { get; private set; }
    public string? Description { get; private set; }

    public TransactionDirection Direction { get; private set; }

    private Transaction() { }

    public Transaction(
       Guid pAccountId,
       TransactionDirection pDirection,
       decimal pAmount,
       DateTime pDateTime,
       Guid? pCategoryId = null,
       Guid? pSubcategoryId = null,
       Guid? pPayeeId = null,
       string? pIdentifier = null,
       string? pDescription = null)
    {
        ValidateAccountId(pAccountId);
        ValidateDirection(pDirection);
        ValidateAmount(pAmount);
        ValidateDateTime(pDateTime);

        Id = Guid.NewGuid();
        AccountId = pAccountId;
        Amount = pAmount;
        DateTime = pDateTime;
        CategoryId = pCategoryId;
        SubcategoryId = pSubcategoryId;
        PayeeId = pPayeeId;
        Identifier = pIdentifier;
        Description = pDescription;
    }

    public void UpdateCategory(Guid? pCategoryId)
    {
        CategoryId = pCategoryId;
    }

    public void UpdateSubcategory(Guid? pSubcategoryId)
    {
        SubcategoryId = pSubcategoryId;
    }

    public void UpdatePayee(Guid? pPayeeId)
    {
        PayeeId = pPayeeId;
    }

    public void UpdateAmount(decimal pAmount)
    {
        ValidateAmount(pAmount);
        Amount = pAmount;
    }

    public void UpdateDateTime(DateTime pDateTime)
    {
        ValidateDateTime(pDateTime);
        DateTime = pDateTime;
    }

    public void UpdateIdentifier(string? pIdentifier)
    {
        Identifier = pIdentifier;
    }

    public void UpdateDescription(string? pDescription)
    {
        Description = pDescription;
    }

    public void UpdateDirection(TransactionDirection pDirection)
    {
        ValidateDirection(pDirection);
        Direction = pDirection;
    }

    private static void ValidateAmount(decimal pAmount)
    {
        if (pAmount < 0)
            throw new DomainRuleException(DomainRuleErrorCode.NegativeAmount, typeof(Transaction), Fields.Amount);
    }

    private static void ValidateDateTime(DateTime pDateTime)
    {
        if (pDateTime == default)
            throw new DomainRuleException(DomainRuleErrorCode.RequiredField, typeof(Transaction), Fields.DateTime);
    }

    private static void ValidateAccountId(Guid pAccountId)
    {
        if (pAccountId == Guid.Empty)
            throw new DomainRuleException(DomainRuleErrorCode.RequiredField, typeof(Transaction), Fields.Identifier);
    }

    private static void ValidateDirection(TransactionDirection pDirection)
    {
        if (!Enum.IsDefined(pDirection))
            throw new DomainRuleException(DomainRuleErrorCode.RequiredField, typeof(Transaction), Fields.Direction);
    }
}
