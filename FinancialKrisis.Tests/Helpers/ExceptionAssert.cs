using FinancialKrisis.Application.Enums;
using FinancialKrisis.Application.Exceptions;
using FinancialKrisis.Domain.Enums;
using FinancialKrisis.Domain.Exceptions;

namespace FinancialKrisis.Tests.Helpers;

public static class ExceptionAssert
{
    public static async Task AssertDomainRuleException<T>(Func<Task> action, DomainRuleErrorCode pDomainRuleErrorCode)
    {
        DomainRuleException ex = await Assert.ThrowsAsync<DomainRuleException>(action);

        Assert.NotNull(ex.Message);
        Assert.NotEqual(string.Empty, ex.Message);
        Assert.Equal(pDomainRuleErrorCode, ex.ErrorCode);
        Assert.Equal(typeof(T), ex.EntityType);
    }

    public static async Task AssertApplicationRuleException<T>(Func<Task> action, ApplicationRuleErrorCode pApplicationRuleErrorCode)
    {
        ApplicationRuleException ex = await Assert.ThrowsAsync<ApplicationRuleException>(action);

        Assert.NotNull(ex.Message);
        Assert.NotEqual(string.Empty, ex.Message);
        Assert.Equal(pApplicationRuleErrorCode, ex.ErrorCode);
        Assert.Equal(typeof(T), ex.EntityType);
    }
}
