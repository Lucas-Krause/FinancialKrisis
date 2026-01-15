using FinancialKrisis.Application.Services;
using FinancialKrisis.Domain.Repositories;
using FinancialKrisis.Infrastructure.Persistence;
using FinancialKrisis.Infrastructure.Repositories;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace FinancialKrisis.Tests.TestInfrastructure;

public static class TestServiceProviderFactory
{
    public static ServiceProvider Create()
    {
        ServiceCollection services = new();

        SqliteConnection connection = new("Data Source=:memory:");
        connection.Open();

        services
            .AddDbContext<FinancialKrisisDbContext>(pOptions => pOptions.UseSqlite(connection))
            .AddScoped<IAccountRepository, AccountRepository>()
            .AddScoped<CreateAccountService>()
            .AddScoped<GetAllAccountsService>()
            .AddScoped<GetAccountByIdService>()
            .AddScoped<UpdateAccountService>()
            .AddScoped<DeactivateAccountService>();

        ServiceProvider provider = services.BuildServiceProvider();

        using IServiceScope scope = provider.CreateScope();
        FinancialKrisisDbContext context = scope.ServiceProvider.GetRequiredService<FinancialKrisisDbContext>();
        context.Database.EnsureCreated();

        return provider;
    }
}
