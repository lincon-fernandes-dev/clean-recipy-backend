using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infra.Data.EntitiesConfiguration
{
    public class TagConfiguration : IEntityTypeConfiguration<Tag>
    {
        public void Configure(EntityTypeBuilder<Tag> builder)
        {
            builder.ToTable("Tags");
            builder.HasKey(t => t.Id);

            builder.Property(x => x.Id)
                .HasColumnName("IdTag")
                .ValueGeneratedOnAdd()
                .IsRequired();

            builder.Property(t => t.Title)
                .IsRequired()
                .HasMaxLength(64);

            builder.HasIndex(t => t.Title)
                .IsUnique();

            // Relationships
            builder.HasMany(t => t.RecipeTags)
                .WithOne(rt => rt.Tag)
                .HasForeignKey(rt => rt.IdTag)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
