using AiJobEx1.Domain.ValueObjects;

namespace AiJobEx1.Domain.Entities;

public class JobDescription : BaseEntity
{
    public string Title { get; set; } = default!;
    public Guid? DepartmentId { get; set; }
    public Guid? UserId { get; set; }
    public MarkdownContent Body { get; set; } = MarkdownContent.Empty;
    public int Version { get; set; } = 1;
    public bool IsActive { get; set; } = true;

    public Department? Department { get; set; }
    public User? User { get; set; }
    public ICollection<JobDescriptionChangeRequest> ChangeRequests { get; set; } = new List<JobDescriptionChangeRequest>();
}
