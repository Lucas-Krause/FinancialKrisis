using System;

namespace FinancialKrisis.Domain.Entities;

public class SubCategory
{
    public Guid Id { get; private set; }
    public string Name { get; private set; } = null!;
    public bool IsActive { get; private set; }
    public Guid CategoryId { get; private set; }
    public Category Category { get; private set; } = null!;

    private SubCategory() { }

    public SubCategory(string name, Category category)
    {
        Id = Guid.NewGuid();
        Name = name;
        Category = category ?? throw new ArgumentNullException(nameof(category));
        CategoryId = category.Id;
        IsActive = true;
    }

    public void Rename(string newName)
    {
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
