namespace AiJobEx1.Domain.ValueObjects;

public readonly record struct DocumentMetadata(string FileName, long SizeBytes, string ContentType)
{
    public static DocumentMetadata Empty => new(string.Empty, 0, string.Empty);
}
