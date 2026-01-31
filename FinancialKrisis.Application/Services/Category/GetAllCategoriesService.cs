using FinancialKrisis.Domain.Entities;
using FinancialKrisis.Domain.Repositories;

namespace FinancialKrisis.Application.Services;

public class GetAllCategoriesService(ICategoryRepository pCategoryRepository) : GetAllEntitiesService<Category, ICategoryRepository>(pCategoryRepository)
{
}

