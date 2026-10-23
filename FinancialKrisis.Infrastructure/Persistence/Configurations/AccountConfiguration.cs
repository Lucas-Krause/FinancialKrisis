using FinancialKrisis.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FinancialKrisis.Infrastructure.Persistence.Configurations;

public class AccountConfiguration : IEntityTypeConfiguration<Account>
{
    public void Configure(EntityTypeBuilder<Account> pBuilder)
    {
        pBuilder.ToTable("Accounts");

        pBuilder.HasKey(p => p.Id);

        pBuilder.HasEntityName();

        pBuilder
            .Property(p => p.InitialBalance)
            .HasColumnType("decimal(18,2)")
            .IsRequired();

        pBuilder
            .Property(p => p.IsActive)
            .IsRequired();

        pBuilder.HasIndex(p => p.Name.NormalizedValue).IsUnique();
        pBuilder.HasIndex(p => p.IsActive);
    }
}
