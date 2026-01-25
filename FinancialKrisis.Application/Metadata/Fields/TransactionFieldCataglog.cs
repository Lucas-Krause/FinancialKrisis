using FinancialKrisis.Application.Enums;
using FinancialKrisis.Common.Records;
using FinancialKrisis.Domain.Entities;

namespace FinancialKrisis.Application.Metadata;

public static class TransactionFieldCatalog
{
    public static readonly Dictionary<FieldKey, FieldMetadata> Fields =
        new()
        {
            { Transaction.Fields.Memo, new("Memo", GrammaticalGender.Feminine) },
            { Transaction.Fields.Identifier, new("Identificador", GrammaticalGender.Masculine) },
            { Transaction.Fields.Amount, new("Montante", GrammaticalGender.Masculine) },
            { Transaction.Fields.DateTime, new("Data", GrammaticalGender.Feminine) },
            { Transaction.Fields.Direction, new("Direção", GrammaticalGender.Feminine) },
            { Transaction.Fields.Account, new("Conta", GrammaticalGender.Feminine) },
            { Transaction.Fields.Payee, new("Beneficiário", GrammaticalGender.Masculine) },
            { Transaction.Fields.Category, new("Categoria", GrammaticalGender.Feminine) },
            { Transaction.Fields.Subcategory, new("Subcategoria", GrammaticalGender.Feminine) },
        };
}