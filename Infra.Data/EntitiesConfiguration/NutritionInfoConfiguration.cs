using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infra.Data.EntitiesConfiguration
{
    public class NutritionInfoConfiguration : IEntityTypeConfiguration<NutritionInfo>
    {
        public void Configure(EntityTypeBuilder<NutritionInfo> builder)
        {
            builder.ToTable("NutritionInfos");

            builder.HasKey(n => n.Id);

            builder.Property(n => n.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("IdNutritionInfo")
                .IsRequired();

            builder.Property(n => n.IdRecipe)
                .IsRequired()
                .HasColumnName("IdRecipe");

            builder.Property(n => n.Calories)
                .IsRequired()
                .HasColumnName("Calories");

            builder.Property(n => n.Proteins)
                .IsRequired()
                .HasColumnName("Proteins");

            builder.Property(n => n.Carbs)
                .IsRequired()
                .HasColumnName("Carbs");

            builder.Property(n => n.Fat)
                .IsRequired()
                .HasColumnName("Fat");

            // Campos de auditoria
            builder.Property(n => n.CreatedAt)
                .IsRequired()
                .HasColumnName("CreatedAt")
                .HasDefaultValueSql("GETUTCDATE()");

            builder.Property(n => n.UpdatedAt)
                .IsRequired()
                .HasColumnName("UpdatedAt")
                .HasDefaultValueSql("GETUTCDATE()");

            // Relationships
            builder.HasOne(n => n.Recipe)
                .WithOne(r => r.NutritionInfo)
                .HasForeignKey<NutritionInfo>(n => n.IdRecipe)
                .OnDelete(DeleteBehavior.Cascade);

            // Índice único para garantir uma única nutrition info por receita
            builder.HasIndex(n => n.IdRecipe)
                .IsUnique();
        }
    }
}
