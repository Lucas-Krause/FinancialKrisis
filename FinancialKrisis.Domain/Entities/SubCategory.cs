namespace FinancialKrisis.Domain.Entities;

public partial class Subcategory : ActivatableEntity
{
    public Subcategory(string pName, Category pCategory)
    {
        ValidateName(pName);

        Id = Guid.NewGuid();
        Name = pName;
        Category = pCategory;
        CategoryId = pCategory.Id;
    }
}
