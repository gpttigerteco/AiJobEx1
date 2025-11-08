namespace AiJobEx1.Domain.ValueObjects;

public readonly record struct MarkdownContent(string Value)
{
    public static MarkdownContent Empty => new(string.Empty);

    public override string ToString() => Value;
}
