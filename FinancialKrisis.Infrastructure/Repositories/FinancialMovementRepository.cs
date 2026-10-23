using FinancialKrisis.Domain.Entities;
using FinancialKrisis.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace FinancialKrisis.Infrastructure.Repositories;

public class FinancialMovementRepository<TMovement>(FinancialKrisisDbContext pContext) : BaseRepository<TMovement>(pContext)
    where TMovement : FinancialMovement
{
    public override async Task<TMovement?> GetByIdAsync(Guid pId)
    {
        return await QueryWithNavigations().FirstOrDefaultAsync(m => m.Id == pId);
    }

    public override async Task<IReadOnlyList<TMovement>> GetAllAsync()
    {
        return await QueryWithNavigations().AsNoTracking().ToListAsync();
    }

    private IQueryable<TMovement> QueryWithNavigations()
    {
        return _dbSet.Include(m => m.Account)
                     .Include(m => m.Payee)
                     .Include(m => m.Category)
                     .Include(m => m.Subcategory);
    }
}
