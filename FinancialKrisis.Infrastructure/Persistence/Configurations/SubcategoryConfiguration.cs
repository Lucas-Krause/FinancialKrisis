using FinancialKrisis.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FinancialKrisis.Infrastructure.Persistence.Configurations;

public class SubcategoryConfiguration : IEntityTypeConfiguration<Subcategory>
{
    public void Configure(EntityTypeBuilder<Subcategory> pBuilder)
    {
        pBuilder.ToTable("SubCategories");

        pBuilder.HasKey(p => p.Id);

        pBuilder
            .Property(p => p.Name)
            .IsRequired()
            .HasMaxLength(100);

        pBuilder
            .Property(p => p.IsActive)
            .IsRequired();

        pBuilder
            .Property(p => p.CategoryId)
            .IsRequired();

        pBuilder
            .HasOne<Category>()
            .WithMany()
            .HasForeignKey(p => p.CategoryId)
            .OnDelete(DeleteBehavior.Restrict);

        pBuilder.HasIndex(p => p.CategoryId);
        pBuilder.HasIndex(p => new { p.CategoryId, p.Name });
    }
}
