using AiJobEx1.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AiJobEx1.Infrastructure.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("Users");
        builder.HasKey(u => u.Id);
        builder.Property(u => u.FullName).HasMaxLength(200).IsRequired();
        builder.Property(u => u.Email).HasMaxLength(200).IsRequired();
        builder.Property(u => u.Phone).HasMaxLength(50);
        builder.Property(u => u.Position).HasMaxLength(200);

        builder.OwnsMany(u => u.AuditTrailEntries, navigationBuilder =>
        {
            navigationBuilder.ToJson();
        });
    }
}
