namespace AiJobEx1.Domain.Entities;

public class AiMessage : BaseEntity
{
    public Guid SessionId { get; set; }
    public string Sender { get; set; } = default!;
    public string Text { get; set; } = default!;
    public int TokensInput { get; set; }
    public int TokensOutput { get; set; }
    public decimal Cost { get; set; }

    public AiSession Session { get; set; } = default!;
}
