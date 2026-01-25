using FinancialKrisis.Application.Helpers;
using FinancialKrisis.Domain.Entities;
using FinancialKrisis.Domain.Repositories;

namespace FinancialKrisis.Application.Services;

public class GetPayeeByIdService(IPayeeRepository pRepository)
{
    public async Task<Payee?> ExecuteAsync(Guid pPayeeId)
    {
        try
        {
            return await pRepository.GetByIdAsync(pPayeeId);
        }
        catch (Exception pEx)
        {
            throw ErrorMessageResolver.Resolve(pEx);
        }
    }
}
