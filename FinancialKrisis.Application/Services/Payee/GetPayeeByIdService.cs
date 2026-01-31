using FinancialKrisis.Domain.Entities;
using FinancialKrisis.Domain.Interfaces;

namespace FinancialKrisis.Application.Services;

public class GetPayeeByIdService(IPayeeRepository pPayeeRepository) : GetEntityEntityByIdService<Payee, IPayeeRepository>(pPayeeRepository)
{
}

