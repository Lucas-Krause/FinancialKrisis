using FinancialKrisis.Domain.Entities;
using FinancialKrisis.Domain.Interfaces;
using FinancialKrisis.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace FinancialKrisis.Infrastructure.Repositories;

public class PayeeRepository(FinancialKrisisDbContext pContext) : BaseRepository<Payee>(pContext), IPayeeRepository
{
    public async Task<bool> ExistsByNameAsync(string pName, Guid? pIgnoredId = null)
    {
        string normalizedName = pName.Trim().ToLower();
        return await _dbSet.AnyAsync(p => p.Name.ToLower() == normalizedName && (pIgnoredId == null || p.Id != pIgnoredId.Value));
    }
}
