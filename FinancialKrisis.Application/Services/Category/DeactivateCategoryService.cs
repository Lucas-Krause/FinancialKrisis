using FinancialKrisis.Domain.Entities;
using FinancialKrisis.Domain.Interfaces;

namespace FinancialKrisis.Application.Services;

public class DeactivateCategoryService(ICategoryRepository pCategoryRepository, IUnitOfWork pUnitOfWork) : DeactivateEntityService<Category, ICategoryRepository>(pCategoryRepository, pUnitOfWork)
{
}
