namespace FinancialKrisis.Domain.Entities;

public partial class Category
{
    public void ChangeName(string pNewName)
    {
        ValidateName(pNewName);
        Name = pNewName;
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
