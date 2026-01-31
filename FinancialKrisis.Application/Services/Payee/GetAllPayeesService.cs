using FinancialKrisis.Domain.Entities;
using FinancialKrisis.Domain.Repositories;

namespace FinancialKrisis.Application.Services;

public class GetAllPayeesService(IPayeeRepository pPayeeRepository) : GetAllEntitiesService<Payee, IPayeeRepository>(pPayeeRepository)
{
}

