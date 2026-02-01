using FinancialKrisis.Application.Enums;
using FinancialKrisis.Application.Metadata.Fields;
using FinancialKrisis.Common.Records;
using FinancialKrisis.Domain.Entities;

namespace FinancialKrisis.Application.Metadata;

public class PlannedTransactionFieldCatalog
{
    public static readonly Dictionary<FieldKey, GrammarMetadata> Fields = new()
    {
        { PlannedTransaction.Fields.Status, new("Status", GrammaticalGender.Masculine) },
        { PlannedTransaction.Fields.Schedule, new("Agendamento", GrammaticalGender.Masculine) },
    };

    static PlannedTransactionFieldCatalog()
    {
        foreach (KeyValuePair<FieldKey, GrammarMetadata> financialMovementField in FinancialMovementFieldCatalog.Fields)
            if (!Fields.ContainsKey(financialMovementField.Key))
                Fields.Add(financialMovementField.Key, financialMovementField.Value);
    }
}
