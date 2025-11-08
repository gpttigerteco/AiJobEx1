namespace AiJobEx1.Domain.Entities;

public class AiSession : BaseEntity
{
    public Guid UserId { get; set; }
    public string? ContextHint { get; set; }

    public User User { get; set; } = default!;
    public ICollection<AiMessage> Messages { get; set; } = new List<AiMessage>();
}
