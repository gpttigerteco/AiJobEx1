using AiJobEx1.Application.Common.Models;

namespace AiJobEx1.Application.Common.Interfaces;

public interface IAiProvider
{
    Task<AiResponse> AskAsync(AiRequest request, CancellationToken cancellationToken = default);
}
