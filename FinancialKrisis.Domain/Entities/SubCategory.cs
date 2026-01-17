using FinancialKrisis.Domain.Common;
using FinancialKrisis.Domain.Identity;

namespace FinancialKrisis.Domain.Entities;

public class Subcategory : IActivatable
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

    private Subcategory() { }

    public Subcategory(string name, Category category)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Subcategory name is required.");
        Id = Guid.NewGuid();
        Name = name;
        Category = category ?? throw new ArgumentNullException(nameof(category));
        CategoryId = category.Id;
        IsActive = true;
    }

    public void Rename(string newName)
    {
        if (string.IsNullOrWhiteSpace(newName))
            throw new ArgumentException("Subcategory name is required.");
        Name = newName;
    }

    public void ChangeCategory(Category category)
    {
        Category = category ?? throw new ArgumentNullException(nameof(category));
        CategoryId = category.Id;
    }

    public void Deactivate()
    {
        IsActive = false;
    }
}
