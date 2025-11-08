using AiJobEx1.Application.JobDescriptions.Commands;
using AiJobEx1.Application.JobDescriptions.Queries;
using AiJobEx1.Domain.Entities;
using AiJobEx1.Shared;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AiJobEx1.Web.Controllers;

[Authorize]
public class JobDescriptionsController : ApiControllerBase
{
    private readonly IMediator _mediator;

    public JobDescriptionsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("my")]
    public async Task<ActionResult<Result<JobDescription?>>> GetMyJobDescription()
    {
        // در پیاده‌سازی واقعی شناسه کاربر جاری استفاده خواهد شد.
        return Ok(await _mediator.Send(new GetMyJobDescriptionQuery(Guid.Empty)));
    }

    [HttpPost("requests")]
    public async Task<ActionResult<Result<JobDescriptionChangeRequest>>> RequestChange([FromBody] RequestJobDescriptionChangeCommand command)
    {
        return Ok(await _mediator.Send(command));
    }
}
