using FinancialKrisis.Domain.Entities;
using FinancialKrisis.Domain.Repositories;

namespace FinancialKrisis.Application.Services;

public class GetCategoryByIdService(ICategoryRepository pCategoryRepository) : GetEntityEntityByIdService<Category, ICategoryRepository>(pCategoryRepository)
{
}

