using FinancialKrisis.Common.Records;
using FinancialKrisis.Domain.Enums;
using FinancialKrisis.Domain.Exceptions;

namespace FinancialKrisis.Domain.Entities;

public class Category : IEntity, IActivatable
{
    public static class Fields
    {
        public static readonly FieldKey Name = new("Name");
    }

    public Guid Id { get; private set; }
    public string Name { get; private set; } = null!;
    public bool IsActive { get; private set; }

    public List<Subcategory> Subcategories { get; private set; } = [];

    private Category() { }

    public Category(string pName)
    {
        ValidateName(pName);

        Id = Guid.NewGuid();
        Name = pName;
        IsActive = true;
    }

    public void Rename(string pNewName)
    {
        ValidateName(pNewName);
        Name = pNewName;
    }

    public void Deactivate()
    {
        IsActive = false;

        foreach (Subcategory subcategory in Subcategories)
        {
            subcategory.Deactivate();
        }
    }
    private static void ValidateName(string pName)
    {
        if (string.IsNullOrWhiteSpace(pName))
            throw new DomainRuleException(DomainRuleErrorCode.RequiredField, typeof(Category), Fields.Name);
    }
}
