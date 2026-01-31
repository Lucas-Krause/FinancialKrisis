using FinancialKrisis.Domain.Entities;
using FinancialKrisis.Domain.Repositories;

namespace FinancialKrisis.Application.Services;

public class GetPayeeByIdService(IPayeeRepository pPayeeRepository) : GetEntityEntityByIdService<Payee, IPayeeRepository>(pPayeeRepository)
{
}

