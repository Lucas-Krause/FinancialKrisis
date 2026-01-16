using FinancialKrisis.Application.DTOs;
using FinancialKrisis.Domain.Entities;
using FinancialKrisis.Domain.Repositories;

namespace FinancialKrisis.Application.Services;

public class CreateCategoryService(ICategoryRepository pRepository)
{
    public async Task<Category> ExecuteAsync(CreateCategoryDTO pCreateCategoryDTO)
    {
        var category = new Category(pCreateCategoryDTO.Name);
        await pRepository.AddAsync(category);
        return category;
    }
}
