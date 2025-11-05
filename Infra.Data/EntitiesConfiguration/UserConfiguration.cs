using Domain.Entities;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infra.Data.EntitiesConfiguration
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            // Tabela
            builder.ToTable("Users");

            // Chave Primária\
            builder.HasKey(x => x.Id);

            // Propriedades
            builder.Property(x => x.Id)
                .HasColumnName("IdUser")
                .ValueGeneratedOnAdd()
                .IsRequired();

            builder.Property(x => x.Name)
                .HasMaxLength(128)
                .IsRequired()
                .HasColumnName("Name");

            builder.Property(u => u.Email)
               .IsRequired()
               .HasMaxLength(255);

            builder.Property(u => u.Avatar)
                .HasMaxLength(500);

            builder.Property(x => x.PasswordHash)
                .HasMaxLength(512)
                .IsRequired()
                .HasColumnName("PasswordHash");

            builder.Property(x => x.Status)
                .IsRequired()
                .HasConversion<int>()
                .HasColumnName("Status")
                .HasDefaultValue(UserStatus.Active)
                .HasSentinel((UserStatus)999);

            builder.Property(x => x.UpdatedAt)
                .IsRequired()
                .HasColumnName("LastModifiedDate")
                .HasDefaultValueSql("GETUTCDATE()");


            // Índices
            builder.HasIndex(x => x.Email)
                .IsUnique()
                .HasDatabaseName("IX_Users_Email");

            builder.HasIndex(x => x.Name)
                .HasDatabaseName("IX_Users_Name");

            builder.HasIndex(x => x.Status)
                .HasDatabaseName("IX_Users_Status");

            // Relacionamentos
            builder.HasMany(u => u.Recipes)
               .WithOne(r => r.User)
               .HasForeignKey(r => r.IdUser)
               .OnDelete(DeleteBehavior.ClientCascade); 

            builder.HasMany(u => u.Comments)
                .WithOne(c => c.User)
                .HasForeignKey(c => c.IdUser)
                .OnDelete(DeleteBehavior.ClientCascade); 

            builder.HasMany(u => u.RecipeLikes)
                .WithOne(rl => rl.User)
                .HasForeignKey(rl => rl.IdUser)
                .OnDelete(DeleteBehavior.ClientCascade);

            builder.HasMany(u => u.CommentLikes)
                .WithOne(cl => cl.User)
                .HasForeignKey(cl => cl.IdUser)
                .OnDelete(DeleteBehavior.ClientCascade);

        }
    }
}