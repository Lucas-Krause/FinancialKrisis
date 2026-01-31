using FinancialKrisis.Domain.Entities;
using FinancialKrisis.Domain.Interfaces;

namespace FinancialKrisis.Application.Services;

public class GetAllCategoriesService(ICategoryRepository pCategoryRepository) : GetAllEntitiesService<Category, ICategoryRepository>(pCategoryRepository)
{
}

