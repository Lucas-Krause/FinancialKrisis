using FinancialKrisis.Application.Helpers;
using FinancialKrisis.Domain.Entities;
using FinancialKrisis.Domain.Repositories;

namespace FinancialKrisis.Application.Services;

public class DeactivatePayeeService(IPayeeRepository pRepository)
{
    public async Task ExecuteAsync(Guid pPayeeId)
    {
        try
        {
            Payee payee = await pRepository.GetByIdOrThrowAsync(pPayeeId);
            payee.Deactivate();
            await pRepository.UpdateAsync(payee);
        }
        catch (Exception pEx)
        {
            throw ErrorMessageResolver.Resolve(pEx);
        }
    }
}
