using FinancialKrisis.Application.DTOs;
using FinancialKrisis.Application.Services;
using FinancialKrisis.Domain.Entities;
using Microsoft.Extensions.DependencyInjection;

namespace FinancialKrisis.Tests.Scenarios;

public sealed class CategoryScenario(IServiceScope pScope, string pName, List<CreateSubcategoryDTO> pSubcategoryDTOs) : BaseScenario(pScope)
{
    private readonly CreateSubcategoryService _createSubcategory = pScope.ServiceProvider.GetRequiredService<CreateSubcategoryService>();
    private readonly GetSubcategoriesByCategoryIdService _getSubcategoriesByCategoryId = pScope.ServiceProvider.GetRequiredService<GetSubcategoriesByCategoryIdService>();
    private readonly CreateCategoryService _createCategory = pScope.ServiceProvider.GetRequiredService<CreateCategoryService>();
    private readonly UpdateCategoryService _updateCategory = pScope.ServiceProvider.GetRequiredService<UpdateCategoryService>();
    private readonly DeactivateCategoryService _deactivateCategory = pScope.ServiceProvider.GetRequiredService<DeactivateCategoryService>();
    private readonly GetCategoryByIdService _getCategoryById = pScope.ServiceProvider.GetRequiredService<GetCategoryByIdService>();

    public async Task<Category> CreateAsync()
    {
        Category category = await _createCategory.ExecuteAsync(new CreateCategoryDTO
        {
            Name = pName
        });

        foreach (CreateSubcategoryDTO subcategoryDTO in pSubcategoryDTOs)
        {
            subcategoryDTO.CategoryId = category.Id;
            await _createSubcategory.ExecuteAsync(subcategoryDTO);
        }

        return category;
    }

    public Task<Category> UpdateAsync(Guid pCategoryId)
    {
        return _updateCategory.ExecuteAsync(new UpdateCategoryDTO
        {
            Id = pCategoryId,
            Name = pName
        });
    }

    public Task DeactivateAsync(Guid pCategoryId)
    {
        return _deactivateCategory.ExecuteAsync(pCategoryId);
    }

    public Task<Category?> GetCategoryById(Guid pCategoryId)
    {
        return _getCategoryById.ExecuteAsync(pCategoryId);
    }

    public Task<IReadOnlyList<Subcategory>> GetSubcategoriesByCategoryId(Guid pCategoryId)
    {
        return _getSubcategoriesByCategoryId.ExecuteAsync(pCategoryId);
    }
}
