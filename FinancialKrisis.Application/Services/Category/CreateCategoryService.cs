using FinancialKrisis.Application.DTOs;
using FinancialKrisis.Domain.Entities;
using FinancialKrisis.Domain.Interfaces;

namespace FinancialKrisis.Application.Services;

public class CreateCategoryService(ICategoryRepository pCategoryRepository) : CreateEntityService<Category, ICategoryRepository, CreateCategoryDTO>(pCategoryRepository)
{
    protected override async Task<Category> CreateEntity(CreateCategoryDTO pCreateDTO)
    {
        return new Category(pCreateDTO.Name);
    }
}