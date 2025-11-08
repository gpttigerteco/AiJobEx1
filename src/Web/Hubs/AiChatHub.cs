using AiJobEx1.Application.Ai.Commands;
using AiJobEx1.Application.Common.Interfaces;
using AiJobEx1.Application.Common.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace AiJobEx1.Web.Hubs;

[Authorize]
public class AiChatHub : Hub
{
    private readonly IMediator _mediator;
    private readonly IAccessContext _accessContext;

    public AiChatHub(IMediator mediator, IAccessContext accessContext)
    {
        _mediator = mediator;
        _accessContext = accessContext;
    }

    public async Task SendMessage(string message, bool requestCitations)
    {
        var userId = _accessContext.GetCurrentUserId();
        if (!userId.HasValue)
        {
            throw new HubException("User not authenticated");
        }

        var response = await _mediator.Send(new AskAiCommand(userId.Value, message, requestCitations));
        if (!response.Succeeded || response.Value is null)
        {
            await Clients.Caller.SendAsync("ReceiveError", response.Error ?? "Unknown error");
            return;
        }

        await Clients.Caller.SendAsync("ReceiveMessage", new
        {
            answer = response.Value.Answer,
            citations = response.Value.Citations,
            usage = new
            {
                inputTokens = response.Value.InputTokens,
                outputTokens = response.Value.OutputTokens,
                cost = response.Value.Cost
            },
            hasInsufficientData = response.Value.HasInsufficientData
        });
    }
}
