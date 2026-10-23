using FinancialKrisis.Domain.ValueObjects;

namespace FinancialKrisis.Domain.Entities;

public partial class Category
{
    public void ChangeName(string pNewName)
    {
        Name = new EntityName(pNewName, typeof(Category));
    }

    public override void Deactivate()
    {
        base.Deactivate();

        foreach (Subcategory subcategory in Subcategories.Where(s => s.IsActive))
        {
            subcategory.DeactivateByCategory();
        }
    }

    public override void Reactivate()
    {
        base.Reactivate();

        foreach (Subcategory subcategory in Subcategories.Where(s => s.DeactivatedByCategory))
        {
            subcategory.Reactivate();
        }
    }
}
