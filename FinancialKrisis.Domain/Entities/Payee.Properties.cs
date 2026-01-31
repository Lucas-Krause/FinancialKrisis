using FinancialKrisis.Common.Records;

namespace FinancialKrisis.Domain.Entities;

public partial class Payee
{
    public Payee() { }

    public static class Fields
    {
        public static readonly FieldKey Name = new("Name");
    }

    public string Name { get; private set; } = null!;

    public List<Transaction> Transactions { get; private set; } = [];
}
