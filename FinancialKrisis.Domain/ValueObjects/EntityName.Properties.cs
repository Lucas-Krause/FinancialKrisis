using FinancialKrisis.Common.Records;

namespace FinancialKrisis.Domain.ValueObjects;

public sealed partial record EntityName
{
    public const int MaxLength = 150;

    public EntityName() { }

    public static class Fields
    {
        public static readonly FieldKey Name = new("Name");
    }

    public string Value { get; private set; } = null!;
    public string NormalizedValue { get; private set; } = null!;
}
