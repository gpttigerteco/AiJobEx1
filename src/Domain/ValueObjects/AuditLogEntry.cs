namespace AiJobEx1.Domain.ValueObjects;

public record AuditLogEntry(string Action, string Description, DateTime CreatedAt)
{
    public static AuditLogEntry Create(string action, string description)
        => new(action, description, DateTime.UtcNow);
}
