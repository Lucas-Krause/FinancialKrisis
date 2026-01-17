using FinancialKrisis.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FinancialKrisis.Infrastructure.Persistence.Configurations;

public class CategoryConfiguration : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> pBuilder)
    {
        pBuilder.ToTable("Categories");

        pBuilder.HasKey(p => p.Id);

        pBuilder
            .Property(p => p.Name)
            .IsRequired()
            .HasMaxLength(200);

        pBuilder
            .Property(p => p.IsActive)
            .IsRequired();

        pBuilder.HasIndex(p => p.Name);
        pBuilder.HasIndex(p => p.IsActive);
    }
}
