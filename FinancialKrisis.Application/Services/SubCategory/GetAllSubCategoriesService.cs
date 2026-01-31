using FinancialKrisis.Domain.Entities;
using FinancialKrisis.Domain.Interfaces;

namespace FinancialKrisis.Application.Services;

public class GetAllSubcategoriesService(ISubcategoryRepository pSubcategoryRepository) : GetAllEntitiesService<Subcategory, ISubcategoryRepository>(pSubcategoryRepository)
{
}
