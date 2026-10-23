using FinancialKrisis.Domain.Entities;
using FinancialKrisis.Domain.Interfaces;

namespace FinancialKrisis.Application.Services;

public class ReactivateCategoryService(ICategoryRepository pCategoryRepository, IUnitOfWork pUnitOfWork) : ReactivateEntityService<Category, ICategoryRepository>(pCategoryRepository, pUnitOfWork)
{
}
