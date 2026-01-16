namespace FinancialKrisis.Domain.Entities;

public class Transaction
{
    public Guid Id { get; private set; }
    public string Identifier { get; private set; } = null!;
    public string Description { get; private set; } = null!;
    public DateTime DateTime { get; private set; }
    public Guid AccountId { get; private set; }
    public Account Account { get; private set; } = null!;
    public Guid? PayeeId { get; private set; }
    public Payee? Payee { get; private set; }
    public Guid? CategoryId { get; private set; }
    public Category? Category { get; private set; }
    public Guid? SubCategoryId { get; private set; }
    public SubCategory? SubCategory { get; private set; }
    public decimal Amount { get; private set; }

    private Transaction() { }

    public Transaction(
        string pIdentifier,
        string pDescription,
        DateTime pDateTime,
        Account pAccount,
        Payee? pPayee,
        Category? pCategory,
        SubCategory? pSubCategory,
        decimal pAmount)
    {
        Id = Guid.NewGuid();
        Identifier = pIdentifier;
        Description = pDescription;
        DateTime = pDateTime;
        Account = pAccount ?? throw new ArgumentNullException(nameof(pAccount));
        AccountId = pAccount.Id;
        Payee = pPayee;
        PayeeId = pPayee?.Id;
        Category = pCategory;
        CategoryId = pCategory?.Id;
        SubCategory = pSubCategory;
        SubCategoryId = pSubCategory?.Id;
        Amount = pAmount;
    }

    public void ChangeDescription(string pDescription)
    {
        Description = pDescription;
    }

    public void ChangeAmount(decimal pAmount)
    {
        Amount = pAmount;
    }

    public void ChangeDateTime(DateTime pDateTime)
    {
        DateTime = pDateTime;
    }

    public void ChangeAccount(Account pAccount)
    {
        Account = pAccount ?? throw new ArgumentNullException(nameof(pAccount));
        AccountId = pAccount.Id;
    }

    public void ChangePayee(Payee? pPayee)
    {
        Payee = pPayee;
        PayeeId = pPayee?.Id;
    }

    public void ChangeCategory(Category? pCategory)
    {
        Category = pCategory;
        CategoryId = pCategory?.Id;
    }

    public void ChangeSubCategory(SubCategory? pSubCategory)
    {
        SubCategory = pSubCategory;
        SubCategoryId = pSubCategory?.Id;
    }
}
