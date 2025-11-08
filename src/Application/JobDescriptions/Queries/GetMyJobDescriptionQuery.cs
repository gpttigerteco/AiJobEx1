using AiJobEx1.Application.Common.Interfaces;
using AiJobEx1.Domain.Entities;
using AiJobEx1.Shared;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AiJobEx1.Application.JobDescriptions.Queries;

public record GetMyJobDescriptionQuery(Guid UserId) : IRequest<Result<JobDescription?>>;

public class GetMyJobDescriptionQueryHandler : IRequestHandler<GetMyJobDescriptionQuery, Result<JobDescription?>>
{
    private readonly IApplicationDbContext _context;

    public GetMyJobDescriptionQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Result<JobDescription?>> Handle(GetMyJobDescriptionQuery request, CancellationToken cancellationToken)
    {
        var jobDescription = await _context.JobDescriptions
            .Include(j => j.Department)
            .FirstOrDefaultAsync(j => j.UserId == request.UserId && j.IsActive, cancellationToken);

        return Result<JobDescription?>.Success(jobDescription);
    }
}
