using FinancialKrisis.Domain.Entities;
using FinancialKrisis.Domain.Repositories;
using FinancialKrisis.Infrastructure.Persistence;

namespace FinancialKrisis.Infrastructure.Repositories;

public class CategoryRepository(FinancialKrisisDbContext context) : BaseRepository<Category>(context), ICategoryRepository
{
}
