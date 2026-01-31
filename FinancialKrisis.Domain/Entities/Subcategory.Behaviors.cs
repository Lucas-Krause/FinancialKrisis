namespace FinancialKrisis.Domain.Entities;

public partial class Subcategory
{
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
}
