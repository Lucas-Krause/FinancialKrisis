using FinancialKrisis.Application.Enums;
using FinancialKrisis.Application.Exceptions;
using FinancialKrisis.Domain.Enums;
using FinancialKrisis.Domain.Exceptions;
using System.Reflection;
using Xunit.Sdk;

namespace FinancialKrisis.Tests.Scenarios.Assertions;

public static class BaseScenarioAssertions
{
    extension<TScenario, TInput>(Scenario<TScenario, TInput> pScenario)
        where TScenario : Scenario<TScenario, TInput>
        where TInput : new()
    {
        public TestContext ShouldBeInactive()
        {
            Type entityType = pScenario.EntityType;

            object entity = pScenario.Context.GetCurrentOrThrow(entityType);

            PropertyInfo? isActiveProperty = entityType.GetProperty("IsActive");

            if (isActiveProperty is null || isActiveProperty.PropertyType != typeof(bool))
            {
                throw new XunitException($"A entidade '{entityType.Name}' não possui a propriedade 'IsActive' do tipo booleano.");
            }

            bool isActive = (bool)isActiveProperty.GetValue(entity)!;

            if (isActive)
                throw new XunitException($"Esperava que a entidade '{entityType.Name}' estivesse inativa, mas está ativa.");

            return pScenario.Context;
        }

        public TestContext ShouldMatchInput()
        {
            if (pScenario.LastException is not null)
                throw pScenario.LastException;

            TInput input = pScenario.Input;
            Type entityType = pScenario.EntityType;

            if (!pScenario.Context.TryGetCurrent(entityType, out object? entity))
                throw new XunitException(
                    $"Nenhuma entidade do tipo '{entityType.Name}' foi encontrada no TestContext. " +
                    $"Esqueceu de chamar Create() ou AsCurrent{entityType.Name}() ou WithCurrent{entityType.Name}()?");

            foreach (PropertyInfo inputProperty in typeof(TInput).GetProperties())
            {
                if (!inputProperty.CanRead)
                    continue;

                PropertyInfo? entityProperty = entityType.GetProperty(inputProperty.Name);

                if (entityProperty is null || !entityProperty.CanRead)
                    continue;

                object? inputValue = inputProperty.GetValue(input);
                object? entityValue = entityProperty.GetValue(entity);

                if (!Equals(inputValue, entityValue))
                {
                    throw new XunitException(
                        $"Input não corresponde a entidade '{entityType.Name}'{Environment.NewLine}" +
                        $"Propriedade: {inputProperty.Name}{Environment.NewLine}" +
                        $"Valor do Input: {FormatValue(inputValue)}{Environment.NewLine}" +
                        $"Valor da Entidade: {FormatValue(entityValue)}");
                }
            }

            return pScenario.Context;
        }

        public void ShouldFailWithApplicationRuleException(ApplicationRuleErrorCode pErrorCode, Type? pEntityType = null)
        {
            if (pScenario.LastException is not ApplicationRuleException ex)
                throw new XunitException($"Expected ApplicationRuleException but got {pScenario.LastException}");

            Assert.NotNull(ex.Message);
            Assert.NotEqual(string.Empty, ex.Message);
            Assert.Equal(pErrorCode, ex.ErrorCode);
            Assert.Equal(pEntityType ?? pScenario.EntityType, ex.EntityType);
        }

        public void ShouldFailWithDomainRuleException(DomainRuleErrorCode pErrorCode, Type? pEntityType = null)
        {
            if (pScenario.LastException is not DomainRuleException ex)
                throw new XunitException($"Expected DomainRuleException but got {pScenario.LastException}");

            Assert.NotNull(ex.Message);
            Assert.NotEqual(string.Empty, ex.Message);
            Assert.Equal(pErrorCode, ex.ErrorCode);
            Assert.Equal(pEntityType ?? pScenario.EntityType, ex.EntityType);
        }
    }

    private static string FormatValue(object? value)
    {
        if (value is null)
            return "<null>";

        if (value is DateTime dateTime)
            return dateTime.ToString("O");

        return value.ToString()!;
    }
}