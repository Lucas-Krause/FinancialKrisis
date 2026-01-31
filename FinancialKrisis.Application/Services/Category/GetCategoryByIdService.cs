using FinancialKrisis.Domain.Entities;
using FinancialKrisis.Domain.Interfaces;

namespace FinancialKrisis.Application.Services;

public class GetCategoryByIdService(ICategoryRepository pCategoryRepository) : GetEntityEntityByIdService<Category, ICategoryRepository>(pCategoryRepository)
{
}

