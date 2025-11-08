using AiJobEx1.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AiJobEx1.Infrastructure.Configurations;

public class AiMessageConfiguration : IEntityTypeConfiguration<AiMessage>
{
    public void Configure(EntityTypeBuilder<AiMessage> builder)
    {
        builder.ToTable("AiMessages");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Sender).HasMaxLength(20).IsRequired();
        builder.Property(x => x.Text).HasColumnType("nvarchar(max)");

        builder.HasIndex(x => new { x.SessionId, x.CreatedAt });
    }
}
