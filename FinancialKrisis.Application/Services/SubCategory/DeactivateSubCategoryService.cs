using FinancialKrisis.Domain.Entities;
using FinancialKrisis.Domain.Interfaces;

namespace FinancialKrisis.Application.Services;

public class DeactivateSubcategoryService(ISubcategoryRepository pSubcategoryRepository) : DeactivateEntityService<Subcategory, ISubcategoryRepository>(pSubcategoryRepository)
{
}
