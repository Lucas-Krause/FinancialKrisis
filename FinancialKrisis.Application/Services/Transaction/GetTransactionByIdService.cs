using FinancialKrisis.Application.Helpers;
using FinancialKrisis.Domain.Entities;
using FinancialKrisis.Domain.Repositories;

namespace FinancialKrisis.Application.Services;

public class GetTransactionByIdService(ITransactionRepository pTransactionRepository)
{
    public async Task<Transaction?> ExecuteAsync(Guid pTransactionId)
    {
        try
        {
            return await pTransactionRepository.GetByIdAsync(pTransactionId);
        }
        catch (Exception pEx)
        {
            throw ErrorMessageResolver.Resolve(pEx);
        }
    }
}
