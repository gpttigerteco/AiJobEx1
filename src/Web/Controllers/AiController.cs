using AiJobEx1.Application.Ai.Commands;
using AiJobEx1.Application.Common.Models;
using AiJobEx1.Shared;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AiJobEx1.Web.Controllers;

[Authorize]
public class AiController : ApiControllerBase
{
    private readonly IMediator _mediator;

    public AiController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("messages")]
    public async Task<ActionResult<Result<AiResponse>>> Ask([FromBody] AskAiCommand command)
    {
        return Ok(await _mediator.Send(command));
    }
}
