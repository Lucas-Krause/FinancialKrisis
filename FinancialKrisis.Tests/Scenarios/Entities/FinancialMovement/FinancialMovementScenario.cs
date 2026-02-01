using FinancialKrisis.Application.DTOs;
using FinancialKrisis.Domain.Entities;
using FinancialKrisis.Tests.Scenarios.Interfaces;

namespace FinancialKrisis.Tests.Scenarios.Entities;

public partial class FinancialMovementScenario<TScenario, TCreateDTO, TUpdateDTO, TMovement> : Scenario<TScenario, TCreateDTO, TUpdateDTO, TMovement>, IFinancialMovementScenario, IScenario
    where TScenario : FinancialMovementScenario<TScenario, TCreateDTO, TUpdateDTO, TMovement>
    where TCreateDTO : CreateFinancialMovementDTO
    where TUpdateDTO : UpdateFinancialMovementDTO
    where TMovement : FinancialMovement
{
    public FinancialMovementScenario(TestContext pContext) : base(pContext)
    {
        CreateInput.Identifier = "FM1";
        CreateInput.Memo = "Test FinancialMovement";
        CreateInput.Amount = 250m;
    }

    public TScenario CreatingWithCurrentAccount()
    {
        CreateInput.AccountId = Context.GetCurrentOrThrow<Account>().Id;
        return (TScenario)this;
    }

    public TScenario CreatingWithCurrentPayee()
    {
        CreateInput.PayeeId = Context.GetCurrentOrThrow<Payee>().Id;
        return (TScenario)this;
    }

    public TScenario CreatingWithCurrentCategory()
    {
        CreateInput.CategoryId = Context.GetCurrentOrThrow<Category>().Id;
        return (TScenario)this;
    }

    public TScenario CreatingWithCurrentSubcategory()
    {
        CreateInput.SubcategoryId = Context.GetCurrentOrThrow<Subcategory>().Id;
        return (TScenario)this;
    }

    public TScenario UpdatingWithCurrentPayee()
    {
        UpdateInput.PayeeId = Context.GetCurrentOrThrow<Payee>().Id;
        return (TScenario)this;
    }

    public TScenario UpdatingWithCurrentCategory()
    {
        UpdateInput.CategoryId = Context.GetCurrentOrThrow<Category>().Id;
        return (TScenario)this;
    }

    public TScenario UpdatingWithCurrentSubcategory()
    {
        UpdateInput.SubcategoryId = Context.GetCurrentOrThrow<Subcategory>().Id;
        return (TScenario)this;
    }
}
