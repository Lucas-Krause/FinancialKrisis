using FinancialKrisis.Domain.Entities;
using FinancialKrisis.Domain.Repositories;

namespace FinancialKrisis.Application.Services;

public class GetSubcategoryByIdService(ISubcategoryRepository pSubcategoryRepository) : GetEntityEntityByIdService<Subcategory, ISubcategoryRepository>(pSubcategoryRepository)
{
}
