using FinancialKrisis.Domain.Entities;
using FinancialKrisis.Domain.Interfaces;

namespace FinancialKrisis.Application.Services;

public class ReactivateCategoryService(ICategoryRepository pCategoryRepository) : ReactivateEntityService<Category, ICategoryRepository>(pCategoryRepository)
{
}
