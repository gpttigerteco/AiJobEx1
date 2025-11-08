namespace AiJobEx1.Application.Common.Models;

public record AiResponse(
    string Answer,
    IReadOnlyCollection<Citation> Citations,
    int InputTokens,
    int OutputTokens,
    decimal Cost,
    bool HasInsufficientData);

public record Citation(string Type, Guid? EntityId, string? Title, string? Url);
