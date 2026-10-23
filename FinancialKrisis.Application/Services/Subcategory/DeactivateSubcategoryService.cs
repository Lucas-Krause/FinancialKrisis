using FinancialKrisis.Domain.Entities;
using FinancialKrisis.Domain.Interfaces;

namespace FinancialKrisis.Application.Services;

public class DeactivateSubcategoryService(ISubcategoryRepository pSubcategoryRepository, IUnitOfWork pUnitOfWork) : DeactivateEntityService<Subcategory, ISubcategoryRepository>(pSubcategoryRepository, pUnitOfWork)
{
}
