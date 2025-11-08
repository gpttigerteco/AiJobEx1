using AiJobEx1.Domain.Enums;
using AiJobEx1.Domain.ValueObjects;

namespace AiJobEx1.Domain.Entities;

public class JobDescriptionChangeRequest : BaseEntity
{
    public Guid TargetJobDescriptionId { get; set; }
    public Guid RequestedByUserId { get; set; }
    public MarkdownContent ProposedBody { get; set; } = MarkdownContent.Empty;
    public ChangeRequestStatus Status { get; set; } = ChangeRequestStatus.Pending;
    public Guid? ReviewerId { get; set; }
    public string? ReviewNote { get; set; }
    public DateTime? ResolvedAt { get; set; }

    public JobDescription TargetJobDescription { get; set; } = default!;
    public User RequestedBy { get; set; } = default!;
    public User? Reviewer { get; set; }
}
