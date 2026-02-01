using FinancialKrisis.Application.DTOs;
using FinancialKrisis.Tests.Scenarios.Interfaces;

namespace FinancialKrisis.Tests.Scenarios.Entities;

public partial class FinancialMovementScenario<TScenario, TCreateDTO, TUpdateDTO, TMovement> : IFinancialMovementScenario
{
    IFinancialMovementScenario IFinancialMovementScenario.CreatingWith(Action<CreateFinancialMovementDTO> pConfigure)
    {
        return CreatingWith(pConfigure);
    }
    IFinancialMovementScenario IFinancialMovementScenario.CreatingWithCurrentAccount()
    {
        return CreatingWithCurrentAccount();
    }

    IFinancialMovementScenario IFinancialMovementScenario.CreatingWithCurrentPayee()
    {
        return CreatingWithCurrentPayee();
    }

    IFinancialMovementScenario IFinancialMovementScenario.CreatingWithCurrentCategory()
    {
        return CreatingWithCurrentCategory();
    }

    IFinancialMovementScenario IFinancialMovementScenario.CreatingWithCurrentSubcategory()
    {
        return CreatingWithCurrentSubcategory();
    }

    IFinancialMovementScenario IFinancialMovementScenario.UpdatingWith(Action<UpdateFinancialMovementDTO> pConfigure)
    {
        return UpdatingWith(pConfigure);
    }

    IFinancialMovementScenario IFinancialMovementScenario.UpdatingWithCurrentPayee()
    {
        return UpdatingWithCurrentPayee();
    }

    IFinancialMovementScenario IFinancialMovementScenario.UpdatingWithCurrentCategory()
    {
        return UpdatingWithCurrentCategory();
    }

    IFinancialMovementScenario IFinancialMovementScenario.UpdatingWithCurrentSubcategory()
    {
        return UpdatingWithCurrentSubcategory();
    }

    IFinancialMovementScenario IFinancialMovementScenario.AsCurrent()
    {
        return AsCurrent();
    }

    IFinancialMovementScenario IFinancialMovementScenario.Create()
    {
        return Create();
    }

    IFinancialMovementScenario IFinancialMovementScenario.Update()
    {
        return Update();
    }
}
