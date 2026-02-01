using FinancialKrisis.Application.DTOs;
using FinancialKrisis.Domain.Interfaces;
using FinancialKrisis.Tests.Scenarios.Interfaces;

namespace FinancialKrisis.Tests.Scenarios;

public abstract partial class Scenario<TScenario, TCreateInput, TUpdateInput, TEntity> : IScenario
{
    ICreateDTO? IScenario.CreateInput => CreateInput;
    IUpdateDTO? IScenario.UpdateInput => UpdateInput;
    Exception? IScenario.LastException => LastException;

    IEntity? IScenario.Entity => Entity;
}
