using FinancialKrisis.Domain.Entities;
using FinancialKrisis.Domain.Interfaces;

namespace FinancialKrisis.Application.Services;

public class DeactivateCategoryService(ICategoryRepository pCategoryRepository) : DeactivateEntityService<Category, ICategoryRepository>(pCategoryRepository)
{
}
