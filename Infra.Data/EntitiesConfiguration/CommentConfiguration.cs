using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infra.Data.EntitiesConfiguration
{
    public class CommentConfiguration : IEntityTypeConfiguration<Comment>
    {
        public void Configure(EntityTypeBuilder<Comment> builder)
        {
            builder.ToTable("Comments");

            builder.HasKey(c => c.Id);

            builder.Property( c => c.Id)
                .HasColumnName("IdComment")
                .ValueGeneratedOnAdd()
                .IsRequired();

            builder.Property(c => c.Content)
                .IsRequired()
                .HasMaxLength(521);

            builder.Property(c => c.IdRecipe)
                .IsRequired()
                .HasColumnName("IdRecipe");

            builder.Property(c => c.IdUser)
                .IsRequired()
                .HasColumnName("IdUser");

            builder.Property(c => c.ParentCommentId)
                .IsRequired(false)
                .HasColumnName("ParentCommentId");

            builder.Property(c => c.CreatedAt)
               .IsRequired()
               .HasColumnName("CreatedAt")
               .HasDefaultValueSql("GETUTCDATE()");

            builder.Property(c => c.UpdatedAt)
                .IsRequired()
                .HasColumnName("UpdatedAt")
                .HasDefaultValueSql("GETUTCDATE()");

            // Relationships
            builder.HasOne(c => c.Recipe)
                .WithMany(r => r.Comments)
                .HasForeignKey(c => c.IdRecipe)
                .OnDelete(DeleteBehavior.ClientCascade);

            builder.HasOne(c => c.User)
                .WithMany(u => u.Comments)
                .HasForeignKey(c => c.IdUser)
                .OnDelete(DeleteBehavior.ClientCascade); 

            // Self-referencing relationship para replies
            builder.HasOne(c => c.ParentComment)
                .WithMany(c => c.Replies)
                .HasForeignKey(c => c.ParentCommentId)
                .OnDelete(DeleteBehavior.ClientCascade); 

            // Índices
            builder.HasIndex(c => c.IdRecipe);
            builder.HasIndex(c => c.IdUser);
            builder.HasIndex(c => c.ParentCommentId);
        }
    }
}
