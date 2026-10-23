using FinancialKrisis.Domain.Interfaces;

namespace FinancialKrisis.Domain.Entities;

public partial class Category : ActivatableEntity, INamedEntity
{
    public Category(string pName)
    {
        Id = Guid.NewGuid();
        ChangeName(pName);
    }
}
