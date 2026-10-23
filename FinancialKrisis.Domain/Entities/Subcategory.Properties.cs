using FinancialKrisis.Common.Records;
using FinancialKrisis.Domain.ValueObjects;

namespace FinancialKrisis.Domain.Entities;

public partial class Subcategory
{
    public Subcategory() { }

    public static class Fields
    {
        public static readonly FieldKey Name = EntityName.Fields.Name;
        public static readonly FieldKey Category = new("Category");
    }

    public EntityName Name { get; private set; } = null!;
    public bool DeactivatedByCategory { get; private set; }

    public Guid CategoryId { get; private set; }
    public Category Category { get; private set; } = null!;

    public List<Transaction> Transactions { get; private set; } = [];
}
