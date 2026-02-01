using FinancialKrisis.Application.Enums;
using FinancialKrisis.Common.Records;
using FinancialKrisis.Domain.Entities;

namespace FinancialKrisis.Application.Metadata.Fields;

public class FinancialMovementFieldCatalog
{
    public static readonly Dictionary<FieldKey, GrammarMetadata> Fields = new()
    {
        { FinancialMovement.Fields.Memo, new("Memo", GrammaticalGender.Feminine) },
        { FinancialMovement.Fields.Identifier, new("Identificador", GrammaticalGender.Masculine) },
        { FinancialMovement.Fields.Amount, new("Montante", GrammaticalGender.Masculine) },
        { FinancialMovement.Fields.Direction, new("Direção", GrammaticalGender.Feminine) },
        { FinancialMovement.Fields.Account, new("Conta", GrammaticalGender.Feminine) },
        { FinancialMovement.Fields.Payee, new("Beneficiário", GrammaticalGender.Masculine) },
        { FinancialMovement.Fields.Category, new("Categoria", GrammaticalGender.Feminine) },
        { FinancialMovement.Fields.Subcategory, new("Subcategoria", GrammaticalGender.Feminine) },
    };
}
