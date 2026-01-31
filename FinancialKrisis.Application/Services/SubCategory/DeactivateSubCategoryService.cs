using FinancialKrisis.Domain.Entities;
using FinancialKrisis.Domain.Repositories;

namespace FinancialKrisis.Application.Services;

public class DeactivateSubcategoryService(ISubcategoryRepository pSubcategoryRepository) : DeactivateEntityService<Subcategory, ISubcategoryRepository>(pSubcategoryRepository)
{
}
