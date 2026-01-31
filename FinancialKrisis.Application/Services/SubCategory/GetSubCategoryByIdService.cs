using FinancialKrisis.Domain.Entities;
using FinancialKrisis.Domain.Interfaces;

namespace FinancialKrisis.Application.Services;

public class GetSubcategoryByIdService(ISubcategoryRepository pSubcategoryRepository) : GetEntityEntityByIdService<Subcategory, ISubcategoryRepository>(pSubcategoryRepository)
{
}
