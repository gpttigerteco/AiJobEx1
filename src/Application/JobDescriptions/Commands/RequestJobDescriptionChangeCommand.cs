using AiJobEx1.Application.Common.Interfaces;
using AiJobEx1.Domain.Entities;
using AiJobEx1.Domain.Enums;
using AiJobEx1.Domain.ValueObjects;
using AiJobEx1.Shared;
using FluentValidation;
using MediatR;

namespace AiJobEx1.Application.JobDescriptions.Commands;

public record RequestJobDescriptionChangeCommand(Guid JobDescriptionId, Guid UserId, string ProposedMarkdown)
    : IRequest<Result<JobDescriptionChangeRequest>>;

public class RequestJobDescriptionChangeCommandValidator : AbstractValidator<RequestJobDescriptionChangeCommand>
{
    public RequestJobDescriptionChangeCommandValidator()
    {
        RuleFor(x => x.JobDescriptionId).NotEmpty();
        RuleFor(x => x.UserId).NotEmpty();
        RuleFor(x => x.ProposedMarkdown).NotEmpty().MaximumLength(20000);
    }
}

public class RequestJobDescriptionChangeCommandHandler : IRequestHandler<RequestJobDescriptionChangeCommand, Result<JobDescriptionChangeRequest>>
{
    private readonly IApplicationDbContext _context;
    private readonly IDateTimeProvider _dateTimeProvider;

    public RequestJobDescriptionChangeCommandHandler(IApplicationDbContext context, IDateTimeProvider dateTimeProvider)
    {
        _context = context;
        _dateTimeProvider = dateTimeProvider;
    }

    public async Task<Result<JobDescriptionChangeRequest>> Handle(RequestJobDescriptionChangeCommand request, CancellationToken cancellationToken)
    {
        var jobDescription = await _context.JobDescriptions.FindAsync(new object?[] { request.JobDescriptionId }, cancellationToken);
        if (jobDescription is null)
        {
            return Result<JobDescriptionChangeRequest>.Failure("Job description not found");
        }

        var changeRequest = new JobDescriptionChangeRequest
        {
            Id = Guid.NewGuid(),
            TargetJobDescriptionId = request.JobDescriptionId,
            RequestedByUserId = request.UserId,
            ProposedBody = new MarkdownContent(request.ProposedMarkdown),
            Status = ChangeRequestStatus.Pending,
            CreatedAt = _dateTimeProvider.UtcNow
        };

        _context.JobDescriptionChangeRequests.Add(changeRequest);
        await _context.SaveChangesAsync(cancellationToken);

        return Result<JobDescriptionChangeRequest>.Success(changeRequest);
    }
}
