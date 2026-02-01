using FinancialKrisis.Application.Enums;
using FinancialKrisis.Application.Exceptions;
using FinancialKrisis.Application.Extensions;
using FinancialKrisis.Application.Metadata;
using FinancialKrisis.Common.Records;
using FinancialKrisis.Domain.Entities;
using FinancialKrisis.Domain.Enums;
using FinancialKrisis.Domain.Exceptions;
using FinancialKrisis.Domain.ValueObjects;

namespace FinancialKrisis.Application.Helpers;

public static class ErrorMessageResolver
{
    public static Exception Resolve(Exception pException)
    {
        return pException switch
        {
            DomainRuleException domainException => new DomainRuleException(domainException, GetDomainExceptionMessage(domainException)),
            ApplicationRuleException applicationRuleException => new ApplicationRuleException(applicationRuleException, GetApplicationRuleExceptionMessage(applicationRuleException)),
            _ => new Exception("Ocorreu um erro inesperado: " + pException.Message)
        };
    }

    private static string GetDomainExceptionMessage(DomainRuleException pDomainException)
    {
        GrammarMetadata entityMetadata = TypeCatalog.Types[pDomainException.EntityType];
        GrammarMetadata fieldMetadata = new("campo", GrammaticalGender.Masculine);

        string fieldArticle = string.Empty;

        if (pDomainException.Field is not null)
        {
            fieldMetadata = ResolveField(pDomainException.EntityType, pDomainException.Field);
            fieldArticle = fieldMetadata.Gender.ToPtArticle();
        }

        string typeArticle = entityMetadata.Gender.ToPtArticle();

        return pDomainException.ErrorCode switch
        {
            DomainRuleErrorCode.RequiredField =>
                $"{fieldArticle.ToUpper()} {fieldMetadata.NamePt.ToLower()} d{typeArticle} {entityMetadata.NamePt.ToLower()} é obrigatóri{fieldArticle}.",

            DomainRuleErrorCode.NegativeValue =>
                $"{fieldArticle.ToUpper()} {fieldMetadata.NamePt.ToLower()} d{typeArticle} {entityMetadata.NamePt.ToLower()} não pode ser negativ{fieldArticle}.",

            DomainRuleErrorCode.EntityNotFound =>
                $"{typeArticle.ToUpper()} {entityMetadata.NamePt.ToLower()} não foi encontrad{typeArticle}.",

            _ => "Erro de validação."
        };
    }

    private static string GetApplicationRuleExceptionMessage(ApplicationRuleException pApplicationRuleException)
    {
        GrammarMetadata entityMetadata = TypeCatalog.Types[pApplicationRuleException.EntityType];
        GrammarMetadata fieldMetadata = new("campo", GrammaticalGender.Masculine);
        GrammarMetadata transactionMetadata = TypeCatalog.Types[typeof(Transaction)];

        if (pApplicationRuleException.Field is not null)
            fieldMetadata = ResolveField(pApplicationRuleException.EntityType, pApplicationRuleException.Field);

        string typeArticle = entityMetadata.Gender.ToPtArticle();

        return pApplicationRuleException.ErrorCode switch
        {
            ApplicationRuleErrorCode.SubcategoryDoesNotBelongToCategory =>
                $"{typeArticle.ToUpper()} {entityMetadata.NamePt.ToLower()} " +
                $"não pertence à {fieldMetadata.NamePt.ToLower()} d{transactionMetadata.Gender.ToPtArticle()} {transactionMetadata.NamePt.ToLower()}.",

            ApplicationRuleErrorCode.EntityInactive =>
                $"{typeArticle.ToUpper()} {entityMetadata.NamePt.ToLower()} não está ativ{typeArticle}.",

            _ => "Erro de validação."
        };
    }

    private static GrammarMetadata ResolveField(Type pType, FieldKey pFieldKey)
    {
        return pType switch
        {
            _ when pType == typeof(Payee) => PayeeFieldCatalog.Fields[pFieldKey],
            _ when pType == typeof(Account) => AccountFieldCatalog.Fields[pFieldKey],
            _ when pType == typeof(Category) => CategoryFieldCatalog.Fields[pFieldKey],
            _ when pType == typeof(Subcategory) => SubcategoryFieldCatalog.Fields[pFieldKey],
            _ when pType == typeof(Transaction) => TransactionFieldCatalog.Fields[pFieldKey],
            _ when pType == typeof(PlannedTransaction) => PlannedTransactionFieldCatalog.Fields[pFieldKey],
            _ when pType == typeof(Schedule) => ScheduleFieldCatalog.Fields[pFieldKey],
            _ => throw new InvalidOperationException($"O catálogo de campos do tipo {pType.Name} não está registrado.")
        };
    }
}
