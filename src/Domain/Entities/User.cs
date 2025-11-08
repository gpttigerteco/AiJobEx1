using AiJobEx1.Domain.ValueObjects;

namespace AiJobEx1.Domain.Entities;

public class User : BaseEntity
{
    public string FullName { get; set; } = default!;
    public string Email { get; set; } = default!;
    public string? Phone { get; set; }
    public string? Position { get; set; }
    public bool IsActive { get; set; } = true;
    public DateTime? LastLoginAt { get; set; }

    public ICollection<UserDepartment> UserDepartments { get; set; } = new List<UserDepartment>();
    public ICollection<JobDescription> JobDescriptions { get; set; } = new List<JobDescription>();
    public ICollection<JobDescriptionChangeRequest> ChangeRequests { get; set; } = new List<JobDescriptionChangeRequest>();
    public ICollection<AiSession> AiSessions { get; set; } = new List<AiSession>();

    public void Deactivate(string reason)
    {
        IsActive = false;
        AuditTrailEntries.Add(AuditLogEntry.Create("Deactivate", reason));
    }

    public IList<AuditLogEntry> AuditTrailEntries { get; set; } = new List<AuditLogEntry>();
}
