using AiJobEx1.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AiJobEx1.Infrastructure.Configurations;

public class DocumentConfiguration : IEntityTypeConfiguration<Document>
{
    public void Configure(EntityTypeBuilder<Document> builder)
    {
        builder.ToTable("Documents");
        builder.HasKey(d => d.Id);
        builder.Property(d => d.Title).HasMaxLength(300).IsRequired();
        builder.Property(d => d.StorageReference).HasMaxLength(1000).IsRequired();
        builder.Property(d => d.Tags).HasMaxLength(500);

        builder.OwnsOne(d => d.Metadata, metadata =>
        {
            metadata.Property(m => m.FileName).HasMaxLength(260);
            metadata.Property(m => m.ContentType).HasMaxLength(150);
        });
    }
}
