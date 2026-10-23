namespace FinancialKrisis.Domain.ValueObjects;

public sealed partial record EntityName
{
    private static string Normalize(string pValue)
    {
        return pValue.Trim().ToUpperInvariant();
    }

    public override string ToString()
    {
        return Value;
    }
}
