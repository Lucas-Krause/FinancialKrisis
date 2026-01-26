using FinancialKrisis.Common.Records;
using FinancialKrisis.Domain.Abstractions;
using FinancialKrisis.Domain.Enums;
using FinancialKrisis.Domain.Exceptions;

namespace FinancialKrisis.Domain.Entities;

public class Subcategory : IEntity, IActivatable
{
    public static class Fields
    {
        public static readonly FieldKey Name = new("Name");
        public static readonly FieldKey Category = new("Category");
    }

    public Guid Id { get; private set; }
    public string Name { get; private set; } = null!;
    public bool IsActive { get; private set; }

    public Guid CategoryId { get; private set; }
    public Category Category { get; private set; } = null!;

    public List<Transaction> Transactions { get; private set; } = [];

    private Subcategory() { }

    public Subcategory(string pName, Category pCategory)
    {
        ValidateName(pName);

        Id = Guid.NewGuid();
        Name = pName;
        Category = pCategory;
        CategoryId = pCategory.Id;
        IsActive = true;
    }

    public void ChangeName(string pNewName)
    {
        ValidateName(pNewName);
        Name = pNewName;
    }

    public void ChangeCategory(Category pNewCategory)
    {
        Category = pNewCategory;
        CategoryId = pNewCategory.Id;
    }

    public void Deactivate()
    {
        IsActive = false;
    }

    private static void ValidateName(string pName)
    {
        if (string.IsNullOrWhiteSpace(pName))
            throw new DomainRuleException(DomainRuleErrorCode.RequiredField, typeof(Subcategory), Fields.Name);
    }

    public bool BelongsToCategory(Category category)
    {
        return CategoryId == category.Id;
    }
}
