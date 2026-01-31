using FinancialKrisis.Domain.Entities;
using FinancialKrisis.Domain.Repositories;

namespace FinancialKrisis.Application.Services;

public class GetAllSubcategoriesService(ISubcategoryRepository pSubcategoryRepository) : GetAllEntitiesService<Subcategory, ISubcategoryRepository>(pSubcategoryRepository)
{
}
