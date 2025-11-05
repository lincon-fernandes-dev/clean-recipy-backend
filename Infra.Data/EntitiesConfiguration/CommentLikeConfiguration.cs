using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infra.Data.EntitiesConfiguration
{
    public class CommentLikeConfiguration : IEntityTypeConfiguration<CommentLike>
    {
        public void Configure(EntityTypeBuilder<CommentLike> builder)
        {
            builder.ToTable("CommentLikes");

            builder.HasKey(cl => cl.Id);

            builder.Property(cl => cl.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("IdCommentLike")
                .IsRequired();

            builder.Property(cl => cl.IdComment)
                .IsRequired()
                .HasColumnName("IdComment");

            builder.Property(cl => cl.IdUser)
                .IsRequired()
                .HasColumnName("IdUser");

            builder.HasIndex(cl => new { cl.IdComment, cl.IdUser })
                .IsUnique();

            builder.Property(x => x.CreatedAt)
                .IsRequired()
                .HasColumnName("CreatedAt")
                .HasDefaultValueSql("GETUTCDATE()");

            builder.Property(x => x.UpdatedAt)
                .IsRequired()
                .HasColumnName("UpdatedAt")
                .HasDefaultValueSql("GETUTCDATE()");

            // Relationships
            builder.HasOne(cl => cl.Comment)
                .WithMany(c => c.CommentLikes)
                .HasForeignKey(cl => cl.IdComment)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(cl => cl.User)
                .WithMany(u => u.CommentLikes)
                .HasForeignKey(cl => cl.IdUser)
                .OnDelete(DeleteBehavior.Cascade);

            // Índices
            builder.HasIndex(cl => cl.IdComment);
            builder.HasIndex(cl => cl.IdUser);
        }
    }
}
