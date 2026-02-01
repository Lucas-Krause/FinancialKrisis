using FinancialKrisis.Application.DTOs;
using FinancialKrisis.Application.Enums;
using FinancialKrisis.Application.Exceptions;
using FinancialKrisis.Common.Exceptions;
using FinancialKrisis.Common.Records;
using FinancialKrisis.Domain.Enums;
using FinancialKrisis.Domain.Exceptions;
using FinancialKrisis.Domain.Interfaces;
using FinancialKrisis.Tests.Scenarios.Interfaces;
using System.Reflection;
using Xunit.Sdk;

namespace FinancialKrisis.Tests.Scenarios.Assertions;

public static class BaseScenarioAssertions
{
    extension(IScenario pScenario)
    {
        public TestContext ShouldCreateSuccessfully()
        {
            if (pScenario.LastException is not null)
                throw pScenario.LastException;

            InputShouldMatchEntity(pScenario.CreateInput, pScenario.Entity);

            return pScenario.Context;
        }

        public TestContext ShouldUpdateSuccessfully()
        {
            if (pScenario.LastException is not null)
                throw pScenario.LastException;

            InputShouldMatchEntity(pScenario.UpdateInput, pScenario.Entity);

            return pScenario.Context;
        }

        public void ShouldFailWithApplicationRuleException(ApplicationRuleErrorCode pErrorCode, Type pEntityType, FieldKey? pFieldKey = null)
        {
            ShouldFailWithRuleException<ApplicationRuleException, ApplicationRuleErrorCode>(pScenario.LastException, pErrorCode, pEntityType, pFieldKey);
        }

        public void ShouldFailWithDomainRuleException(DomainRuleErrorCode pErrorCode, Type pEntityType, FieldKey? pFieldKey = null)
        {
            ShouldFailWithRuleException<DomainRuleException, DomainRuleErrorCode>(pScenario.LastException, pErrorCode, pEntityType, pFieldKey);
        }
    }

    extension<TScenario, TCreateInput, TUpdateInput, TEntity>(Scenario<TScenario, TCreateInput, TUpdateInput, TEntity> pScenario)
        where TScenario : Scenario<TScenario, TCreateInput, TUpdateInput, TEntity>
        where TEntity : IEntity, IActivatable
        where TCreateInput : ICreateDTO
        where TUpdateInput : IUpdateDTO
    {
        public TestContext ShouldDeactivateSuccessfully()
        {
            return ShouldBeInactive(pScenario);
        }

        public TestContext ShouldBeInactive()
        {
            TEntity entity = pScenario.Context.GetCurrentOrThrow<TEntity>();

            if (entity.IsActive)
                throw new XunitException($"Esperava que a entidade '{typeof(TEntity).Name}' estivesse inativa, mas está ativa.");

            return pScenario.Context;
        }
    }

    private static void ShouldFailWithRuleException<TException, TErrorCode>(Exception? pLastException, TErrorCode pErrorCode, Type pEntityType, FieldKey? pFieldKey = null)
        where TException : RuleException<TErrorCode>
        where TErrorCode : Enum
    {
        if (pLastException is null)
            throw new XunitException("Esperava exceção, mas deu sucesso!");

        if (pLastException is not TException ex)
            throw new XunitException($"Esperava exceção do tipo '{typeof(TException).Name}', mas veio '{pLastException.GetType().Name}'");

        Assert.NotNull(ex.Message);
        Assert.NotEqual(string.Empty, ex.Message);
        Assert.Equal(pErrorCode, ex.ErrorCode);
        Assert.Equal(pEntityType, ex.EntityType);
        Assert.Equal(pFieldKey, ex.Field);
    }

    private static void InputShouldMatchEntity<TInput, TEntity>(TInput pInput, TEntity pEntity)
    {
        foreach (PropertyInfo inputProperty in typeof(TInput).GetProperties())
        {
            object? expectedValue;

            if (!inputProperty.CanRead)
                continue;

            Type propertyType = inputProperty.PropertyType;
            PropertyInfo? entityProperty = typeof(TEntity).GetProperty(inputProperty.Name);

            if (entityProperty is null || !entityProperty.CanRead)
                continue;

            expectedValue = inputProperty.GetValue(pInput);
            object? entityValue = entityProperty.GetValue(pEntity);

            if (propertyType.IsGenericType && propertyType.GetGenericTypeDefinition() == typeof(Optional<>))
            {
                PropertyInfo isDefinedProperty = propertyType.GetProperty(nameof(Optional<>.IsDefined))!;
                PropertyInfo valueProperty = propertyType.GetProperty(nameof(Optional<>.Value))!;

                bool isDefined = (bool)isDefinedProperty.GetValue(expectedValue)!;

                if (!isDefined)
                    continue;

                expectedValue = valueProperty.GetValue(expectedValue);
            }

            if (expectedValue is Guid guid && guid == Guid.Empty && entityValue is null)
                continue;

            if (!Equals(expectedValue, entityValue))
            {
                throw new XunitException(
                    $"Input não corresponde a entidade '{typeof(TEntity).Name}'{Environment.NewLine}" +
                    $"Propriedade: {inputProperty.Name}{Environment.NewLine}" +
                    $"Valor do Input: {FormatValue(expectedValue)}{Environment.NewLine}" +
                    $"Valor da Entidade: {FormatValue(entityValue)}");
            }
        }
    }

    private static string FormatValue(object? pValue)
    {
        if (pValue is null)
            return "<null>";

        if (pValue is DateTime dateTime)
            return dateTime.ToString("O");

        return pValue.ToString()!;
    }
}