using FinancialKrisis.Application.Helpers;
using FinancialKrisis.Domain.Repositories;

namespace FinancialKrisis.Application.Services;

public class DeleteTransactionService(ITransactionRepository pTransactionRepository)
{
    public async Task ExecuteAsync(Guid pTransactionId)
    {
        try
        {
            await pTransactionRepository.DeleteAsync(pTransactionId);
        }
        catch (Exception pEx)
        {
            throw ErrorMessageResolver.Resolve(pEx);
        }
    }
}
