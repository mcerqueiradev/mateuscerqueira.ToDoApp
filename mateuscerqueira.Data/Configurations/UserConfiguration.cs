using mateuscerqueira.ToDoApp.Domain.Entities;
using mateuscerqueira.ToDoApp.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace mateuscerqueira.Data.Configurations
{
    internal class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users");

            builder.HasKey(u => u.Id);


            // Configuração do Value Object Name
            builder.OwnsOne(u => u.Name, n =>
            {
                n.Property(n => n.FirstName)
                    .IsRequired()
                    .HasMaxLength(50);

                n.Property(n => n.LastName)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            // Configuração do Email
            builder.Property(u => u.Email)
                .HasConversion(
                    e => e.Value,
                    e => new Email(e))
                .HasMaxLength(255);

            builder.HasIndex(u => u.Email).IsUnique();

            // Configuração do PasswordHash
            builder.OwnsOne(u => u.Password, p =>
            {
                p.Property(ph => ph.Hash)
                    .IsRequired()
                    .HasColumnType("bytea");

                p.Property(ph => ph.Salt)
                    .IsRequired()
                    .HasColumnType("bytea");

                p.Property(ph => ph.CreatedAt)
                    .IsRequired();
            });
        }
    }
}
