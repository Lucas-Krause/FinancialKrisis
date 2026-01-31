using FinancialKrisis.Domain.Entities;
using FinancialKrisis.Domain.Interfaces;

namespace FinancialKrisis.Application.Services;

public class GetAllPayeesService(IPayeeRepository pPayeeRepository) : GetAllEntitiesService<Payee, IPayeeRepository>(pPayeeRepository)
{
}

