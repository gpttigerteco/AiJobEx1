using AiJobEx1.Application.Common.Interfaces;
using AiJobEx1.Application.Common.Models;
using AiJobEx1.Domain.Entities;
using AiJobEx1.Shared;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AiJobEx1.Application.Ai.Commands;

public record AskAiCommand(Guid UserId, string Message, bool RequestCitations) : IRequest<Result<AiResponse>>;

public class AskAiCommandHandler : IRequestHandler<AskAiCommand, Result<AiResponse>>
{
    private readonly IApplicationDbContext _context;
    private readonly IAiProvider _aiProvider;

    public AskAiCommandHandler(IApplicationDbContext context, IAiProvider aiProvider)
    {
        _context = context;
        _aiProvider = aiProvider;
    }

    public async Task<Result<AiResponse>> Handle(AskAiCommand request, CancellationToken cancellationToken)
    {
        var user = await _context.Users
            .Include(u => u.UserDepartments)
            .FirstOrDefaultAsync(u => u.Id == request.UserId, cancellationToken);

        if (user is null)
        {
            return Result<AiResponse>.Failure("User not found");
        }

        var contextChunks = await BuildContextAsync(user, cancellationToken);

        var aiRequest = new AiRequest(
            SystemPrompt: "دستیار داخلی سازمانی. تنها بر اساس داده‌های مجاز پاسخ بده و در صورت کمبود داده اعلام کن.",
            UserPrompt: request.Message,
            ContextChunks: contextChunks,
            MaxTokens: 512,
            Temperature: 0.2f,
            RequestCitations: request.RequestCitations);

        var response = await _aiProvider.AskAsync(aiRequest, cancellationToken);

        var session = new AiSession
        {
            Id = Guid.NewGuid(),
            UserId = user.Id,
            CreatedAt = DateTime.UtcNow,
            ContextHint = string.Join(" | ", contextChunks.Take(3))
        };

        var userMessage = new AiMessage
        {
            Id = Guid.NewGuid(),
            SessionId = session.Id,
            Sender = "User",
            Text = request.Message,
            TokensInput = response.InputTokens,
            TokensOutput = 0,
            Cost = 0,
            CreatedAt = DateTime.UtcNow
        };

        var aiMessage = new AiMessage
        {
            Id = Guid.NewGuid(),
            SessionId = session.Id,
            Sender = "AI",
            Text = response.Answer,
            TokensInput = response.InputTokens,
            TokensOutput = response.OutputTokens,
            Cost = response.Cost,
            CreatedAt = DateTime.UtcNow
        };

        session.Messages.Add(userMessage);
        session.Messages.Add(aiMessage);

        _context.AiSessions.Add(session);
        _context.AiMessages.AddRange(userMessage, aiMessage);
        await _context.SaveChangesAsync(cancellationToken);

        return Result<AiResponse>.Success(response);
    }

    private async Task<IReadOnlyCollection<string>> BuildContextAsync(User user, CancellationToken cancellationToken)
    {
        var departmentIds = user.UserDepartments.Select(ud => ud.DepartmentId).ToArray();

        var jobDescriptions = await _context.JobDescriptions
            .Where(jd => (jd.UserId == user.Id || (jd.DepartmentId != null && departmentIds.Contains(jd.DepartmentId.Value))) && jd.IsActive)
            .Select(jd => $"شرح وظایف: {jd.Title}\n{jd.Body.Value}")
            .ToListAsync(cancellationToken);

        var documents = await _context.Documents
            .Where(doc => doc.IsActive && (doc.UserId == user.Id || (doc.DepartmentId != null && departmentIds.Contains(doc.DepartmentId.Value))))
            .Select(doc => $"مستند: {doc.Title} -> {doc.StorageReference}")
            .ToListAsync(cancellationToken);

        var faqs = await _context.Faqs
            .Where(f => !f.DepartmentId.HasValue || departmentIds.Contains(f.DepartmentId.Value))
            .OrderByDescending(f => f.Popularity)
            .Select(f => $"Q: {f.Question}\nA: {f.Answer}")
            .Take(5)
            .ToListAsync(cancellationToken);

        return jobDescriptions.Concat(documents).Concat(faqs).ToArray();
    }
}
