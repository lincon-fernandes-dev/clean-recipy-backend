using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infra.Data.EntitiesConfiguration
{
    public class RecipeConfiguration : IEntityTypeConfiguration<Recipe>
    {
        public void Configure(EntityTypeBuilder<Recipe> builder)
        {
            // Tabela
            builder.ToTable("Recipes");

            // Chave Primária
            builder.HasKey(x => x.Id);

            // Propriedades
            builder.Property(x => x.Id)
                .HasColumnName("IdRecipe")
                .ValueGeneratedOnAdd()
                .IsRequired();

            builder.Property(x => x.Title)
                .HasMaxLength(100)
                .IsRequired()
                .HasColumnName("Title");

            builder.Property(x => x.Description)
                .HasMaxLength(524)
                .IsRequired()
                .HasColumnName("Description");

            builder.Property(x => x.ImageUrl)
                .HasMaxLength(100)
                .IsRequired()
                .HasColumnName("ImageUrl");

            builder.Property(r => r.Difficulty)
                .IsRequired()
                .HasMaxLength(20)
                .HasConversion(
                    v => v.ToString(),
                    v => v
                );

            builder.Property(r => r.PreparationTime)
                .IsRequired();

            builder.Property(r => r.Servings)
                .IsRequired();

            builder.Property(x => x.IdUser)
                .IsRequired()
                .HasColumnName("IdUser");

            // Campos de Auditoria
            builder.Property(x => x.CreatedAt)
                .IsRequired()
                .HasColumnName("CreatedAt")
                .HasDefaultValueSql("GETUTCDATE()");

            builder.Property(x => x.UpdatedAt)
                .IsRequired()
                .HasColumnName("UpdatedAt")
                .HasDefaultValueSql("GETUTCDATE()");

            // Relationships
            builder.HasOne(r => r.User)
                .WithMany(u => u.Recipes)
                .HasForeignKey(r => r.IdUser)
                .OnDelete(DeleteBehavior.ClientCascade);

            builder.HasMany(r => r.Ingredients)
                .WithOne(i => i.Recipe)
                .HasForeignKey(i => i.IdRecipe)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(r => r.Instructions)
                .WithOne(i => i.Recipe)
                .HasForeignKey(i => i.IdRecipe)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(r => r.RecipeTags)
                .WithOne(rt => rt.Recipe)
                .HasForeignKey(rt => rt.IdRecipe)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(r => r.RecipeLikes)
                .WithOne(rl => rl.Recipe)
                .HasForeignKey(rl => rl.IdRecipe)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(r => r.Comments)
                .WithOne(c => c.Recipe)
                .HasForeignKey(c => c.IdRecipe)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(r => r.NutritionInfo)
                .WithOne(n => n.Recipe)
                .HasForeignKey<NutritionInfo>(n => n.IdRecipe)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(x => x.Ingredients);
        }
    }
}