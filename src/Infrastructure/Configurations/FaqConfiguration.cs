using AiJobEx1.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AiJobEx1.Infrastructure.Configurations;

public class FaqConfiguration : IEntityTypeConfiguration<Faq>
{
    public void Configure(EntityTypeBuilder<Faq> builder)
    {
        builder.ToTable("Faqs");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Question).HasMaxLength(500).IsRequired();
        builder.Property(x => x.Answer).HasColumnType("nvarchar(max)");
    }
}
