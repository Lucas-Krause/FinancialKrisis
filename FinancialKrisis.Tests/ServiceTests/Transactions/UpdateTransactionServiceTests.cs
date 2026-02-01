using FinancialKrisis.Application.DTOs;
using FinancialKrisis.Domain.Entities;
using FinancialKrisis.Domain.Enums;
using FinancialKrisis.Tests.Scenarios;
using FinancialKrisis.Tests.Scenarios.Assertions;

namespace FinancialKrisis.Tests.ServiceTests.Transactions;

public class UpdateTransactionServiceTests
{
    [Fact]
    public void InvalidDateTime_ShouldFailWithDomainRuleException()
    {
        new TestContext()
            .Account().Create().AsCurrentAccount().ShouldCreateSuccessfully()
            .Transaction()
                .CreatingWithCurrentAccount()
                .Create()
                .AsCurrentTransaction()
                .UpdatingWith(CreateInput => CreateInput.DateTime = Optional<DateTime>.Remove())
                .Update()
                .ShouldFailWithDomainRuleException(DomainRuleErrorCode.RequiredField, typeof(Transaction), Transaction.Fields.DateTime);
    }
}
