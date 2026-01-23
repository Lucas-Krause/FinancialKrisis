using FinancialKrisis.Application.DTOs;
using FinancialKrisis.Application.Helpers;
using FinancialKrisis.Domain.Entities;
using FinancialKrisis.Domain.Repositories;

namespace FinancialKrisis.Application.Services;

public class CreateCategoryService(ICategoryRepository pRepository)
{
    public async Task<Category> ExecuteAsync(CreateCategoryDTO pCreateCategoryDTO)
    {
        try
        {
            var category = new Category(pCreateCategoryDTO.Name);
            await pRepository.AddAsync(category);
            return category;
        }
        catch (Exception pEx)
        {
            throw ErrorMessageResolver.Resolve(pEx);
        }
    }
}
