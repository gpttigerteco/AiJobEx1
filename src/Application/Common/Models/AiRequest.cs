namespace AiJobEx1.Application.Common.Models;

public record AiRequest(
    string SystemPrompt,
    string UserPrompt,
    IReadOnlyCollection<string> ContextChunks,
    int MaxTokens,
    float Temperature,
    bool RequestCitations);
