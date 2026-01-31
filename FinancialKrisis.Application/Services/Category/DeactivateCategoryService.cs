using FinancialKrisis.Domain.Entities;
using FinancialKrisis.Domain.Repositories;

namespace FinancialKrisis.Application.Services;

public class DeactivateCategoryService(ICategoryRepository pCategoryRepository) : DeactivateEntityService<Category, ICategoryRepository>(pCategoryRepository)
{
}
