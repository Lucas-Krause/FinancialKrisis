namespace FinancialKrisis.Domain.ValueObjects;

public sealed partial record EntityName
{
    public EntityName(string pValue, Type pOwnerType)
    {
        Validate(pValue, pOwnerType);

        Value = pValue.Trim();
        NormalizedValue = Normalize(Value);
    }
}
