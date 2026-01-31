using FinancialKrisis.Application.Enums;
using FinancialKrisis.Application.Metadata.Fields;
using FinancialKrisis.Common.Records;
using FinancialKrisis.Domain.Entities;

namespace FinancialKrisis.Application.Metadata;

public class TransactionFieldCatalog
{
    public static readonly Dictionary<FieldKey, FieldMetadata> Fields = new()
    {
        { Transaction.Fields.DateTime, new("Data", GrammaticalGender.Feminine) },
    };

    static TransactionFieldCatalog()
    {
        foreach (KeyValuePair<FieldKey, FieldMetadata> financialMovementField in FinancialMovementFieldCatalog.Fields)
            if (!Fields.ContainsKey(financialMovementField.Key))
                Fields.Add(financialMovementField.Key, financialMovementField.Value);
    }
}