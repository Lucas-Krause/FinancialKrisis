using FinancialKrisis.Application.DTOs;
using FinancialKrisis.Application.Services;
using FinancialKrisis.Domain.Entities;
using Microsoft.Extensions.DependencyInjection;

namespace FinancialKrisis.Tests.Scenarios;

public sealed class TransactionScenario(
    IServiceScope pScope, 
    decimal pAmount, 
    string pIdentifier, 
    string pDescription, 
    DateTime pDateTime, 
    Guid pAccountId, 
    Guid? pCategoryId, 
    Guid? pSubcategoryId, 
    Guid? pPayeeId) : BaseScenario(pScope)
{
    private readonly CreateTransactionService _createTransaction = pScope.ServiceProvider.GetRequiredService<CreateTransactionService>();
    private readonly UpdateTransactionService _updateTransaction = pScope.ServiceProvider.GetRequiredService<UpdateTransactionService>();

    public Task<Transaction> CreateAsync()
    {
        return _createTransaction.ExecuteAsync(new CreateTransactionDTO
        {
            Identifier = pIdentifier,
            Description = pDescription,
            DateTime = pDateTime,
            AccountId = pAccountId,
            CategoryId = pCategoryId,
            SubcategoryId = pSubcategoryId,
            PayeeId = pPayeeId,
            Amount = pAmount
        });
    }

    public Task<Transaction> UpdateAsync(Guid pTransactionId)
    {
        return _updateTransaction.ExecuteAsync(new UpdateTransactionDTO
        {
            Identifier = pIdentifier,
            Description = pDescription,
            Id = pTransactionId,
            DateTime = pDateTime,
            CategoryId = pCategoryId,
            SubcategoryId = pSubcategoryId,
            PayeeId = pPayeeId,
            Amount = pAmount
        });
    }
}
