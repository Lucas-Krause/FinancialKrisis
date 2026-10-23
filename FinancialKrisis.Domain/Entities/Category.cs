namespace FinancialKrisis.Domain.Entities;

public partial class Category : ActivatableEntity
{
    public Category(string pName)
    {
        Id = Guid.NewGuid();
        ChangeName(pName);
    }
}
