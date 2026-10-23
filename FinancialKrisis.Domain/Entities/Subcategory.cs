namespace FinancialKrisis.Domain.Entities;

public partial class Subcategory : ActivatableEntity
{
    public Subcategory(string pName, Category pCategory)
    {
        Id = Guid.NewGuid();
        ChangeName(pName);
        ChangeCategory(pCategory);
    }
}
