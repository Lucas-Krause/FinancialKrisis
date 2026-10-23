using FinancialKrisis.Domain.Interfaces;

namespace FinancialKrisis.Domain.Entities;

public partial class Subcategory : ActivatableEntity, INamedEntity
{
    public Subcategory(string pName, Category pCategory)
    {
        Id = Guid.NewGuid();
        ChangeName(pName);
        ChangeCategory(pCategory);
    }
}
