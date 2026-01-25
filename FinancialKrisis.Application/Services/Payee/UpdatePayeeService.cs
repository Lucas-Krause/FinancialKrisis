using FinancialKrisis.Application.DTOs;
using FinancialKrisis.Application.Helpers;
using FinancialKrisis.Domain.Entities;
using FinancialKrisis.Domain.Repositories;

namespace FinancialKrisis.Application.Services;

public class UpdatePayeeService(IPayeeRepository pRepository)
{
    public async Task<Payee> ExecuteAsync(UpdatePayeeDTO pUpdatePayeeDTO)
    {
        try
        {
            var payee = (Payee)ActiveEntityValidator.EnsureIsActive(await pRepository.GetByIdOrThrowAsync(pUpdatePayeeDTO.Id));

            if (pUpdatePayeeDTO.Name.IsDefined)
                payee.Rename(pUpdatePayeeDTO.Name.Value!);

            await pRepository.UpdateAsync(payee);
            return payee;
        }
        catch (Exception pEx)
        {
            throw ErrorMessageResolver.Resolve(pEx);
        }
    }
}
