using FinancialKrisis.Common.Records;
using FinancialKrisis.Domain.Enums;
using FinancialKrisis.Domain.Exceptions;

namespace FinancialKrisis.Domain.Entities;

public class Transaction : IEntity
{
    public static class Fields
    {
        public static readonly FieldKey Amount = new("Amount");
        public static readonly FieldKey DateTime = new("DateTime");
        public static readonly FieldKey Category = new("Category");
        public static readonly FieldKey Subcategory = new("Subcategory");
        public static readonly FieldKey Payee = new("Payee");
        public static readonly FieldKey Identifier = new("Identifier");
        public static readonly FieldKey Memo = new("Memo");
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
    public string? Memo { get; private set; }

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
       string? pMemo = null)
    {
        ValidateAmount(pAmount);
        ValidateDateTime(pDateTime);
        ValidateDirection(pDirection);

        Id = Guid.NewGuid();
        AccountId = pAccountId;
        Amount = pAmount;
        DateTime = pDateTime;
        CategoryId = pCategoryId;
        SubcategoryId = pSubcategoryId;
        PayeeId = pPayeeId;
        Identifier = pIdentifier;
        Memo = pMemo;
    }

    public void ChangeAmount(decimal pAmount)
    {
        ValidateAmount(pAmount);
        Amount = pAmount;
    }

    public void ChangeIdentifier(string? pIdentifier)
    {
        Identifier = pIdentifier;
    }

    public void ChangeMemo(string? pMemo)
    {
        Memo = pMemo;
    }

    public void ChangeDateTime(DateTime pDateTime)
    {
        ValidateDateTime(pDateTime);
        DateTime = pDateTime;
    }

    public void ChangeDirection(TransactionDirection pDirection)
    {
        ValidateDirection(pDirection);
        Direction = pDirection;
    }

    public void ChangePayee(Guid? pPayeeId)
    {
        PayeeId = pPayeeId;
    }

    public void ChangeCategory(Guid? pCategoryId)
    {
        CategoryId = pCategoryId;
    }

    public void ChangeSubcategory(Guid? pSubcategoryId)
    {
        SubcategoryId = pSubcategoryId;
    }

    private static void ValidateAmount(decimal pAmount)
    {
        if (pAmount < 0)
            throw new DomainRuleException(DomainRuleErrorCode.NegativeValue, typeof(Transaction), Fields.Amount);
    }

    private static void ValidateDateTime(DateTime pDateTime)
    {
        if (pDateTime == default)
            throw new DomainRuleException(DomainRuleErrorCode.RequiredField, typeof(Transaction), Fields.DateTime);
    }

    private static void ValidateDirection(TransactionDirection pDirection)
    {
        if (!Enum.IsDefined(pDirection))
            throw new DomainRuleException(DomainRuleErrorCode.RequiredField, typeof(Transaction), Fields.Direction);
    }
}
