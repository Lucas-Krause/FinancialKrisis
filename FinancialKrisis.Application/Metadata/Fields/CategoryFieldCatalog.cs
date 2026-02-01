using FinancialKrisis.Application.Enums;
using FinancialKrisis.Domain.Entities;
using FinancialKrisis.Common.Records;

namespace FinancialKrisis.Application.Metadata;

public class CategoryFieldCatalog
{
    public static readonly Dictionary<FieldKey, GrammarMetadata> Fields = new()
    {
        { Category.Fields.Name, new("Nome", GrammaticalGender.Masculine) },
    };
}
