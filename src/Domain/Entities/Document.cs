using AiJobEx1.Domain.ValueObjects;

namespace AiJobEx1.Domain.Entities;

public class Document : BaseEntity
{
    public string Title { get; set; } = default!;
    public Guid? DepartmentId { get; set; }
    public Guid? UserId { get; set; }
    public string StorageReference { get; set; } = default!;
    public string? Tags { get; set; }
    public int Version { get; set; } = 1;
    public bool IsActive { get; set; } = true;

    public Department? Department { get; set; }
    public User? User { get; set; }
    public DocumentMetadata Metadata { get; set; } = DocumentMetadata.Empty;
}
