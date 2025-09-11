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

            builder.OwnsOne(u => u.Name, n =>
            {
                n.Property(n => n.FirstName)
                    .IsRequired()
                    .HasMaxLength(50);

                n.Property(n => n.LastName)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            builder.Property(u => u.Email)
                .HasConversion(
                    e => e.Value,
                    e => new Email(e))
                .HasMaxLength(255);

            builder.HasIndex(u => u.Email).IsUnique();

            builder.OwnsOne(u => u.Password, p =>
            {
                p.Property(p => p.Hash)
                    .IsRequired()
                    .HasConversion(
                        h => Convert.ToBase64String(h),
                        h => Convert.FromBase64String(h));

                p.Property(p => p.Salt)
                    .IsRequired()
                    .HasConversion(
                        s => Convert.ToBase64String(s),
                        s => Convert.FromBase64String(s));

                p.Property(p => p.CreatedAt);
            });
        }
    }
}