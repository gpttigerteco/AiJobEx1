using AiJobEx1.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AiJobEx1.Infrastructure.Configurations;

public class JobDescriptionConfiguration : IEntityTypeConfiguration<JobDescription>
{
    public void Configure(EntityTypeBuilder<JobDescription> builder)
    {
        builder.ToTable("JobDescriptions");
        builder.HasKey(j => j.Id);
        builder.Property(j => j.Title).HasMaxLength(200).IsRequired();
        builder.OwnsOne(j => j.Body, body =>
        {
            body.Property(p => p.Value).HasColumnName("BodyMarkdown").HasColumnType("nvarchar(max)");
        });

        builder.HasIndex(j => new { j.DepartmentId, j.UserId, j.IsActive });
    }
}
