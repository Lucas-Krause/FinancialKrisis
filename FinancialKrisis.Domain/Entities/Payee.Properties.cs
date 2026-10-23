using FinancialKrisis.Common.Records;
using FinancialKrisis.Domain.ValueObjects;

namespace FinancialKrisis.Domain.Entities;

public partial class Payee
{
    public Payee() { }

    public static class Fields
    {
        public static readonly FieldKey Name = EntityName.Fields.Name;
    }

    public EntityName Name { get; private set; } = null!;

    public List<Transaction> Transactions { get; private set; } = [];
}
