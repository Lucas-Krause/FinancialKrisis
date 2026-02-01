using FinancialKrisis.Application.DTOs;
using FinancialKrisis.Domain.Interfaces;

namespace FinancialKrisis.Tests.Scenarios.Interfaces;

public interface IScenario
{
    TestContext Context { get; }
    ICreateDTO? CreateInput { get; }
    IUpdateDTO? UpdateInput { get; }
    Exception? LastException { get; }

    IEntity? Entity { get; }
}
