using FinancialKrisis.Application.DTOs;
using FinancialKrisis.Domain.Entities;
using FinancialKrisis.Domain.Repositories;

namespace FinancialKrisis.Application.Services;

public class UpdatePayeeService(IPayeeRepository pRepository)
{
    public async Task<Payee> ExecuteAsync(UpdatePayeeDTO pUpdatePayeeDTO)
    {
        Payee payee = await pRepository.GetByIdOrThrowAsync(pUpdatePayeeDTO.Id);
        payee.Rename(pUpdatePayeeDTO.Name);
        await pRepository.UpdateAsync(payee);
        return payee;
    }
}
