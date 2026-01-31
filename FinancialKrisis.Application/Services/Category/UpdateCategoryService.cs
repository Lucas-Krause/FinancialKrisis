using FinancialKrisis.Application.DTOs;
using FinancialKrisis.Domain.Entities;
using FinancialKrisis.Domain.Interfaces;

namespace FinancialKrisis.Application.Services;

public class UpdateCategoryService(ICategoryRepository pCategoryRepository) : UpdateEntityService<Category, ICategoryRepository, UpdateCategoryDTO>(pCategoryRepository)
{
    protected override async Task ApplyChangesToEntity(Category pCategory, UpdateCategoryDTO pUpdateDTO)
    {
        if (pUpdateDTO.Name.IsDefined)
            pCategory.ChangeName(pUpdateDTO.Name.Value!);
    }
}