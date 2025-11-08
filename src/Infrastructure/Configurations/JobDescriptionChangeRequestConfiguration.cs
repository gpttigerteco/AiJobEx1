using AiJobEx1.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AiJobEx1.Infrastructure.Configurations;

public class JobDescriptionChangeRequestConfiguration : IEntityTypeConfiguration<JobDescriptionChangeRequest>
{
    public void Configure(EntityTypeBuilder<JobDescriptionChangeRequest> builder)
    {
        builder.ToTable("JDChangeRequests");
        builder.HasKey(x => x.Id);

        builder.OwnsOne(x => x.ProposedBody, body =>
        {
            body.Property(p => p.Value).HasColumnName("ProposedBody").HasColumnType("nvarchar(max)");
        });
    }
}
