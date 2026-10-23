using FinancialKrisis.Domain.ValueObjects;

namespace FinancialKrisis.Domain.Entities;

public partial class Subcategory
{
    public void ChangeName(string pNewName)
    {
        Name = new EntityName(pNewName, typeof(Subcategory));
    }

    public void ChangeCategory(Category pNewCategory)
    {
        Category = pNewCategory;
        CategoryId = pNewCategory.Id;
    }

    public override void Deactivate()
    {
        base.Deactivate();
        DeactivatedByCategory = false;
    }

    public void DeactivateByCategory()
    {
        Deactivate();
        DeactivatedByCategory = true;
    }

    public override void Reactivate()
    {
        base.Reactivate();
        DeactivatedByCategory = false;
    }
}
