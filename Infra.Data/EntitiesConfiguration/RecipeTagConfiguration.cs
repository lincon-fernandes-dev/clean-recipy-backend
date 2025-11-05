using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infra.Data.EntitiesConfiguration
{
    public class RecipeTagConfiguration :
        IEntityTypeConfiguration<RecipeTag>
    {
        public void Configure(EntityTypeBuilder<RecipeTag> builder)
        {
            builder.ToTable("RecipeTags");

            builder.HasKey(rt => rt.Id);

            builder.Property(rt => rt.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("IdRecipeTag")
                .IsRequired();

            builder.Property(rt => rt.IdRecipe)
                .IsRequired()
                .HasColumnName("IdRecipe");

            builder.Property(rt => rt.IdTag)
                .IsRequired()
                .HasColumnName("IdTag");

            // Campos de auditoria
            builder.Property(rt => rt.CreatedAt)
                .IsRequired()
                .HasColumnName("CreatedAt")
                .HasDefaultValueSql("GETUTCDATE()");

            builder.Property(rt => rt.UpdatedAt)
                .IsRequired()
                .HasColumnName("UpdatedAt")
                .HasDefaultValueSql("GETUTCDATE()");

            // Unique constraint para evitar tags duplicadas na mesma receita
            builder.HasIndex(rt => new { rt.IdRecipe, rt.IdTag })
                .IsUnique();

            // Relationships
            builder.HasOne(rt => rt.Recipe)
                .WithMany(r => r.RecipeTags)
                .HasForeignKey(rt => rt.IdRecipe)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(rt => rt.Tag)
                .WithMany(t => t.RecipeTags)
                .HasForeignKey(rt => rt.IdTag)
                .OnDelete(DeleteBehavior.Cascade);

            // Índices
            builder.HasIndex(rt => rt.IdRecipe);
            builder.HasIndex(rt => rt.IdTag);
        }
    }
}
