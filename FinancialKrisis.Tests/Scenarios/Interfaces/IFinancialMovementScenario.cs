using FinancialKrisis.Application.DTOs;

namespace FinancialKrisis.Tests.Scenarios.Interfaces;

public interface IFinancialMovementScenario : IScenario
{
    IFinancialMovementScenario CreatingWith(Action<CreateFinancialMovementDTO> pConfigure);
    IFinancialMovementScenario CreatingWithCurrentAccount();
    IFinancialMovementScenario CreatingWithCurrentPayee();
    IFinancialMovementScenario CreatingWithCurrentCategory();
    IFinancialMovementScenario CreatingWithCurrentSubcategory();

    IFinancialMovementScenario UpdatingWith(Action<UpdateFinancialMovementDTO> pConfigure);
    IFinancialMovementScenario UpdatingWithCurrentPayee();
    IFinancialMovementScenario UpdatingWithCurrentCategory();
    IFinancialMovementScenario UpdatingWithCurrentSubcategory();

    IFinancialMovementScenario AsCurrent();
    IFinancialMovementScenario Create();
    IFinancialMovementScenario Update();
}
