namespace FinancialKrisis.Domain.Entities;

public partial class Subcategory
{
    public bool BelongsToCategory(Category pCategory)
    {
        return CategoryId == pCategory.Id;
    }
}
