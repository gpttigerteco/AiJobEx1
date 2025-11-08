namespace AiJobEx1.Domain.Entities;

public class AuditLog : BaseEntity
{
    public Guid? UserId { get; set; }
    public string Action { get; set; } = default!;
    public string EntityName { get; set; } = default!;
    public Guid? EntityId { get; set; }
    public string? Before { get; set; }
    public string? After { get; set; }
    public string? IpAddress { get; set; }

    public User? User { get; set; }
}
