using FinancialKrisis.Application.Enums;
using FinancialKrisis.Common.Records;
using FinancialKrisis.Domain.Entities;

namespace FinancialKrisis.Application.Metadata;

public class SubcategoryFieldCatalog
{
    public static readonly Dictionary<FieldKey, FieldMetadata> Fields = new()
    {
        { Subcategory.Fields.Name, new("Nome", GrammaticalGender.Masculine) },
        { Subcategory.Fields.Category, new("Categoria", GrammaticalGender.Feminine) },
    };
}
