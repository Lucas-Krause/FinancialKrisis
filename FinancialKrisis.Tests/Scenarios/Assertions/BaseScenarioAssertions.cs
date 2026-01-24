using FinancialKrisis.Application.Enums;
using FinancialKrisis.Application.Exceptions;
using FinancialKrisis.Domain.Common;
using FinancialKrisis.Domain.Enums;
using FinancialKrisis.Domain.Exceptions;
using System.Reflection;
using Xunit.Sdk;

namespace FinancialKrisis.Tests.Scenarios.Assertions;

public static class BaseScenarioAssertions
{
    extension<TScenario, TInput, TEntity>(Scenario<TScenario, TInput, TEntity> pScenario)
        where TScenario : Scenario<TScenario, TInput, TEntity>
        where TEntity : IEntity, IActivatable
    {
        public TestContext ShouldBeInactive()
        {
            TEntity entity = pScenario.Context.GetCurrentOrThrow<TEntity>();

            if (entity.IsActive)
                throw new XunitException($"Esperava que a entidade '{typeof(TEntity).Name}' estivesse inativa, mas está ativa.");

            return pScenario.Context;
        }
    }

    extension<TScenario, TInput, TEntity>(Scenario<TScenario, TInput, TEntity> pScenario)
        where TScenario : Scenario<TScenario, TInput, TEntity>
        where TEntity : IEntity
    {
        public TestContext ShouldMatchInput()
        {
            if (pScenario.LastException is not null)
                throw pScenario.LastException;

            TInput input = pScenario.Input;
            TEntity entity = pScenario.Context.GetCurrentOrThrow<TEntity>();

            foreach (PropertyInfo inputProperty in typeof(TInput).GetProperties())
            {
                if (!inputProperty.CanRead)
                    continue;

                PropertyInfo? entityProperty = typeof(TEntity).GetProperty(inputProperty.Name);

                if (entityProperty is null || !entityProperty.CanRead)
                    continue;

                object? inputValue = inputProperty.GetValue(input);
                object? entityValue = entityProperty.GetValue(entity);

                if (!Equals(inputValue, entityValue))
                {
                    throw new XunitException(
                        $"Input não corresponde a entidade '{typeof(TEntity).Name}'{Environment.NewLine}" +
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
            Assert.Equal(pEntityType ?? typeof(TEntity), ex.EntityType);
        }

        public void ShouldFailWithDomainRuleException(DomainRuleErrorCode pErrorCode, Type? pEntityType = null)
        {
            if (pScenario.LastException is not DomainRuleException ex)
                throw new XunitException($"Expected DomainRuleException but got {pScenario.LastException}");

            Assert.NotNull(ex.Message);
            Assert.NotEqual(string.Empty, ex.Message);
            Assert.Equal(pErrorCode, ex.ErrorCode);
            Assert.Equal(pEntityType ?? typeof(TEntity), ex.EntityType);
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