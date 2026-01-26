using FinancialKrisis.Common.Records;
using FinancialKrisis.Domain.Abstractions;
using FinancialKrisis.Domain.Enums;
using FinancialKrisis.Domain.Exceptions;

namespace FinancialKrisis.Domain.Entities;

public class Transaction : IEntity
{
    public static class Fields
    {
        public static readonly FieldKey Amount = new("Amount");
        public static readonly FieldKey Memo = new("Memo");
        public static readonly FieldKey Identifier = new("Identifier");
        public static readonly FieldKey DateTime = new("DateTime");
        public static readonly FieldKey Direction = new("Direction");
        public static readonly FieldKey Account = new("Account");
        public static readonly FieldKey Payee = new("Payee");
        public static readonly FieldKey Category = new("Category");
        public static readonly FieldKey Subcategory = new("Subcategory");
    }

    public Guid Id { get; private set; }

    public decimal Amount { get; private set; }
    public string? Memo { get; private set; }
    public string? Identifier { get; private set; }
    public DateTime DateTime { get; private set; }
    public TransactionDirection Direction { get; private set; }

    public Guid AccountId { get; private set; }
    public Account Account { get; private set; } = null!;

    public Guid? CategoryId { get; private set; }
    public Category? Category { get; private set; }

    public Guid? SubcategoryId { get; private set; }
    public Subcategory? Subcategory { get; private set; }

    public Guid? PayeeId { get; private set; }
    public Payee? Payee { get; private set; }

    private Transaction() { }

    public Transaction(
       Account pAccount,
       decimal pAmount,
       DateTime pDateTime,
       TransactionDirection pDirection,
       string? pMemo = null,
       string? pIdentifier = null,
       Payee? pPayee = null,
       Category? pCategory = null,
       Subcategory? pSubcategory = null)
    {
        ValidateAmount(pAmount);
        ValidateDateTime(pDateTime);
        ValidateDirection(pDirection);

        Id = Guid.NewGuid();
        AccountId = pAccount.Id;
        Account = pAccount;

        Amount = pAmount;
        Memo = pMemo;
        Identifier = pIdentifier;
        DateTime = pDateTime;

        if (pPayee is not null)
        {
            PayeeId = pPayee.Id;
            Payee = pPayee;
        }

        if (pCategory is not null)
        {
            CategoryId = pCategory.Id;
            Category = pCategory;
        }

        if (pSubcategory is not null)
        {
            SubcategoryId = pSubcategory.Id;
            Subcategory = pSubcategory;
        }
    }

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

    public void ChangePayee(Payee pPayee)
    {
        PayeeId = pPayee.Id;
        Payee = pPayee;
    }

    public void ChangeCategory(Category pCategory)
    {
        CategoryId = pCategory.Id;
        Category = pCategory;
    }

    public void ChangeSubcategory(Subcategory pSubcategory)
    {
        SubcategoryId = pSubcategory.Id;
        Subcategory = pSubcategory;
    }

    public void RemovePayee()
    {
        PayeeId = null;
        Payee = null;
    }

    public void RemoveCategory()
    {
        CategoryId = null;
        Category = null;
    }

    public void RemoveSubcategory()
    {
        SubcategoryId = null;
        Subcategory = null;
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
