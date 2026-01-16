using FinancialKrisis.Domain.Entities;
using FinancialKrisis.Domain.Repositories;

namespace FinancialKrisis.Application.Services;

public class GetTransactionByIdService(ITransactionRepository pTransactionRepository)
{
    public async Task<Transaction?> ExecuteAsync(Guid pTransactionId)
    {
        return await pTransactionRepository.GetByIdAsync(pTransactionId);
    }
}
