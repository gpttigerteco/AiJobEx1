using AiJobEx1.Application.Common.Interfaces;
using AiJobEx1.Domain.Entities;

namespace AiJobEx1.Infrastructure.Persistence;

public class AuditTrailService : IAuditTrailService
{
    private readonly ApplicationDbContext _context;

    public AuditTrailService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task WriteAsync(AuditLog log, CancellationToken cancellationToken = default)
    {
        _context.AuditLogs.Add(log);
        await _context.SaveChangesAsync(cancellationToken);
    }
}
