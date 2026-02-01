using FinancialKrisis.Domain.Entities;
using FinancialKrisis.Domain.Enums;
using FinancialKrisis.Tests.Scenarios;
using FinancialKrisis.Tests.Scenarios.Assertions;

namespace FinancialKrisis.Tests.ServiceTests.Transactions;

public class CreateTransactionServiceTests
{
    [Fact]
    public void InvalidDateTime_ShouldFailWithDomainRuleException()
    {
        new TestContext()
            .Account().Create().AsCurrentAccount().ShouldCreateSuccessfully()
            .Transaction()
                .CreatingWithCurrentAccount()
                .CreatingWith(CreateInput => CreateInput.DateTime = default)
                .Create()
                .ShouldFailWithDomainRuleException(DomainRuleErrorCode.RequiredField, typeof(Transaction), Transaction.Fields.DateTime);
    }
}
