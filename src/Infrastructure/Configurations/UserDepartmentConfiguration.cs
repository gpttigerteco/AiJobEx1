using AiJobEx1.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AiJobEx1.Infrastructure.Configurations;

public class UserDepartmentConfiguration : IEntityTypeConfiguration<UserDepartment>
{
    public void Configure(EntityTypeBuilder<UserDepartment> builder)
    {
        builder.ToTable("UserDepartments");
        builder.HasKey(x => new { x.UserId, x.DepartmentId });
        builder.Property(x => x.RoleInDepartment).HasMaxLength(200);

        builder.HasIndex(x => new { x.UserId, x.DepartmentId });
    }
}
