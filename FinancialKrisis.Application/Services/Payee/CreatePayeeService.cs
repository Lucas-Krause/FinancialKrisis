using FinancialKrisis.Application.DTOs;
using FinancialKrisis.Application.Enums;
using FinancialKrisis.Application.Exceptions;
using FinancialKrisis.Domain.Entities;
using FinancialKrisis.Domain.Interfaces;

namespace FinancialKrisis.Application.Services;

public class CreatePayeeService(IPayeeRepository pPayeeRepository, IUnitOfWork pUnitOfWork) : CreateEntityService<Payee, IPayeeRepository, CreatePayeeDTO>(pPayeeRepository, pUnitOfWork)
{
    protected override async Task<Payee> CreateEntity(CreatePayeeDTO pCreateDTO)
    {
        var payee = new Payee(pCreateDTO.Name);

        if (await EntityRepository.ExistsByNameAsync(payee.Name))
            throw new ApplicationRuleException(ApplicationRuleErrorCode.ValueAlreadyInUse, typeof(Payee), Payee.Fields.Name);

        return payee;
    }
}
