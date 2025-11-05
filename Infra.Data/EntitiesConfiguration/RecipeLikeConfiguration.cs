using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infra.Data.EntitiesConfiguration
{
    public class RecipeLikeConfiguration : IEntityTypeConfiguration<RecipeLike>
    {
        public void Configure(EntityTypeBuilder<RecipeLike> builder)
        {
            builder.ToTable("RecipeLikes");
            builder.HasKey(rl => rl.Id);

            builder.Property(x => x.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("IdRecipeLike")
                .IsRequired();

            // Unique constraint para evitar likes duplicados
            builder.HasIndex(rl => new { rl.IdRecipe, rl.IdUser })
                .IsUnique();

            builder.Property(rl => rl.CreatedAt)
                .IsRequired();

            // Relationships
            builder.HasOne(rl => rl.Recipe)
                .WithMany(r => r.RecipeLikes)
                .HasForeignKey(rl => rl.IdRecipe)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(rl => rl.User)
                .WithMany(u => u.RecipeLikes)
                .HasForeignKey(rl => rl.IdUser)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}