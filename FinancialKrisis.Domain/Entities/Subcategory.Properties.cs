using FinancialKrisis.Common.Records;

namespace FinancialKrisis.Domain.Entities;

public partial class Subcategory
{
    public Subcategory() { }

    public static class Fields
    {
        public static readonly FieldKey Name = new("Name");
        public static readonly FieldKey Category = new("Category");
    }

    public string Name { get; private set; } = null!;

    public Guid CategoryId { get; private set; }
    public Category Category { get; private set; } = null!;

    public List<Transaction> Transactions { get; private set; } = [];
}
