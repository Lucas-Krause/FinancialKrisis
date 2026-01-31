namespace FinancialKrisis.Domain.Entities;

public partial class Category : ActivatableEntity
{
    public Category(string pName)
    {
        ValidateName(pName);

        Id = Guid.NewGuid();
        Name = pName;
    }
}
