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
        GrammarMetadata typeMetadata = TypeCatalog.Types[pDomainException.Type];
        GrammarMetadata fieldMetadata = new("campo", GrammaticalGender.Masculine);

        string ownedText = string.Empty;

        if (pDomainException.OwnerType is not null)
        {
            GrammarMetadata ownerTypeMetadata = TypeCatalog.Types[pDomainException.OwnerType];
            ownedText = $" d{ownerTypeMetadata.Gender.ToPtArticle()} {ownerTypeMetadata.NamePt.ToLower()}";
        }

        string fieldArticle = string.Empty;

        if (pDomainException.Field is not null)
        {
            fieldMetadata = ResolveField(pDomainException.Type, pDomainException.Field);
            fieldArticle = fieldMetadata.Gender.ToPtArticle();
        }

        string typeArticle = typeMetadata.Gender.ToPtArticle();

        return pDomainException.ErrorCode switch
        {
            DomainRuleErrorCode.RequiredField =>
                $"{fieldArticle.ToUpper()} {fieldMetadata.NamePt.ToLower()} d{typeArticle} {typeMetadata.NamePt.ToLower()}{ownedText} é obrigatóri{fieldArticle}.",

            DomainRuleErrorCode.NegativeValue =>
                $"{fieldArticle.ToUpper()} {fieldMetadata.NamePt.ToLower()} d{typeArticle} {typeMetadata.NamePt.ToLower()}{ownedText} não pode ser negativ{fieldArticle}.",

            DomainRuleErrorCode.ValueMustBePositive =>
                $"{fieldArticle.ToUpper()} {fieldMetadata.NamePt.ToLower()} d{typeArticle} {typeMetadata.NamePt.ToLower()}{ownedText} deve ser maior que zero.",

            DomainRuleErrorCode.EntityNotFound =>
                $"{typeArticle.ToUpper()} {typeMetadata.NamePt.ToLower()} não foi encontrad{typeArticle}.",

            DomainRuleErrorCode.WeeklyScheduleRequiresDaysOfWeek =>
                $"{fieldArticle.ToUpper()} {fieldMetadata.NamePt.ToLower()} por semana d{typeArticle} {typeMetadata.NamePt.ToLower()}{ownedText} requer pelo menos um dia da semana configurado.",

            DomainRuleErrorCode.MonthlyScheduleRequiresValidDayOfMonth =>
                $"{fieldArticle.ToUpper()} {fieldMetadata.NamePt.ToLower()} por mês d{typeArticle} {typeMetadata.NamePt.ToLower()}{ownedText} requer um dia do mês válido (1-31).",

            _ => "Erro de validação."
        };
    }

    private static string GetApplicationRuleExceptionMessage(ApplicationRuleException pApplicationRuleException)
    {
        GrammarMetadata typeMetadata = TypeCatalog.Types[pApplicationRuleException.Type];
        GrammarMetadata fieldMetadata = new("campo", GrammaticalGender.Masculine);

        string ownedText = string.Empty;

        if (pApplicationRuleException.OwnerType is not null)
        {
            GrammarMetadata ownerTypeMetadata = TypeCatalog.Types[pApplicationRuleException.OwnerType];
            ownedText = $" d{ownerTypeMetadata.Gender.ToPtArticle()} {ownerTypeMetadata.NamePt.ToLower()}";
        }

        if (pApplicationRuleException.Field is not null)
            fieldMetadata = ResolveField(pApplicationRuleException.Type, pApplicationRuleException.Field);

        string typeArticle = typeMetadata.Gender.ToPtArticle();

        return pApplicationRuleException.ErrorCode switch
        {
            ApplicationRuleErrorCode.SubcategoryDoesNotBelongToCategory =>
                $"{typeArticle.ToUpper()} {typeMetadata.NamePt.ToLower()} não pertence à {fieldMetadata.NamePt.ToLower()}{ownedText}.",

            ApplicationRuleErrorCode.EntityInactive =>
                $"{typeArticle.ToUpper()} {typeMetadata.NamePt.ToLower()} não está ativ{typeArticle}.",

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
