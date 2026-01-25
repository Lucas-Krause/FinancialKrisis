using FinancialKrisis.Application.Enums;
using FinancialKrisis.Application.Exceptions;
using FinancialKrisis.Application.Metadata;
using FinancialKrisis.Common.Records;
using FinancialKrisis.Domain.Entities;
using FinancialKrisis.Domain.Enums;
using FinancialKrisis.Domain.Exceptions;

namespace FinancialKrisis.Application.Helpers;

public static class ErrorMessageResolver
{
    public static Exception Resolve(Exception pException)
    {
        return pException switch
        {
            DomainRuleException domainException => new DomainRuleException(domainException, GetDomainExceptionMessage(domainException)),
            ApplicationRuleException applicationRuleException => new ApplicationRuleException(applicationRuleException, GetApplicationRuleExceptionMessage(applicationRuleException)),
            _ => new Exception("Ocorreu um erro inesperado.")
        };
    }

    private static string GetDomainExceptionMessage(DomainRuleException pDomainException)
    {
        EntityMetadata entityMetadata = EntityCatalog.Entities[pDomainException.EntityType];
        FieldMetadata fieldMetadata = new("campo", GrammaticalGender.Masculine);

        string articleField = string.Empty;

        if (pDomainException.Field is not null)
        {
            fieldMetadata = ResolveField(pDomainException.EntityType, pDomainException.Field);
            articleField = fieldMetadata.Gender == GrammaticalGender.Feminine ? "a" : "o";
        }

        string articleEntity = entityMetadata.Gender == GrammaticalGender.Feminine ? "a" : "o";

        return pDomainException.ErrorCode switch
        {
            DomainRuleErrorCode.RequiredField =>
                $"{articleField.ToUpper()} {fieldMetadata.NamePt.ToLower()} d{articleEntity} {entityMetadata.NamePt.ToLower()} é obrigatóri{articleField}.",

            DomainRuleErrorCode.NegativeValue =>
                $"{articleField.ToUpper()} {fieldMetadata.NamePt.ToLower()} d{articleEntity} {entityMetadata.NamePt.ToLower()} não pode ser negativ{articleField}.",

            DomainRuleErrorCode.EntityNotFound =>
                $"{articleEntity.ToUpper()} {entityMetadata.NamePt.ToLower()} não foi encontrad{articleEntity}.",

            _ => "Erro de validação."
        };
    }

    private static string GetApplicationRuleExceptionMessage(ApplicationRuleException pApplicationRuleException)
    {
        EntityMetadata entityMetadata = EntityCatalog.Entities[pApplicationRuleException.EntityType];
        EntityMetadata transactionMetadata = EntityCatalog.Entities[typeof(Transaction)];
        FieldMetadata fieldMetadata = new("campo", GrammaticalGender.Masculine);

        string articleField = string.Empty;

        if (pApplicationRuleException.Field is not null)
        {
            fieldMetadata = ResolveField(pApplicationRuleException.EntityType, pApplicationRuleException.Field);
            articleField = fieldMetadata.Gender == GrammaticalGender.Feminine ? "a" : "o";
        }

        string articleEntity = entityMetadata.Gender == GrammaticalGender.Feminine ? "a" : "o";
        string transactionArticle = transactionMetadata.Gender == GrammaticalGender.Feminine ? "a" : "o";

        return pApplicationRuleException.ErrorCode switch
        {
            ApplicationRuleErrorCode.SubcategoryDoesNotBelongToCategory =>
                $"{articleEntity.ToUpper()} {entityMetadata.NamePt.ToLower()} não pertence à {fieldMetadata.NamePt.ToLower()} d{transactionArticle} {transactionMetadata.NamePt.ToLower()}.",

            ApplicationRuleErrorCode.EntityInactive =>
                $"{articleEntity.ToUpper()} {entityMetadata.NamePt.ToLower()} não está ativ{articleEntity}.",

            _ => "Erro de validação."
        };
    }

    private static FieldMetadata ResolveField(Type pEntityType, FieldKey pFieldKey)
    {
        return pEntityType switch
        {
            _ when pEntityType == typeof(Payee) => PayeeFieldCatalog.Fields[pFieldKey],
            _ when pEntityType == typeof(Account) => AccountFieldCatalog.Fields[pFieldKey],
            _ when pEntityType == typeof(Category) => CategoryFieldCatalog.Fields[pFieldKey],
            _ when pEntityType == typeof(Subcategory) => SubcategoryFieldCatalog.Fields[pFieldKey],
            _ when pEntityType == typeof(Transaction) => TransactionFieldCatalog.Fields[pFieldKey],
            _ => throw new InvalidOperationException("Field metadata not registered.")
        };
    }
}
