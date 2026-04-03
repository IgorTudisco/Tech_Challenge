using CloudGame.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CloudGame.Infrastructure.EntityFramework.Mappings
{

    public sealed class UserMapping : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable(nameof(User));
            builder.HasKey(k => k.Id);
            builder.Property(p => p.Id).HasColumnType("INT").UseIdentityColumn();
            builder.Property(p => p.Name).IsRequired().HasColumnType("VARCHAR(120)");
            builder.Property(p => p.Email).IsRequired().HasColumnType("VARCHAR(120)");
            builder.Property(p => p.Password).IsRequired().HasColumnType("VARCHAR(250)");
            builder.Property(p => p.BirthDate).HasColumnType("DATETIME2");
            builder.Property(p => p.CreatedAt).HasColumnType("DATETIME2");
            builder.Property(p => p.UpdateAt).HasColumnType("DATETIME2");
            builder.Property(p => p.Active).HasColumnType("BIT");
            builder.Property(p => p.IsAdmin).HasColumnType("BIT");
        }
    }
}
