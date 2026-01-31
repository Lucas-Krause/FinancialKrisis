using FinancialKrisis.Application.DTOs;
using FinancialKrisis.Domain.Entities;
using FinancialKrisis.Domain.Repositories;

namespace FinancialKrisis.Application.Services;

public class UpdatePayeeService(IPayeeRepository pPayeeRepository) : UpdateEntityService<Payee, IPayeeRepository, UpdatePayeeDTO>(pPayeeRepository)
{
    protected override async Task ApplyChangesToEntity(Payee pPayee, UpdatePayeeDTO pUpdateDTO)
    {
        if (pUpdateDTO.Name.IsDefined)
            pPayee.ChangeName(pUpdateDTO.Name.Value!);
    }
}
