using AiJobEx1.Application.Common.Interfaces;
using AiJobEx1.Application.Common.Models;

namespace AiJobEx1.Infrastructure.Ai;

public class MockAiProvider : IAiProvider
{
    public Task<AiResponse> AskAsync(AiRequest request, CancellationToken cancellationToken = default)
    {
        var answer = request.ContextChunks.Any()
            ? $"پاسخ ساختگی بر اساس {request.ContextChunks.Count} منبع." : "اطلاعات کافی در دسترس نیست.";

        var citations = request.RequestCitations
            ? request.ContextChunks.Select((chunk, index) =>
                new Citation($"Context{index + 1}", null, chunk.Length > 30 ? chunk[..30] + "..." : chunk, null)).ToArray()
            : Array.Empty<Citation>();

        var response = new AiResponse(
            answer,
            citations,
            InputTokens: request.UserPrompt.Length / 4,
            OutputTokens: answer.Length / 4,
            Cost: 0m,
            HasInsufficientData: !request.ContextChunks.Any());

        return Task.FromResult(response);
    }
}
